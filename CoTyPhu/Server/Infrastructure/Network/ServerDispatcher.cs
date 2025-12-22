using Common.Constracts;
using Common.Constracts.Room;
using Common.Contracts.Auth;
using Common.Contracts.Game;
using Common.Contracts.Room;
using Common.Domain.Models.Entities;
using Server.Domain;
using Server.Domain.GameState;
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

        public ServerDispatcher()
        {
            var db = new DBConnection();
            _accountRepo = new AccountRepo(db);
            _matchRepo = new MatchRepo(db);
            _playerRepo = new PlayerRepo(db);
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

                _ => Task.FromResult(MakeError("No handler for message type"))
            };
        }

        // ================== AUTH ==================
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
                return MakeError("Create room failed");

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

        private MessageEnvelope HandleJoinRoom(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<JoinRoomRequest>(req.Payload, JsonOpt)!;

            if (!ServerState.Matches.TryGetValue(body.RoomID, out var match))
                return Wrap(MessageType.JoinRoomResponse, new JoinRoomResponse { Success = false });

            if (match.IsMatch != 0)
                return Wrap(MessageType.JoinRoomResponse, new JoinRoomResponse { Success = false });

            if (match.Players.Values.Any(p => p.CharacterIndex == body.CharacterIndex))
                return Wrap(MessageType.JoinRoomResponse, new JoinRoomResponse { Success = false });

            int slot = Enumerable.Range(1, 4)
                .FirstOrDefault(i => !match.Players.ContainsKey(i));

            if (slot == 0)
                return Wrap(MessageType.JoinRoomResponse, new JoinRoomResponse { Success = false });

            match.Players[slot] = new PlayerState
            {
                PlayerId = slot,
                AccountId = body.AccountID,
                CharacterIndex = body.CharacterIndex
            };

            _connections[body.AccountID] = ServerState.CurrentConnection!;

            _playerRepo.InsertPlayer(body.RoomID, body.AccountID, body.CharacterIndex);
            _matchRepo.IncreasePlayerCount(body.RoomID);

            if (match.CurrentTurnPlayerId == 0)
                match.CurrentTurnPlayerId = 1;

            BroadcastRoom(
                match.MatchId,
                Wrap(
                    MessageType.RoomUpdatedEvent,
                    new RoomUpdatedEvent { RoomId = match.MatchId },
                    match.MatchId,
                    null
                )
            );

            return Wrap(
                MessageType.JoinRoomResponse,
                new JoinRoomResponse { Success = true, IDPlayer = slot },
                match.MatchId,
                slot
            );
        }


        private MessageEnvelope HandleLeaveRoom(MessageEnvelope req)
        {
            int matchId = req.MatchId!.Value;
            int playerId = req.PlayerId!.Value;

            if (!ServerState.Matches.TryGetValue(matchId, out var match))
                return MakeError("Match not found");

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
                return MakeError("Invalid start");

            if (req.PlayerId != 1)
                return MakeError("Only host can start");

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

            return Wrap(
                MessageType.StartMatchResponse,
                new { Success = true },
                match.MatchId,
                null
            );
        }


        // ================== BROADCAST ==================
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
        #endregion

        // ================== OTP + HASH ==================
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
    }
}

