using Common.Constracts;
using Common.Constracts.Room;
using Common.Contracts.Auth;
using Common.Contracts.Game;
using Common.Contracts.Room;
using Common.Contracts.Room.Common.Constracts.Room;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Network
{
    public sealed class TcpClientService : IDisposable
    {
        private TcpClient? _tcp;
        private NetworkStream? _stream;
        private readonly SemaphoreSlim _sendLock = new(1, 1);

        // map MessageId -> waiter (đợi đúng response)
        private readonly ConcurrentDictionary<Guid, TaskCompletionSource<MessageEnvelope>> _pending = new();

        // Event broadcast từ server (TurnResultEvent, PlayerMovedEvent...)
        public event Action<MessageEnvelope>? OnEvent;

        public bool IsConnected => _tcp?.Connected == true && _stream != null;

        private static readonly JsonSerializerOptions JsonOpt = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task ConnectAsync(string host, int port, CancellationToken ct = default)
        {
            if (IsConnected) return;

            _tcp = new TcpClient();
            await _tcp.ConnectAsync(host, port, ct);
            _stream = _tcp.GetStream();

            _ = Task.Run(() => ReceiveLoopAsync(ct), ct);
        }

        public Task<LoginResponse> LoginAsync(string username, string password, CancellationToken ct = default)
        {
            return RequestAsync<LoginRequest, LoginResponse>(
                MessageType.LoginRequest,
                MessageType.LoginResponse,
                new LoginRequest { Username = username, Password = password },
                matchId: null, playerId: null,
                ct: ct);
        }

        public Task<RegisterResponse> RegisterAsync(RegisterRequest req, CancellationToken ct = default)
        {
            return RequestAsync<RegisterRequest, RegisterResponse>(
                MessageType.RegisterRequest,
                MessageType.RegisterResponse,
                req,
                matchId: null, playerId: null,
                ct: ct);
        }


        public Task<VerifyOTPResponse> VerifyOTPAsync(string email, string otp, CancellationToken ct = default)
        {
            return RequestAsync<VerifyOTPRequest, VerifyOTPResponse>(
                MessageType.VerifyOTPRequest,
                MessageType.VerifyOTPResponse,
                new VerifyOTPRequest
                {
                    Email = email,
                    OTP = otp
                },
                matchId: null,
                playerId: null,
                ct: ct);
        }


        public Task<ForgotPasswordResponse> ForgotPasswordAsync(string email, CancellationToken ct = default)
        {
            return RequestAsync<ForgotPasswordRequest, ForgotPasswordResponse>(
                MessageType.ForgotPasswordRequest,
                MessageType.ForgotPasswordResponse,
                new ForgotPasswordRequest { Email = email },
                matchId: null, playerId: null,
                ct: ct);
        }

        public Task<ResetPasswordResponse> ResetPasswordAsync(string email, string newPassword, CancellationToken ct = default)
        {
            return RequestAsync<ResetPasswordRequest, ResetPasswordResponse>(
                MessageType.ResetPasswordRequest,
                MessageType.ResetPasswordResponse,
                new ResetPasswordRequest { Email = email, NewPassword = newPassword },
                matchId: null, playerId: null,
                ct: ct);
        }

        public Task<DiceRolledEvent> RollDiceAsync(int matchId, int playerId)
        {
            return RequestAsync<object, DiceRolledEvent>(
                MessageType.RollDiceRequest,
                MessageType.DiceRolledEvent,
                new { },
                matchId,
                playerId);
        }

        public Task<MoneyChangedEvent> BuyDecisionAsync(int matchId, int playerId, int propertyId, bool accept)
        {
            return RequestAsync<BuyDecisionRequest, MoneyChangedEvent>(
                MessageType.BuyDecisionRequest,
                MessageType.MoneyChangedEvent,
                new BuyDecisionRequest
                {
                    PropertyID = propertyId,
                    Accept = accept
                },
                matchId,
                playerId);
        }

        public Task<PlayerReleasedFromJailEvent> GetOutOfJailAsync(int matchId, int playerId, string method)
        {
            return RequestAsync<GetOutOfJailRequest, PlayerReleasedFromJailEvent>(
                MessageType.GetOutOfJailRequest,
                MessageType.PlayerReleasedFromJailEvent,
                new GetOutOfJailRequest { Method = method },
                matchId,
                playerId);
        }

        public Task<MoneyChangedEvent> UpgradePropertyAsync(int matchId, int playerId, int propertyId)
        {
            return RequestAsync<UpgradePropertyRequest, MoneyChangedEvent>(
                MessageType.UpgradePropertyRequest,
                MessageType.MoneyChangedEvent,
                new UpgradePropertyRequest { PropertyId = propertyId },
                matchId,
                playerId);
        }

        public Task<TurnResultEvent> EndTurnAsync(int matchId, int playerId)
        {
            return RequestAsync<object, TurnResultEvent>(
                MessageType.EndTurnRequest,
                MessageType.TurnResultEvent,
                new { },
                matchId,
                playerId);
        }

        public Task<MoneyChangedEvent> SellPropertyAsync(int matchId, int playerId, int propertyId)
        {
            return RequestAsync<SellPropertyRequest, MoneyChangedEvent>(
                MessageType.SellPropertyRequest,
                MessageType.MoneyChangedEvent,
                new SellPropertyRequest { PropertyId = propertyId },
                matchId,
                playerId);
        }

        public Task<PlayerLeftEvent> LeaveMatchAsync(int matchId, int playerId)
        {
            return RequestAsync<object, PlayerLeftEvent>(
                MessageType.LeaveMatchRequest,
                MessageType.PlayerLeftEvent,
                new { },
                matchId,
                playerId);
        }

        // CORE REQUEST (generic)
        public async Task<TResp> RequestAsync<TReq, TResp>(
            MessageType reqType,
            MessageType respType,
            TReq body,
            int? matchId,
            int? playerId,
            CancellationToken ct = default)
        {
            EnsureConnected();

            var env = new MessageEnvelope
            {
                MessageId = Guid.NewGuid(),
                Type = reqType,
                MatchId = matchId,
                PlayerId = playerId,
                Payload = JsonSerializer.Serialize(body, JsonOpt)
            };

            var tcs = new TaskCompletionSource<MessageEnvelope>(TaskCreationOptions.RunContinuationsAsynchronously);
            _pending[env.MessageId] = tcs;

            await SendEnvelopeAsync(env, ct);

            var respEnv = await tcs.Task;
            if (respEnv.Type != respType)
                throw new InvalidOperationException($"Expected {respType} but got {respEnv.Type}");

            return JsonSerializer.Deserialize<TResp>(respEnv.Payload, JsonOpt)!;
        }

        public async Task SendEnvelopeAsync(MessageEnvelope env, CancellationToken ct = default)
        {
            EnsureConnected();

            byte[] payload = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(env, JsonOpt));

            await _sendLock.WaitAsync(ct);
            try { await WriteFrameAsync(_stream!, payload, ct); }
            finally { _sendLock.Release(); }
        }

        public Task<CreateRoomResponse> CreateRoomAsync(int accountId, CancellationToken ct = default)
        {
            return RequestAsync<CreateRoomRequest, CreateRoomResponse>(
                MessageType.CreateRoomRequest,
                MessageType.CreateRoomResponse,
                new CreateRoomRequest
                {
                    AccountID = accountId
                },
                matchId: null,
                playerId: null,
                ct: ct
            );
        }


        public Task<SearchRoomResponse> SearchRoomAsync(int accountId, CancellationToken ct = default)
        {
            return RequestAsync<SearchRoomRequest, SearchRoomResponse>(
                MessageType.SearchRoomRequest,
                MessageType.SearchRoomResponse,
                new SearchRoomRequest
                {
                    AccountID = accountId
                },
                matchId: null,
                playerId: null,
                ct: ct
            );
        }



        public Task<JoinRoomResponse> JoinRoomAsync(
    int roomId,
    int accountId,
    int characterIndex,
    CancellationToken ct = default)
        {
            return RequestAsync<JoinRoomRequest, JoinRoomResponse>(
                MessageType.JoinRoomRequest,
                MessageType.JoinRoomResponse,
                new JoinRoomRequest
                {
                    RoomID = roomId,
                    AccountID = accountId,
                    CharacterIndex = characterIndex
                },
                matchId: null,
                playerId: null,
                ct: ct
            );
        }



        private async Task ReceiveLoopAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested && _stream != null)
                {
                    byte[]? bytes = await ReadFrameAsync(_stream, ct);
                    if (bytes == null) break;

                    var env = JsonSerializer.Deserialize<MessageEnvelope>(Encoding.UTF8.GetString(bytes), JsonOpt);
                    if (env == null) continue;

                    // Response theo MessageId
                    if (_pending.TryRemove(env.MessageId, out var waiter))
                    {
                        waiter.TrySetResult(env);
                        continue;
                    }

                    // Event broadcast
                    OnEvent?.Invoke(env);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CLIENT] ReceiveLoop error: " + ex.Message);
            }
        }

        private void EnsureConnected()
        {
            if (!IsConnected) throw new InvalidOperationException("Client chưa ConnectAsync()");
        }

        public void Dispose()
        {
            try { _stream?.Close(); } catch { }
            try { _tcp?.Close(); } catch { }
            _stream = null;
            _tcp = null;
        }


        private static async Task WriteFrameAsync(NetworkStream stream, byte[] payload, CancellationToken ct)
        {
            byte[] len = BitConverter.GetBytes(payload.Length);
            await stream.WriteAsync(len, 0, 4, ct);
            await stream.WriteAsync(payload, 0, payload.Length, ct);
            await stream.FlushAsync(ct);
        }

        private static async Task<byte[]?> ReadFrameAsync(NetworkStream stream, CancellationToken ct)
        {
            byte[] lenBuf = await ReadExactAsync(stream, 4, ct);
            if (lenBuf.Length == 0) return null;

            int length = BitConverter.ToInt32(lenBuf, 0);
            if (length <= 0 || length > 10_000_000) throw new InvalidOperationException("Invalid frame length");

            byte[] payload = await ReadExactAsync(stream, length, ct);
            return payload.Length == 0 ? null : payload;
        }

        private static async Task<byte[]> ReadExactAsync(NetworkStream stream, int size, CancellationToken ct)
        {
            byte[] buf = new byte[size];
            int read = 0;
            while (read < size)
            {
                int n = await stream.ReadAsync(buf, read, size - read, ct);
                if (n == 0) return Array.Empty<byte>(); // disconnected
                read += n;
            }
            return buf;
        }
    }
}