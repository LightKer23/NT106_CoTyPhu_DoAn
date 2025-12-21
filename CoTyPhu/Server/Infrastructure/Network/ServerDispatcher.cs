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

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;


namespace Server.Infrastructure.Network
{
    public sealed class ServerDispatcher : IRequestDispatcher
    {
        private static readonly JsonSerializerOptions JsonOpt = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly AccountRepo _accountRepo;

        public ServerDispatcher()
        {
            var db = new DBConnection();
            _accountRepo = new AccountRepo(db);
        }

        // ================== DISPATCH ==================
        public Task<MessageEnvelope> DispatchAsync(MessageEnvelope req)
        {
            return req.Type switch
            {
                MessageType.LoginRequest => Task.FromResult(HandleLogin(req)),
                MessageType.RegisterRequest => Task.FromResult(HandleRegister(req)),
                MessageType.ForgotPasswordRequest => HandleForgotPassword(req),
                MessageType.VerifyOTPRequest => Task.FromResult(HandleVerifyOtp(req)),
                MessageType.ResetPasswordRequest => Task.FromResult(HandleResetPassword(req)),

                MessageType.CreateRoomRequest => Task.FromResult(HandleCreateRoom(req)),
                MessageType.JoinRoomRequest => Task.FromResult(HandleJoinRoom(req)),
                MessageType.SearchRoomRequest => Task.FromResult(HandleSearchRoom(req)),

                MessageType.RollDiceRequest => Task.FromResult(HandleRollDice(req)),
                MessageType.BuyDecisionRequest => Task.FromResult(HandleBuyDecision(req)),
                MessageType.GetOutOfJailRequest => Task.FromResult(HandleGetOutOfJail(req)),
                MessageType.SellPropertyRequest => Task.FromResult(HandleSellProperty(req)),
                MessageType.UpgradePropertyRequest => Task.FromResult(HandleUpgradeProperty(req)),
                MessageType.EndTurnRequest => Task.FromResult(HandleEndTurn(req)),
                MessageType.LeaveMatchRequest => Task.FromResult(HandleLeaveMatch(req)),

                _ => Task.FromResult(MakeError("No handler for message type"))
            };
        }

        // ================== AUTH ==================
        private MessageEnvelope HandleLogin(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<LoginRequest>(req.Payload, JsonOpt)!;

            string passwordHash = NormalizeToSha256Hex(body.Password);
            bool ok = _accountRepo.CheckLogin(body.Username, passwordHash);

            int? idAcc = ok
                ? _accountRepo.GetIdByLogin(body.Username, passwordHash)
                : null;

            return Wrap(
                MessageType.LoginResponse,
                new LoginResponse
                {
                    Success = ok,
                    Message = ok ? "Đăng nhập thành công" : "Sai tài khoản hoặc mật khẩu",
                    IDAccount = idAcc
                }
            );
        }

        private MessageEnvelope HandleRegister(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<RegisterRequest>(req.Payload, JsonOpt)!;

            if (string.IsNullOrWhiteSpace(body.Username) ||
                string.IsNullOrWhiteSpace(body.Password) ||
                string.IsNullOrWhiteSpace(body.Email))
            {
                return Wrap(MessageType.RegisterResponse,
                    new RegisterResponse { Success = false, Message = "Vui lòng nhập đầy đủ thông tin" });
            }

            if (_accountRepo.CheckUsername(body.Username))
                return Wrap(MessageType.RegisterResponse,
                    new RegisterResponse { Success = false, Message = "Username đã tồn tại" });

            if (_accountRepo.CheckEmail(body.Email))
                return Wrap(MessageType.RegisterResponse,
                    new RegisterResponse { Success = false, Message = "Email đã tồn tại" });

            string hash = NormalizeToSha256Hex(body.Password);

            var acc = new Account
            {
                Username = body.Username,
                PasswordHash = hash,
                Email = body.Email,
                DisplayName = string.IsNullOrWhiteSpace(body.DisplayName) ? null : body.DisplayName
            };

            int id = _accountRepo.Insert(acc);

            return Wrap(
                MessageType.RegisterResponse,
                new RegisterResponse
                {
                    Success = id > 0,
                    Message = id > 0 ? "Đăng ký thành công" : "Đăng ký thất bại"
                }
            );
        }

        private Task<MessageEnvelope> HandleForgotPassword(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<ForgotPasswordRequest>(req.Payload, JsonOpt)!;

            if (!_accountRepo.CheckEmail(body.Email))
            {
                return Task.FromResult(
                    Wrap(MessageType.ForgotPasswordResponse,
                        new ForgotPasswordResponse { Success = false, Message = "Email không tồn tại" })
                );
            }

            string otp = GenerateOtp(body.Email);

            _ = Task.Run(async () =>
            {
                try
                {
                    await SendOtpMailAsync(body.Email, otp);
                    Console.WriteLine($"[MAIL] Sent OTP to {body.Email}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[MAIL ERROR] " + ex.Message);
                }
            });

            return Task.FromResult(
                Wrap(MessageType.ForgotPasswordResponse,
                    new ForgotPasswordResponse { Success = true, Message = "OTP đang được gửi về email" })
            );
        }



        private MessageEnvelope HandleVerifyOtp(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<VerifyOTPRequest>(req.Payload, JsonOpt)!;

            bool ok = VerifyOtp(body.Email, body.OTP);

            return Wrap(
                MessageType.VerifyOTPResponse,
                new VerifyOTPResponse
                {
                    Success = ok,
                    Message = ok ? "OTP hợp lệ" : "OTP không hợp lệ hoặc hết hạn"
                }
            );
        }

        private MessageEnvelope HandleResetPassword(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<ResetPasswordRequest>(req.Payload, JsonOpt)!;

            if (!CanResetPassword(body.Email))
            {
                return Wrap(MessageType.ResetPasswordResponse,
                    new ResetPasswordResponse { Success = false, Message = "OTP chưa xác thực" });
            }

            bool ok = _accountRepo.ChangePasswordByEmail(
                body.Email,
                NormalizeToSha256Hex(body.NewPassword));

            if (ok) ClearOtp(body.Email);

            return Wrap(
                MessageType.ResetPasswordResponse,
                new ResetPasswordResponse
                {
                    Success = ok,
                    Message = ok ? "Đổi mật khẩu thành công" : "Đổi mật khẩu thất bại"
                }
            );
        }

       //================== ROOM ==================
        private static int _roomAutoId = 1;

        private MessageEnvelope HandleCreateRoom(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<CreateRoomRequest>(req.Payload, JsonOpt)!;

            int matchId = _roomAutoId++;

            ServerState.Matches[matchId] = new MatchState(matchId, new Dictionary<int, PlayerState>(), new Dictionary<int, PropertyState>())
            {
                IsMatch = 0
            };


            return Wrap(
                MessageType.CreateRoomResponse,
                new CreateRoomResponse
                {
                    Success = true,
                    RoomID = matchId,
                    Message = "Create room success"
                },
                matchId,
                body.AccountID
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

            var taken = match.Players.Values.Select(p => p.CharacterIndex).ToList();

            return Wrap(
                MessageType.SearchRoomResponse,
                new SearchRoomResponse
                {
                    Success = true,
                    RoomId = match.MatchId,
                    PlayerRooms = taken
                },
                match.MatchId,
                req.PlayerId
            );
        }

        private MessageEnvelope HandleJoinRoom(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<JoinRoomRequest>(req.Payload, JsonOpt)!;

            if (!ServerState.Matches.TryGetValue(body.RoomID, out var match) ||
                match.IsMatch != 0 ||
                match.Players.ContainsKey(body.AccountID))
            {
                return Wrap(MessageType.JoinRoomResponse,
                    new JoinRoomResponse { Success = false });
            }

            match.Players[body.AccountID] = new PlayerState
            {
                PlayerId = body.AccountID,
                CharacterIndex = body.CharacterIndex
            };

            var players = match.Players.Values.Select(p => new Player
            {
                IDAccount = p.PlayerId,
                IDMatch = match.MatchId
            }).ToList();

            return Wrap(
                MessageType.JoinRoomResponse,
                new JoinRoomResponse { Success = true, Players = players },
                match.MatchId,
                body.AccountID
            );
        }

        // ================== GAME ==================
        private MessageEnvelope HandleRollDice(MessageEnvelope req)
        {
            return Wrap(
                MessageType.PlayerReleasedFromJailEvent,
                new PlayerReleasedFromJailEvent { PlayerId = req.PlayerId ?? 0 },
                req.MatchId,
                req.PlayerId
            );
        }

        private MessageEnvelope HandleGetOutOfJail(MessageEnvelope req)
            => HandleRollDice(req);

        private MessageEnvelope HandleSellProperty(MessageEnvelope req)
            => Wrap(MessageType.MoneyChangedEvent,
                new MoneyChangedEvent { PlayerId = req.PlayerId ?? 0, Amount = 150 },
                req.MatchId, req.PlayerId);

        private MessageEnvelope HandleUpgradeProperty(MessageEnvelope req)
            => Wrap(MessageType.MoneyChangedEvent,
                new MoneyChangedEvent { PlayerId = req.PlayerId ?? 0, Amount = -100 },
                req.MatchId, req.PlayerId);

        private MessageEnvelope HandleEndTurn(MessageEnvelope req)
            => Wrap(MessageType.TurnResultEvent,
                new TurnResultEvent { PlayerID = req.PlayerId ?? 0 },
                req.MatchId, req.PlayerId);

        private MessageEnvelope HandleLeaveMatch(MessageEnvelope req)
            => Wrap(MessageType.PlayerLeftEvent,
                new PlayerLeftEvent { PlayerId = req.PlayerId ?? 0 },
                req.MatchId, req.PlayerId);

        private MessageEnvelope HandleBuyDecision(MessageEnvelope req)
        {
            var body = JsonSerializer.Deserialize<BuyDecisionRequest>(req.Payload, JsonOpt)!;

            if (!body.Accept)
                return HandleEndTurn(req);

            return Wrap(
                MessageType.MoneyChangedEvent,
                new MoneyChangedEvent { PlayerId = req.PlayerId ?? 0, Amount = -200 },
                req.MatchId,
                req.PlayerId
            );
        }

        // ================== WRAP HELPERS ==================
        private static MessageEnvelope Wrap<T>(MessageType type, T body)
            => new MessageEnvelope
            {
                Type = type,
                Payload = JsonSerializer.Serialize(body, JsonOpt)
            };

        private static MessageEnvelope Wrap<T>(
            MessageType type,
            T body,
            int? matchId,
            int? playerId)
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

        // ================== HASH + OTP ==================
        private static string NormalizeToSha256Hex(string input)
            => IsHexSha256(input) ? input.ToLowerInvariant() : Sha256Hex(input);

        private static bool IsHexSha256(string s)
            => !string.IsNullOrEmpty(s) && s.Length == 64 && Regex.IsMatch(s, "^[0-9a-fA-F]{64}$");

        private static string Sha256Hex(string raw)
        {
            using var sha = SHA256.Create();
            return string.Concat(sha
                .ComputeHash(Encoding.UTF8.GetBytes(raw))
                .Select(b => b.ToString("x2")));
        }

        private static readonly Dictionary<string, (string otp, DateTime exp, bool verified)> _otp = new();

        private static string GenerateOtp(string email)
        {
            string otp = new Random().Next(100000, 999999).ToString();
            _otp[email] = (otp, DateTime.UtcNow.AddMinutes(2), false);
            return otp;
        }

        private static async Task SendOtpMailAsync(string toEmail, string otp)
        {
            string host = ConfigurationManager.AppSettings["SMTP_HOST"];
            int port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
            string fromEmail = ConfigurationManager.AppSettings["SMTP_EMAIL"];
            string password = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

            using var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true,
                Timeout = 10_000 // 10 giây
            };

            var mail = new MailMessage
            {
                From = new MailAddress(fromEmail, "Monopoly Game"),
                Subject = "Mã OTP đặt lại mật khẩu",
                Body = $@"Xin chào,
Mã OTP của bạn là: {otp}

Mã có hiệu lực trong 2 phút.
Vui lòng không chia sẻ mã này.

Monopoly Server",
                IsBodyHtml = false
            };

            mail.To.Add(toEmail);

            await smtp.SendMailAsync(mail);
        }



        private static bool VerifyOtp(string email, string input)
        {
            if (!_otp.TryGetValue(email, out var s)) return false;
            if (s.exp < DateTime.UtcNow) { _otp.Remove(email); return false; }
            if (s.otp != input) return false;
            _otp[email] = (s.otp, s.exp, true);
            return true;
        }

        private static bool CanResetPassword(string email)
            => _otp.TryGetValue(email, out var s) && s.verified && s.exp >= DateTime.UtcNow;

        private static void ClearOtp(string email) => _otp.Remove(email);
    }
}
