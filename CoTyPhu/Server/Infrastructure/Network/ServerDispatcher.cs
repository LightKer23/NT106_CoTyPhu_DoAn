using Common.Constracts;
using Common.Constracts.Room;
using Common.Contracts.Auth;
using Common.Contracts.Game;
using Common.Contracts.Room;
using Common.Domain.Game.Enums;
using Common.Domain.Models.Entities;
using Server.Domain;
using Server.Domain.GameLogic;
using Server.Domain.GameState;
using Server.Domain.GameState.Board;
using Server.Infrastructure.Database.Connection;
using Server.Infrastructure.Database.Repository;
using Server.Infrastructure.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server.Infrastructure.Network
{
    public sealed class ServerDispatcher : IRequestDispatcher
    {
        private static readonly JsonSerializerOptions JsonOpt = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly GameFlowService flow = new();


        private readonly AccountRepo _accountRepo;
        private readonly MatchRepo _matchRepo;
        private readonly PlayerRepo _playerRepo;
        private static readonly object _locksGuard = new();
        private static readonly Dictionary<int, object> _matchLocks = new();

        private static object GetMatchLock(int matchId)
        {
            lock (_locksGuard)
            {
                if (!_matchLocks.TryGetValue(matchId, out var o))
                    _matchLocks[matchId] = o = new object();
                return o;
            }
        }

        private static readonly Dictionary<int, ClientConnection> _connections = new();

        private static readonly Dictionary<(int matchId, int playerId), int> _doubleStreak = new();

        private const int JailTileIndex = 30;

        private static int GetStreak(MatchState match, PlayerState p)
            => _doubleStreak.TryGetValue((match.MatchId, p.PlayerId), out var v) ? v : 0;

        private static void SetStreak(MatchState match, PlayerState p, int v)
            => _doubleStreak[(match.MatchId, p.PlayerId)] = v;

        private static void SendToJail(MatchState match, PlayerState p)
        {
            p.InJail = true;
            p.Position = JailTileIndex;
            SetStreak(match, p, 0);
        }

        private Card DrawChanceCard(MatchState match)
        {
            if (match.ChanceDeck.Count == 0)
                match.ChanceDeck = ChanceDeckLoader.LoadDefaultDeck();

            var card = match.ChanceDeck[0];
            match.ChanceDeck.RemoveAt(0);

            if (card.ChanceType != ChanceCardType.GetOutOfJailFree)
                match.ChanceDeck.Add(card);


            return card;
        }

        private Card DrawCommunityChestCard(MatchState match)
        {
            if (match.CommunityChestDeck.Count == 0)
                match.CommunityChestDeck = CommunityChestDeckLoader.LoadDefaultDeck();

            var card = match.CommunityChestDeck[0];
            match.CommunityChestDeck.RemoveAt(0);

            if (card.ChestType != CommunityChestCardType.GetOutOfJailFree)
            {
                match.CommunityChestDeck.Add(card);
            }

            return card;
        }

        public ServerDispatcher()
        {
            var db = new DBConnection();
            _accountRepo = new AccountRepo(db);
            _matchRepo = new MatchRepo(db);
            _playerRepo = new PlayerRepo(db);

            // ✅ ĐĂNG KÝ CALLBACK với DELAY để đợi animation di chuyển hoàn thành
            flow.OnDrawCard = async (match, playerId, cardType, cardIndex, description) =>
            {
                Console.WriteLine($"[OnDrawCard] Starting delay for player {playerId}, card {cardType}_{cardIndex}");
                
                // ✅ ĐỢI animation di chuyển hoàn thành
                // Animation: 200ms/bước × ~7 bước = ~1.4s
                // Delay 1.5s để đảm bảo animation đã xong
                await Task.Delay(1500);
                
                Console.WriteLine($"[OnDrawCard] Broadcasting DrawCardEvent for player {playerId}");
                BroadcastDrawCard(match, playerId, cardType, cardIndex, description);
                Console.WriteLine($"[OnDrawCard] Broadcast completed");
            };

            // ✅ ĐĂNG KÝ CALLBACK để broadcast jail event
            flow.OnPlayerJailed = (match, player, reason, fromTile) =>
            {
                BroadcastPlayerJailed(match, player, reason, fromTile);
            };

            // ✅ ĐĂNG KÝ CALLBACK để broadcast movement từ card
            flow.OnPlayerMoved = (match, player, fromTile, toTile) =>
            {
                Console.WriteLine($"[OnPlayerMoved] Player {player.PlayerId} moved by card from {fromTile} to {toTile}");
                
                // ✅ CHECK ĐI QUA GO (tile 0) KHI DI CHUYỂN TỪ CARD
                bool passedGo = toTile < fromTile && toTile != 10; // Không tính khi vào tù (tile 10)
                if (passedGo)
                {
                    int moneyBefore = player.Money;
                    player.Money += 200;
                    BroadcastMoneyChanged(match, player.PlayerId, 200, player.Money);
                    Console.WriteLine($"[OnPlayerMoved] Player {player.PlayerId} passed GO from card, received $200");
                }
                
                // Broadcast PlayerMovedEvent với Roll1=0, Roll2=0 và FromTile/ToTile
                BroadcastRoom(match.MatchId, Wrap(
                    MessageType.PlayerMovedEvent,
                    new PlayerMoveEvent 
                    { 
                        PlayerId = player.PlayerId, 
                        Roll1 = 0, 
                        Roll2 = 0,
                        FromTile = fromTile,
                        ToTile = toTile
                    },
                    match.MatchId, 
                    null
                ));
            };
        }

        public Task<MessageEnvelope> DispatchAsync(MessageEnvelope req)
        {
            return req.Type switch
            {
                MessageType.LoginRequest => Task.FromResult(HandleLogin(req)),
                MessageType.RegisterRequest => Task.FromResult(HandleRegister(req)),
                MessageType.ForgotPasswordRequest => Task.FromResult(HandleForgotPassword(req)),
                MessageType.VerifyOTPRequest => Task.FromResult(HandleVerifyOtp(req)),
                MessageType.ResetPasswordRequest => Task.FromResult(HandleResetPassword(req)),

                MessageType.CreateRoomRequest => Task.FromResult(HandleCreateRoom(req)),
                MessageType.SearchRoomRequest => Task.FromResult(HandleSearchRoom(req)),
                MessageType.JoinRoomRequest => Task.FromResult(HandleJoinRoom(req)),
                MessageType.LeaveMatchRequest => Task.FromResult(HandleLeaveRoom(req)),
                MessageType.StartMatchRequest => Task.FromResult(HandleStartMatch(req)),

                MessageType.RollDiceRequest => Task.FromResult(HandleRollDice(req)),
                MessageType.BuyDecisionRequest => Task.FromResult(HandleBuyDecision(req)),
                MessageType.EndTurnRequest => Task.FromResult(HandleEndTurn(req)),
                MessageType.PlayerSurrenderRequest => Task.FromResult(HandlePlayerSurrender(req)),
                MessageType.SendChatMessageRequest => Task.FromResult(HandleSendChatMessage(req)),
                MessageType.GetOutOfJailRequest => Task.FromResult(HandleGetOutOfJail(req)),

                _ => Task.FromResult(MakeError("No handler for message type"))
            };
        }

        #region Auth Handlers
        private MessageEnvelope HandleLogin(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<LoginRequest>(req.Payload, JsonOpt)!;
            string hash = NormalizeToSha256Hex(body.Password);

            bool ok = _accountRepo.CheckLogin(body.Username, hash);
            int? id = ok ? _accountRepo.GetIdByLogin(body.Username, hash) : null;

            if (ok && id != null)
            {
                _connections[id.Value] = ServerState.CurrentConnection!;
            }

            return Wrap(
                MessageType.LoginResponse,
                new LoginResponse
                {
                    Success = ok,
                    Message = ok ? "Đăng nhập thành công" : "Sai tài khoản hoặc mật khẩu",
                    IDAccount = id
                });
        }

        private MessageEnvelope HandleRegister(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<RegisterRequest>(req.Payload, JsonOpt)!;

            if (_accountRepo.CheckUsername(body.Username))
                return Wrap(MessageType.RegisterResponse,
                    new RegisterResponse { Success = false, Message = "Username đã tồn tại" });

            if (_accountRepo.CheckEmail(body.Email))
                return Wrap(MessageType.RegisterResponse,
                    new RegisterResponse { Success = false, Message = "Email đã tồn tại" });

            int id = _accountRepo.Insert(new Account
            {
                Username = body.Username,
                PasswordHash = NormalizeToSha256Hex(body.Password),
                Email = body.Email,
                DisplayName = body.DisplayName
            });

            return Wrap(
                MessageType.RegisterResponse,
                new RegisterResponse
                {
                    Success = id > 0,
                    Message = id > 0 ? "Đăng ký thành công" : "Đăng ký thất bại"
                });
        }

        private MessageEnvelope HandleForgotPassword(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<ForgotPasswordRequest>(req.Payload, JsonOpt)!;
            if (!_accountRepo.CheckEmail(body.Email))
                return Wrap(MessageType.ForgotPasswordResponse,
                    new ForgotPasswordResponse { Success = false });

            GenerateOtp(body.Email);
            return Wrap(MessageType.ForgotPasswordResponse,
                new ForgotPasswordResponse { Success = true });
        }

        private MessageEnvelope HandleVerifyOtp(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<VerifyOTPRequest>(req.Payload, JsonOpt)!;
            return Wrap(
                MessageType.VerifyOTPResponse,
                new VerifyOTPResponse { Success = VerifyOtp(body.Email, body.OTP) });
        }

        private MessageEnvelope HandleResetPassword(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<ResetPasswordRequest>(req.Payload, JsonOpt)!;

            bool ok = _accountRepo.ChangePasswordByEmail(
                body.Email,
                NormalizeToSha256Hex(body.NewPassword));

            return Wrap(
                MessageType.ResetPasswordResponse,
                new ResetPasswordResponse { Success = ok });
        }
        #endregion

        #region Room Handlers
        private MessageEnvelope HandleCreateRoom(MessageEnvelope req)
        {
            int matchId = _matchRepo.CreateMatch();
            if (matchId <= 0)
                return MakeError("Tạo phòng thất bại!");

            ServerState.Matches[matchId] = new MatchState
            {
                MatchId = matchId,
                IsMatch = 0,
                CurrentTurnPlayerId = 0
            };

            return Wrap(
                MessageType.CreateRoomResponse,
                new CreateRoomResponse { Success = true, RoomID = matchId },
                matchId,
                null
            );
        }

        private MessageEnvelope HandleSearchRoom(MessageEnvelope req)
        {
            if (req.MatchId == null ||
                !ServerState.Matches.TryGetValue(req.MatchId.Value, out var match))
            {
                return Wrap(MessageType.SearchRoomResponse,
                    new SearchRoomResponse { Success = false });
            }

            return Wrap(
                MessageType.SearchRoomResponse,
                new SearchRoomResponse
                {
                    Success = true,
                    RoomId = match.MatchId,
                    PlayerRooms = match.Players.Values
                        .Select(p => p.CharacterIndex)
                        .Where(c => c > 0)
                        .ToList(),
                    Players = match.Players.Values
                        .OrderBy(p => p.PlayerId)
                        .Select(p => 
                        {
                            var acc = _accountRepo.GetById(p.AccountId);
                            return new RoomPlayerInfo
                            {
                                DisplayName = acc?.Username ?? $"Player {p.PlayerId}",
                                PlayerId = p.PlayerId,
                                CharacterIndex = p.CharacterIndex
                            };
                        })
                        .ToList()
                },
                match.MatchId,
                null
            );
        }

        private MessageEnvelope HandleGetOutOfJail(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<GetOutOfJailRequest>(req.Payload, JsonOpt)!;

            if (!req.MatchId.HasValue || !req.PlayerId.HasValue)
                return MakeError("Invalid GetOutOfJail request");

            if (!ServerState.Matches.TryGetValue(req.MatchId.Value, out var match))
                return MakeError("Match not found");

            if (!match.Players.TryGetValue(req.PlayerId.Value, out var player))
                return MakeError("Player not found");

            if (!player.InJail)
                return MakeError("Player is not in jail");

            Console.WriteLine($"[HandleGetOutOfJail] Player {player.PlayerId} method: {body.Method}");

            switch (body.Method)
            {
                case "PayMoney":
                    if (player.Money < 200)
                    {
                        return Wrap(
                            MessageType.ErrorResponse,
                            new { Message = "Không đủ $200 để ra tù!" },
                            match.MatchId,
                            player.PlayerId
                        );
                    }

                    player.Money -= 200;
                    player.InJail = false;
                    player.JailTurnsRemaining = 0;

                    BroadcastMoneyChanged(match, player.PlayerId, -200, player.Money);

                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PlayerReleasedFromJailEvent,
                        new PlayerReleasedFromJailEvent
                        {
                            PlayerId = player.PlayerId,
                            Method = "PayMoney",
                            Message = $"Player {player.PlayerId} đã trả $200 để ra tù"
                        },
                        match.MatchId, null));

                    return Wrap(
                        MessageType.GetOutOfJailRequest,
                        new GetOutOfJailResponse
                        {
                            Success = true,
                            Method = "PayMoney",
                            Message = "Đã trả $200 để ra tù. Hãy tung xúc xắc!",
                            MoneyPaid = 200
                        },
                        match.MatchId,
                        player.PlayerId
                    );

                case "UseCard":
                    if (!player.hasGetOutOfJailCard)
                    {
                        return Wrap(
                            MessageType.ErrorResponse,
                            new { Message = "Bạn không có thẻ ra tù!" },
                            match.MatchId,
                            player.PlayerId
                        );
                    }

                    player.hasGetOutOfJailCard = false;
                    player.InJail = false;
                    player.JailTurnsRemaining = 0;

                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PlayerReleasedFromJailEvent,
                        new PlayerReleasedFromJailEvent
                        {
                            PlayerId = player.PlayerId,
                            Method = "UseCard",
                            Message = $"Player {player.PlayerId} đã sử dụng thẻ ra tù"
                        },
                        match.MatchId, null));

                    return Wrap(
                        MessageType.GetOutOfJailRequest,
                        new GetOutOfJailResponse
                        {
                            Success = true,
                            Method = "UseCard",
                            Message = "Đã sử dụng thẻ ra tù. Hãy tung xúc xắc!",
                            UsedCard = true
                        },
                        match.MatchId,
                        player.PlayerId
                    );

                default:
                    return MakeError("Invalid method");
            }
        }
        private MessageEnvelope HandleJoinRoom(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<JoinRoomRequest>(req.Payload, JsonOpt)!;

            if (!ServerState.Matches.TryGetValue(body.RoomID, out var match))
                return Wrap(MessageType.JoinRoomResponse, new JoinRoomResponse { Success = false });

            lock (GetMatchLock(match.MatchId))
            {
                if (match.IsMatch != 0)
                    return Wrap(MessageType.JoinRoomResponse, new JoinRoomResponse { Success = false });

                if (match.Players.Values.Any(p => p.CharacterIndex == body.CharacterIndex))
                    return Wrap(MessageType.JoinRoomResponse, new JoinRoomResponse { Success = false });

                int slot = Enumerable.Range(1, 4).FirstOrDefault(i => !match.Players.ContainsKey(i));
                if (slot == 0)
                    return Wrap(MessageType.JoinRoomResponse, new JoinRoomResponse { Success = false });

                match.Players[slot] = new PlayerState
                {
                    PlayerId = slot,
                    AccountId = body.AccountID,
                    CharacterIndex = body.CharacterIndex
                };

                _connections[body.AccountID] = ServerState.CurrentConnection!;

                if (match.CurrentTurnPlayerId == 0)
                    match.CurrentTurnPlayerId = 1;
            }

            _playerRepo.InsertPlayer(body.RoomID, body.AccountID, body.CharacterIndex);
            _matchRepo.IncreasePlayerCount(body.RoomID);

            BroadcastRoom(
                match.MatchId,
                Wrap(MessageType.RoomUpdatedEvent, new RoomUpdatedEvent { RoomId = match.MatchId }, match.MatchId, null)
            );

            return Wrap(
                MessageType.JoinRoomResponse,
                new JoinRoomResponse { Success = true, IDPlayer = match.Players.Keys.Max() },
                match.MatchId,
                null
            );
        }

        private MessageEnvelope HandleLeaveRoom(MessageEnvelope req)
        {
            int matchId = req.MatchId!.Value;
            int playerId = req.PlayerId!.Value;

            if (!ServerState.Matches.TryGetValue(matchId, out var match))
                return MakeError("Không tìm thấy trận đấu.");

            match.Players.Remove(playerId);

            _playerRepo.DeletePlayer(matchId, playerId);
            _matchRepo.DecreasePlayerCount(matchId);

            if (match.Players.Count == 0)
            {
                _matchRepo.EndMatch(matchId);
                ServerState.Matches.Remove(matchId);
                return Wrap(
                    MessageType.PlayerLeftEvent,
                    new PlayerLeftEvent { PlayerId = playerId },
                    matchId,
                    null
                );
            }

            if (match.CurrentTurnPlayerId == playerId)
            {
                match.CurrentTurnPlayerId = match.Players.Keys.Min();
            }

            BroadcastRoom(
                matchId,
                Wrap(
                    MessageType.RoomUpdatedEvent,
                    new RoomUpdatedEvent { RoomId = matchId },
                    matchId,
                    null
                )
            );

            return Wrap(
                MessageType.PlayerLeftEvent,
                new PlayerLeftEvent { PlayerId = playerId },
                matchId,
                null
            );
        }
        #endregion

        private MessageEnvelope HandleStartMatch(MessageEnvelope req)
        {
            if (req.MatchId == null || req.PlayerId == null)
                return MakeError("Không thể bắt đầu trận đấu!");

            if (req.PlayerId != 1)
                return MakeError("Chỉ có chủ phòng mới có thể bắt đầu trận đấu!");

            var match = ServerState.Matches[req.MatchId.Value];

            match.IsMatch = 1;
            match.CurrentTurnPlayerId = match.Players.Keys.Min();

            _matchRepo.StartMatch(match.MatchId); 

            BroadcastRoom(
                match.MatchId,
                Wrap(
                    MessageType.StartMatchResponse,
                    new StartMatchResponse { MatchId = match.MatchId },
                    match.MatchId,
                    null
                )
            );

            BroadcastRoom(
                match.MatchId,
                Wrap(
                    MessageType.PlayerLeftEvent,
                    new PlayerLeftEvent { PlayerId = match.CurrentTurnPlayerId },
                    match.MatchId,
                    null
                )
            );

            return Wrap(
                MessageType.StartMatchResponse,
                new { Success = true },
                match.MatchId,
                null
            );
        }

        #region Broadcast Helpers
        private void BroadcastRoom(int matchId, MessageEnvelope env)
        {
            if (!ServerState.Matches.TryGetValue(matchId, out var match))
                return;

            foreach (var p in match.Players.Values)
            {
                if (_connections.TryGetValue(p.AccountId, out var conn))
                {
                    _ = conn.SendAsync(env);
                }
            }
        }
        
        private void BroadcastMoneyChanged(MatchState match, int playerId, int change, int currentMoney)
        {
            BroadcastRoom(match.MatchId, Wrap(
                MessageType.MoneyChangedEvent,
                new MoneyChangedEvent
                {
                    PlayerId = playerId,
                    CurrentMoney = currentMoney,
                    MoneyChange = change
                },
                match.MatchId,
                null
            ));
        }

        private void BroadcastDrawCard(MatchState match, int playerId, string cardType, int cardIndex, string description)
        {
            BroadcastRoom(match.MatchId, Wrap(
                MessageType.DrawCardEvent,
                new DrawCardEvent
                {
                    PlayerId = playerId,
                    CardType = cardType,
                    CardIndex = cardIndex,
                    Description = description
                },
                match.MatchId,
                null
            ));
        }

        private void BroadcastPropertyOwnershipChanged(MatchState match, PropertyState property)
        {
            string propertyType = property.type switch
            {
                PropertyType.Property => "Property",
                PropertyType.RailRoad => "RailRoad",
                PropertyType.Utility => "Utility",
                _ => "Unknown"
            };

            BroadcastRoom(match.MatchId, Wrap(
                MessageType.PropertyOwnershipChangedEvent,
                new PropertyOwnershipChangedEvent
                {
                    TileIndex = property.TileIndex,
                    OwnerId = property.PlayerOwnerId,
                    HouseCount = property.houseCount,
                    HasHotel = property.hasHotel,
                    PropertyType = propertyType
                },
                match.MatchId,
                null
            ));
        }

        private void BroadcastPlayerJailed(MatchState match, PlayerState player, string reason, int fromTile)
        {
            BroadcastRoom(match.MatchId, Wrap(
                MessageType.PlayerJailedEvent,
                new PlayerJailedEvent
                {
                    PlayerId = player.PlayerId,
                    Reason = reason,
                    FromTile = fromTile,
                    ToTile = JailTileIndex
                },
                match.MatchId,
                null
            ));
        }
        #endregion

        #region Game Handlers
        private MessageEnvelope HandleRollDice(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<RollDiceRequest>(req.Payload, JsonOpt)!;

            if (!ServerState.Matches.TryGetValue(body.MatchId, out var match))
                return MakeError("Không tìm thấy trận đấu");

            if (match.CurrentTurnPlayerId != body.PlayerId)
                return Error("Không phải lượt bạn");

            if (!match.Players.TryGetValue(body.PlayerId, out var player))
                return MakeError("Người chơi hiện không ở trong trận đấu");

            int dice1 = Random.Shared.Next(1, 7);
            int dice2 = Random.Shared.Next(1, 7);
            bool isDouble = dice1 == dice2;


            if (player.InJail)
            {
                player.JailTurnsRemaining--;

                Console.WriteLine($"[HandleRollDice] Player {player.PlayerId} in jail, turns remaining: {player.JailTurnsRemaining}, rolled: {dice1}, {dice2}");

                if (isDouble)
                {
                    player.InJail = false;
                    player.JailTurnsRemaining = 0;
                    SetStreak(match, player, 0);

                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PlayerReleasedFromJailEvent,
                        new PlayerReleasedFromJailEvent
                        {
                            PlayerId = player.PlayerId,
                            Method = "RollDice",
                            Message = $"Player {player.PlayerId} tung được xúc xắc đôi ({dice1}, {dice2}) và ra tù!"
                        },
                        match.MatchId, null));

                    int fromJ = player.Position;
                    int toJ = (fromJ + dice1 + dice2) % match.Board.Count;
                    player.Position = toJ;

                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PlayerMovedEvent,
                        new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = dice1, Roll2 = dice2 },
                        match.MatchId, null));

                    HandleTile(match, player, toJ, dice1, dice2);
                }
                else if (player.JailTurnsRemaining <= 0)
                {
                    player.InJail = false;
                    player.JailTurnsRemaining = 0;

                    int moneyBefore = player.Money;
                    player.Money -= 200;
                    BroadcastMoneyChanged(match, player.PlayerId, -200, player.Money);

                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PlayerReleasedFromJailEvent,
                        new PlayerReleasedFromJailEvent
                        {
                            PlayerId = player.PlayerId,
                            Method = "ForcedRelease",
                            Message = $"Player {player.PlayerId} đã hết 3 lượt trong tù, tự động ra và trả $200"
                        },
                        match.MatchId, null));

                    int fromJ = player.Position;
                    int toJ = (fromJ + dice1 + dice2) % match.Board.Count;
                    player.Position = toJ;

                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PlayerMovedEvent,
                        new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = dice1, Roll2 = dice2 },
                        match.MatchId, null));

                    HandleTile(match, player, toJ, dice1, dice2);
                }
                else
                {
                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PlayerMovedEvent,
                        new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = dice1, Roll2 = dice2 },
                        match.MatchId, null));

                    Console.WriteLine($"[HandleRollDice] Player {player.PlayerId} stays in jail, {player.JailTurnsRemaining} turns remaining");
                }

                return Wrap(MessageType.DiceRolledEvent, new { Success = true }, match.MatchId, body.PlayerId);
            }

            int streak = GetStreak(match, player);
            streak = isDouble ? streak + 1 : 0;
            SetStreak(match, player, streak);

            if (streak >= 3)
            {
                int fromPos = player.Position;
                SendToJail(match, player);

                BroadcastPlayerJailed(match, player, "ThreeDoubles", fromPos);

                BroadcastRoom(match.MatchId, Wrap(
                    MessageType.PlayerMovedEvent,
                    new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = dice1, Roll2 = dice2 },
                    match.MatchId, null));

                return Wrap(MessageType.DiceRolledEvent, new { Success = true }, match.MatchId, body.PlayerId);
            }

            int from = player.Position;
            int to = (from + dice1 + dice2) % match.Board.Count;
            
            bool passedGo = to < from; // Nếu vòng lại là đi qua GO
            
            player.Position = to;
            
            if (passedGo)
            {
                int moneyBefore = player.Money;
                player.Money += 200;
                BroadcastMoneyChanged(match, player.PlayerId, 200, player.Money);
                Console.WriteLine($"[HandleRollDice] Player {player.PlayerId} passed GO, received $200");
            }

            BroadcastRoom(match.MatchId, Wrap(
                MessageType.PlayerMovedEvent,
                new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = dice1, Roll2 = dice2 },
                match.MatchId, null));

            HandleTile(match, player, to, dice1, dice2);

            return Wrap(MessageType.DiceRolledEvent, new { Success = true }, match.MatchId, body.PlayerId);
        }

        private void HandleTile(MatchState match, PlayerState player, int tileIndex, int d1, int d2)
        {
            {
                // ✅ XÓA LOGIC BROADCAST Ở ĐÂY (vì sẽ broadcast ngay cả khi đi qua không dừng)
                // Logic broadcast sẽ được di chuyển vào HandleChance/HandleCommunityChest

                // ✅ XỬ LÝ LOGIC GAME
                flow.HandlePlayerLanded(match, player);

                AutoLiquidateToCoverDebt(match, player);

                Console.WriteLine($"Player {player.PlayerId} landed on tile {tileIndex} : {player.Money}");

                if (match.WaitingForBuyDecision && match.PendingTileIndex == tileIndex)
                {
                    if (!match.Properties.TryGetValue(tileIndex, out var property))
                        goto END_TURN;

                    if (!_connections.TryGetValue(player.AccountId, out var conn))
                        goto END_TURN;

                    bool isUpgrade = property.PlayerOwnerId == player.PlayerId &&
                        property.type == PropertyType.Property && !property.hasHotel;

                    int price;

                    if (isUpgrade)
                    {
                        price = property.houseCount < 4
                            ? property.housePrice
                            : property.hotelPrice;
                    }
                    else
                    {
                        price = property.type switch
                        {
                            PropertyType.Property => property.landPrice,
                            PropertyType.RailRoad => property.RailRoadBuyPrice,
                            PropertyType.Utility => property.UtilityBuyPrice,
                            _ => 0
                        };
                    }

                    _ = conn.SendAsync(
                        Wrap(
                            MessageType.AskBuyPropertyEvent,
                            new AskBuyPropertyEvent
                            {
                                TileIndex = tileIndex,
                                Name = ServerState.Board[tileIndex].name,
                                Price = price,
                                IsAuction = isUpgrade 
                            },
                            match.MatchId,
                            player.PlayerId
                        )
                    );

                    return;
                }

            END_TURN:
                return;
            }
        }

        private MessageEnvelope HandlePlayerSurrender(MessageEnvelope req)
        {
            int matchId = req.MatchId!.Value;
            int playerId = req.PlayerId!.Value;

            if (!ServerState.Matches.TryGetValue(matchId, out var match))
                return MakeError("Match not found");

            if (!match.Players.TryGetValue(playerId, out var player))
                return MakeError("Player not found");

            lock (GetMatchLock(matchId))
            {
                player.IsBankrupt = true;
                player.Money = 0;

                foreach (var prop in match.Properties.Values)
                {
                    if (prop.PlayerOwnerId == playerId)
                    {
                        prop.PlayerOwnerId = null;
                        prop.houseCount = 0;
                        prop.hasHotel = false;
                    }
                }

                if (match.CurrentTurnPlayerId == playerId)
                {
                    flow.NextTurn(match);
                }
            }

            BroadcastRoom(
                matchId,
                Wrap(
                    MessageType.PlayerSurrenderEvent,
                    new PlayerSurrenderEvent { PlayerId = playerId },
                    matchId,
                    null
                )
            );

            return Wrap(
                MessageType.PlayerSurrenderEvent,
                new PlayerSurrenderEvent { PlayerId = playerId },
                matchId,
                playerId
            );
        }

        private MessageEnvelope HandleBuyDecision(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<BuyDecisionRequest>(req.Payload, JsonOpt)!;

            if (!req.MatchId.HasValue || !req.PlayerId.HasValue)
                return MakeError("Invalid BuyDecision request");

            if (!ServerState.Matches.TryGetValue(req.MatchId.Value, out var match))
                return MakeError("Match not found");

            if (!match.Players.TryGetValue(req.PlayerId.Value, out var player))
                return MakeError("Player not found");

            if (!body.Accept)
            {
                match.WaitingForBuyDecision = false;
                match.PendingTileIndex = null;

                return Wrap(
                    MessageType.PropertyUpdatedEvent,
                    new PropertyUpdatedEvent 
                    { 
                        Success = false,
                        Message = "Đã từ chối mua/nâng cấp"
                    },
                    match.MatchId,
                    req.PlayerId
                );
            }

            int moneyBefore = player.Money;

            // ✅ LƯU PENDING TILE INDEX TRƯỚC KHI RESET
            int? pendingTile = match.PendingTileIndex;
            
            // ✅ CHECK TIỀN TRƯỚC KHI MUA
            if (pendingTile.HasValue && match.Properties.TryGetValue(pendingTile.Value, out var property))
            {
                int requiredMoney = 0;
                string actionName = "";
                
                if (property.PlayerOwnerId == null)
                {
                    // Mua đất mới
                    requiredMoney = property.type switch
                    {
                        PropertyType.Property => property.landPrice,
                        PropertyType.RailRoad => property.RailRoadBuyPrice,
                        PropertyType.Utility => property.UtilityBuyPrice,
                        _ => 0
                    };
                    actionName = "mua";
                }
                else if (property.PlayerOwnerId == player.PlayerId && property.type == PropertyType.Property && !property.hasHotel)
                {
                    // Nâng cấp
                    requiredMoney = property.houseCount < 4 ? property.housePrice : property.hotelPrice;
                    actionName = property.houseCount < 4 ? "xây nhà" : "xây khách sạn";
                }
                
                if (player.Money < requiredMoney)
                {
                    match.WaitingForBuyDecision = false;
                    match.PendingTileIndex = null;
                    
                    return Wrap(
                        MessageType.PropertyUpdatedEvent,
                        new PropertyUpdatedEvent
                        {
                            Success = false,
                            PropertyTileIndex = pendingTile,
                            PlayerId = player.PlayerId,
                            Message = $"Không đủ tiền để {actionName}!\nCần: ${requiredMoney}\nCó: ${player.Money}\nThiếu: ${requiredMoney - player.Money}"
                        },
                        match.MatchId,
                        req.PlayerId
                    );
                }
            }

            bool bought = flow.BuyTile(match, player);

            match.WaitingForBuyDecision = false;
            match.PendingTileIndex = null;

            if (!bought)
            {
                return Wrap(
                    MessageType.PropertyUpdatedEvent,
                    new PropertyUpdatedEvent
                    {
                        Success = false,
                        PropertyTileIndex = pendingTile,
                        PlayerId = player.PlayerId,
                        Message = "Không thể mua/nâng cấp tài sản này!"
                    },
                    match.MatchId,
                    req.PlayerId
                );
            }

            int moneyChange = player.Money - moneyBefore;
            BroadcastMoneyChanged(match, player.PlayerId, moneyChange, player.Money);

            // ✅ LẤY PROPERTY THEO PENDING TILE INDEX
            if (pendingTile.HasValue && 
                match.Properties.TryGetValue(pendingTile.Value, out var boughtProperty))
            {
                BroadcastPropertyOwnershipChanged(match, boughtProperty);
            }

            BroadcastRoom(
                match.MatchId,
                Wrap(
                    MessageType.PropertyUpdatedEvent,
                    new PropertyUpdatedEvent
                    {
                        Success = true,
                        PropertyTileIndex = pendingTile ?? player.Position,
                        PlayerId = player.PlayerId,
                        Message = "Mua/nâng cấp thành công!"
                    },
                    match.MatchId,
                    null
                )
            );

            return Wrap(
                MessageType.PropertyUpdatedEvent,
                new PropertyUpdatedEvent 
                { 
                    Success = true,
                    Message = "Mua/nâng cấp thành công!"
                },
                match.MatchId,
                req.PlayerId
            );
        }

        private void HandleChanceCard(MatchState match, PlayerState player)
        {
            var card = DrawChanceCard(match);

            if (match.NextChanceCardIndex == -1)
            {
                match.NextChanceCardIndex = Random.Shared.Next(1, 17);
            }
            int cardIndex = match.NextChanceCardIndex;
            
            BroadcastDrawCard(match, player.PlayerId, "Chance", cardIndex, card.Description);

            match.NextChanceCardIndex = (cardIndex % 16) + 1;

            switch (card.ChanceType)
            {
                case ChanceCardType.GetOutOfJailFree:
                    player.hasGetOutOfJailCard = true;
                    break;

                case ChanceCardType.StreetRepairs:
                    {
                        int cost = 0;
                        foreach (var prop in match.Properties.Values)
                        {
                            if (prop.PlayerOwnerId == player.PlayerId)
                            {
                                cost += prop.houseCount * 40;
                                if (prop.hasHotel) cost += 115;
                            }
                        }
                        player.Money -= cost;
                        break;
                    }

                case ChanceCardType.PayMoney:
                    player.Money -= card.Amount;
                    break;

                case ChanceCardType.EarnMoney:
                    player.Money += card.Amount;
                    break;

                case ChanceCardType.PayEachPlayer:
                    {
                        foreach (var other in match.Players.Values)
                        {
                            if (other.PlayerId == player.PlayerId || other.IsBankrupt) continue;
                            player.Money -= card.Amount;
                            other.Money += card.Amount;
                        }
                        break;
                    }

                case ChanceCardType.MoveBackSpaces:
                    {
                        int to = (player.Position - card.MoveBackSteps + match.Board.Count) % match.Board.Count;
                        player.Position = to;

                        BroadcastRoom(match.MatchId, Wrap(
                            MessageType.PlayerMovedEvent,
                            new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = 0, Roll2 = 0 },
                            match.MatchId, null));

                        HandleTile(match, player, to, 0, 0);
                        return;
                    }

                case ChanceCardType.MoveToTile:
                    {
                        player.Position = card.MoveToTileIndex;

                        BroadcastRoom(match.MatchId, Wrap(
                            MessageType.PlayerMovedEvent,
                            new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = 0, Roll2 = 0 },
                            match.MatchId, null));

                        HandleTile(match, player, card.MoveToTileIndex, 0, 0);
                        return;
                    }

                case ChanceCardType.MoveToNearestUtility:
                    {
                        int target = match.Properties
                            .Where(p => p.Value.type == PropertyType.Utility)
                            .Select(p => p.Key)
                            .Where(idx => idx > player.Position)
                            .DefaultIfEmpty(
                                match.Properties
                                    .Where(p => p.Value.type == PropertyType.Utility)
                                    .Select(p => p.Key)
                                    .Min()
                            )
                            .First();

                        player.Position = target;

                        BroadcastRoom(match.MatchId, Wrap(
                            MessageType.PlayerMovedEvent,
                            new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = 0, Roll2 = 0 },
                            match.MatchId, null));

                        HandleTile(match, player, target, 0, 0);
                        return;
                    }

                case ChanceCardType.MoveToNearestRailroad:
                    {
                        int target = match.Properties
                            .Where(p => p.Value.type == PropertyType.RailRoad)
                            .Select(p => p.Key)
                            .Where(idx => idx > player.Position)
                            .DefaultIfEmpty(
                                match.Properties
                                    .Where(p => p.Value.type == PropertyType.RailRoad)
                                    .Select(p => p.Key)
                                    .Min()
                            )
                            .First();

                        player.Position = target;

                        BroadcastRoom(match.MatchId, Wrap(
                            MessageType.PlayerMovedEvent,
                            new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = 0, Roll2 = 0 },
                            match.MatchId, null));

                        HandleTile(match, player, target, 0, 0);
                        return;
                    }

                case ChanceCardType.GoToJail:
                    SendToJail(match, player);
                    break;
            }

            AutoLiquidateToCoverDebt(match, player);
        }

        private void HandleCommunityChestCard(MatchState match, PlayerState player)
        {
            var card = DrawCommunityChestCard(match);

            if (match.NextCommunityChestCardIndex == -1)
            {
                match.NextCommunityChestCardIndex = Random.Shared.Next(1, 17);
            }
            int cardIndex = match.NextCommunityChestCardIndex;
            
            BroadcastDrawCard(match, player.PlayerId, "CommunityChest", cardIndex, card.Description);

            match.NextCommunityChestCardIndex = (cardIndex % 16) + 1;

            switch (card.ChestType)
            {
                case CommunityChestCardType.GetOutOfJailFree:
                    player.hasGetOutOfJailCard = true;
                    break;

                case CommunityChestCardType.EarnMoney:
                    player.Money += card.Amount;
                    break;

                case CommunityChestCardType.PayMoney:
                    player.Money -= card.Amount;
                    break;

                case CommunityChestCardType.CollectFromEachPlayer:
                    foreach (var other in match.Players.Values)
                    {
                        if (other.PlayerId == player.PlayerId || other.IsBankrupt) continue;
                        other.Money -= card.Amount;
                        player.Money += card.Amount;

                        // nếu muốn chặt chẽ: gọi AutoLiquidate cho "other" nếu họ âm tiền
                        AutoLiquidateToCoverDebt(match, other);
                    }
                    break;

                case CommunityChestCardType.MoveToTile:
                    player.Position = card.MoveToTileIndex;

                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PlayerMovedEvent,
                        new PlayerMoveEvent { PlayerId = player.PlayerId, Roll1 = 0, Roll2 = 0 },
                        match.MatchId, null));

                    HandleTile(match, player, card.MoveToTileIndex, 0, 0);
                    return;

                case CommunityChestCardType.StreetRepairs:
                    {
                        int cost = 0;
                        foreach (var prop in match.Properties.Values)
                        {
                            if (prop.PlayerOwnerId == player.PlayerId)
                            {
                                cost += prop.houseCount * 40;
                                if (prop.hasHotel) cost += 115;
                            }
                        }
                        player.Money -= cost;
                        break;
                    }

                case CommunityChestCardType.GoToJail:
                    SendToJail(match, player);
                    break;
            }

            AutoLiquidateToCoverDebt(match, player);
        }


        private void AutoLiquidateToCoverDebt(MatchState match, PlayerState player)
        {
            if (player.Money >= 0) return;

            var owned = match.Properties
                .Where(kv => kv.Value.PlayerOwnerId == player.PlayerId)
                .Select(kv => kv.Value)
                .ToList();

            foreach (var prop in owned)
            {
                if (player.Money >= 0) break;

                if (prop.hasHotel)
                {
                    prop.hasHotel = false;
                    prop.houseCount = Math.Min(prop.houseCount, 4);

                    player.Money += prop.hotelPrice / 2;

                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PropertyUpdatedEvent,
                        new PropertyUpdatedEvent { PropertyTileIndex = prop.TileIndex, PlayerId = (int)prop.PlayerOwnerId },
                        match.MatchId, null));

                    // ✅ BROADCAST OWNERSHIP CHANGE
                    BroadcastPropertyOwnershipChanged(match, prop);
                }
            }

            foreach (var prop in owned)
            {
                while (prop.houseCount > 0 && player.Money < 0)
                {
                    prop.houseCount--;
                    player.Money += prop.housePrice / 2;

                    BroadcastRoom(match.MatchId, Wrap(
                        MessageType.PropertyUpdatedEvent,
                        new PropertyUpdatedEvent { PropertyTileIndex = prop.TileIndex, PlayerId = (int)prop.PlayerOwnerId },
                        match.MatchId, null));

                    // ✅ BROADCAST OWNERSHIP CHANGE
                    BroadcastPropertyOwnershipChanged(match, prop);
                }
            }

            foreach (var prop in owned)
            {
                if (player.Money >= 0) break;

                if (prop.PlayerOwnerId != player.PlayerId) continue;

                int sellValue = prop.type switch
                {
                    PropertyType.Property => prop.landPrice / 2,
                    PropertyType.RailRoad => prop.RailRoadBuyPrice / 2,
                    PropertyType.Utility => prop.UtilityBuyPrice / 2,
                    _ => 0
                };

                prop.PlayerOwnerId = 0;
                prop.houseCount = 0;
                prop.hasHotel = false;

                player.Money += sellValue;

                BroadcastRoom(match.MatchId, Wrap(
                    MessageType.PropertyUpdatedEvent,
                    new PropertyUpdatedEvent { PropertyTileIndex = prop.TileIndex, PlayerId = 0 },
                    match.MatchId, null));

                // ✅ BROADCAST OWNERSHIP CHANGE (property bị bán)
                BroadcastPropertyOwnershipChanged(match, prop);
            }

            // 4) vẫn âm => phá sản
            if (player.Money < 0)
            {
                player.IsBankrupt = true;
                // (tuỳ bạn) có thể broadcast 1 event phá sản sau này
            }
        }


        private void FinishTurn(MatchState match, int d1, int d2)
        {
            if ((d1 == 1 && d2 == 1) || (d1 == 6 && d2 == 6)) return;

            match.CurrentTurnPlayerId = match.Players.Keys
                .Where(id => id > match.CurrentTurnPlayerId)
                .DefaultIfEmpty(match.Players.Keys.Min())
                .First();

            BroadcastRoom(
                match.MatchId,
                Wrap(
                    MessageType.PlayerLeftEvent,
                    new PlayerLeftEvent
                    {
                        PlayerId = match.CurrentTurnPlayerId
                    },
                    match.MatchId,
                    null
                )
            );
        }


        private MessageEnvelope HandleEndTurn(MessageEnvelope req)
        {
            if (!req.MatchId.HasValue || !req.PlayerId.HasValue)
                return MakeError("Invalid EndTurn");

            if (!ServerState.Matches.TryGetValue(req.MatchId.Value, out var match))
                return MakeError("Match not found");

            if (match.CurrentTurnPlayerId != req.PlayerId.Value)
                return MakeError("Not your turn");


            match.WaitingForBuyDecision = false;
            match.PendingTileIndex = null;

            FinishTurn(match, 0, 0);

            return Wrap(
                MessageType.EndTurnReponse,
                new { Success = true },
                match.MatchId,
                req.PlayerId
            );
        }
        #endregion

        #region Helpers
        private static MessageEnvelope Wrap<T>(
            MessageType type,
            T body,
            int? matchId = null,
            int? playerId = null)
            => new MessageEnvelope
            {
                MessageId = Guid.NewGuid(),
                Type = type,
                MatchId = matchId,
                PlayerId = playerId,
                Payload = JsonSerializer.Serialize(body, JsonOpt)
            };

        private static MessageEnvelope MakeError(string msg)
            => new MessageEnvelope
            {
                MessageId = Guid.NewGuid(),
                Type = MessageType.ErrorResponse,
                Payload = $"{{\"message\":\"{msg}\"}}"
            };

        private MessageEnvelope Error(string message)
        {
            return new MessageEnvelope(
                MessageType.ErrorResponse,
                JsonSerializer.Serialize(new { Message = message })
            );
        }
        #endregion

        #region OTP and Hash Handlers
        private static string NormalizeToSha256Hex(string input)
            => IsHexSha256(input) ? input.ToLowerInvariant() : Sha256Hex(input);

        private static bool IsHexSha256(string s)
            => !string.IsNullOrEmpty(s) && s.Length == 64 &&
               Regex.IsMatch(s, "^[0-9a-fA-F]{64}$");

        private static string Sha256Hex(string raw)
        {
            using var sha = SHA256.Create();
            return string.Concat(
                sha.ComputeHash(Encoding.UTF8.GetBytes(raw))
                   .Select(b => b.ToString("x2")));
        }

        private static readonly Dictionary<string, (string otp, DateTime exp, bool verified)> _otp = new();

        private static void GenerateOtp(string email)
        {
            _otp[email] = (new Random().Next(100000, 999999).ToString(),
                DateTime.UtcNow.AddMinutes(2), false);
        }

        private static bool VerifyOtp(string email, string input)
        {
            if (!_otp.TryGetValue(email, out var s)) return false;
            if (s.exp < DateTime.UtcNow) return false;
            if (s.otp != input) return false;
            _otp[email] = (s.otp, s.exp, true);
            return true;
        }
        #endregion

        #region Chat Handlers
        private MessageEnvelope HandleSendChatMessage(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<SendChatMessageRequest>(req.Payload, JsonOpt)!;

            if (!ServerState.Matches.TryGetValue(body.MatchId, out var match))
                return MakeError("Match not found");

            if (!match.Players.TryGetValue(body.PlayerId, out var player))
                return MakeError("Player not found");

            var account = _accountRepo.GetById(player.AccountId);
            string playerName = account?.Username ?? $"Player {body.PlayerId}";

            foreach (var p in match.Players.Values)
            {
                if (p.PlayerId == body.PlayerId)
                    continue;
                
                if (_connections.TryGetValue(p.AccountId, out var conn))
                {
                    _ = conn.SendAsync(
                        Wrap(
                            MessageType.ChatMessageEvent,
                            new ChatMessageEvent
                            {
                                PlayerId = body.PlayerId,
                                PlayerName = playerName,
                                Message = body.Message
                            },
                            match.MatchId,
                            null
                        )
                    );
                }
            }

            return Wrap(
                MessageType.ChatMessageEvent,
                new { Success = true },
                match.MatchId,
                body.PlayerId
            );
        }
        #endregion
    }
}