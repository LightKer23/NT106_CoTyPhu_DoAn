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
using System;
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

        public ServerDispatcher()
        {
            var db = new DBConnection();
            _accountRepo = new AccountRepo(db);
            _matchRepo = new MatchRepo(db);
        }

        // ================== DISPATCH ==================
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
        private MessageEnvelope HandleLogin(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<LoginRequest>(req.Payload, JsonOpt)!;
            string hash = NormalizeToSha256Hex(body.Password);

            bool ok = _accountRepo.CheckLogin(body.Username, hash);
            int? id = ok ? _accountRepo.GetIdByLogin(body.Username, hash) : null;

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

        // ================== ROOM ==================
        private MessageEnvelope HandleCreateRoom(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<CreateRoomRequest>(req.Payload, JsonOpt)!;

            // 1️⃣ SQL sinh RoomID
            int matchId = _matchRepo.CreateMatch();
            if (matchId <= 0)
                return MakeError("Create match failed");

            // 2️⃣ Tạo MatchState trong RAM
            var match = new MatchState
            {
                MatchId = matchId,
                IsMatch = 0,
                CurrentTurnPlayerId = 1 // host
            };

            match.Players[1] = new PlayerState
            {
                PlayerId = 1,
                AccountId = body.AccountID
            };

            ServerState.Matches[matchId] = match;

            // 3️⃣ Trả về client
            return Wrap(
                MessageType.CreateRoomResponse,
                new CreateRoomResponse
                {
                    Success = true,
                    RoomID = matchId
                },
                matchId,
                1);
        }

        private MessageEnvelope HandleSearchRoom(MessageEnvelope req)
        {
            if (req.MatchId == null ||
                !ServerState.Matches.TryGetValue(req.MatchId.Value, out var match))
                return Wrap(MessageType.SearchRoomResponse,
                    new SearchRoomResponse { Success = false });

            return Wrap(
                MessageType.SearchRoomResponse,
                new SearchRoomResponse
                {
                    Success = true,
                    RoomId = match.MatchId,
                    PlayerRooms = match.Players.Values
                        .Select(p => p.CharacterIndex)
                        .ToList()
                },
                match.MatchId,
                null);
        }

        private MessageEnvelope HandleJoinRoom(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<JoinRoomRequest>(req.Payload, JsonOpt)!;

            if (!ServerState.Matches.TryGetValue(body.RoomID, out var match) ||
                match.IsMatch != 0)
                return Wrap(MessageType.JoinRoomResponse,
                    new JoinRoomResponse { Success = false });

            int slot = Enumerable.Range(1, 4)
                .FirstOrDefault(i => !match.Players.ContainsKey(i));

            if (slot == 0)
                return Wrap(MessageType.JoinRoomResponse,
                    new JoinRoomResponse { Success = false });

            match.Players[slot] = new PlayerState
            {
                PlayerId = slot,
                AccountId = body.AccountID,
                CharacterIndex = body.CharacterIndex
            };

            _matchRepo.IncreasePlayerCount(match.MatchId);

            return Wrap(
                MessageType.JoinRoomResponse,
                new JoinRoomResponse { Success = true },
                match.MatchId,
                slot);
        }

        private MessageEnvelope HandleLeaveRoom(MessageEnvelope req)
        {
            if (req.MatchId == null || req.PlayerId == null)
                return MakeError("Invalid leave");

            var match = ServerState.Matches[req.MatchId.Value];
            int leaving = req.PlayerId.Value;

            match.Players.Remove(leaving);
            _matchRepo.DecreasePlayerCount(match.MatchId);

            if (match.Players.Count == 0)
            {
                _matchRepo.EndMatch(match.MatchId);
                ServerState.Matches.Remove(match.MatchId);
                return MakeError("Room closed");
            }

            if (match.CurrentTurnPlayerId == leaving)
                match.CurrentTurnPlayerId = match.Players.Keys.Min();

            return Wrap(
                MessageType.PlayerLeftEvent,
                new PlayerLeftEvent { PlayerId = leaving },
                match.MatchId,
                null);
        }

        private MessageEnvelope HandleStartMatch(MessageEnvelope req)
        {
            if (req.MatchId == null || req.PlayerId == null)
                return MakeError("Invalid start");

            var match = ServerState.Matches[req.MatchId.Value];

            // chỉ host (slot 1) được start
            if (req.PlayerId != 1)
                return MakeError("Only host can start");

            match.IsMatch = 1;
            match.CurrentTurnPlayerId = match.Players.Keys.Min();

            _matchRepo.StartMatch(match.MatchId);

            return Wrap(
                MessageType.StartMatchResponse,
                new { Success = true },
                match.MatchId,
                null);
        }

        // ================== HELPERS ==================
        private static MessageEnvelope Wrap<T>(
            MessageType type,
            T body,
            int? matchId = null,
            int? playerId = null)
            => new MessageEnvelope
            {
                Type = type,
                MatchId = matchId,
                PlayerId = playerId,
                Payload = JsonSerializer.Serialize(body, JsonOpt)
            };

        private static MessageEnvelope MakeError(string msg)
            => new MessageEnvelope
            {
                Type = MessageType.ErrorResponse,
                Payload = $"{{\"message\":\"{msg}\"}}"
            };

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

        private static readonly System.Collections.Generic.Dictionary<string, (string otp, DateTime exp, bool verified)> _otp = new();

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
