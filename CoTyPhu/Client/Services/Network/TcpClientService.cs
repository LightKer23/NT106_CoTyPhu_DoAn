using Common.Constracts;
using Common.Constracts.Room;
using Common.Contracts.Auth;
using Common.Contracts.Game;
using Common.Contracts.Room;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services.Network
{
    public sealed class TcpClientService : IDisposable
    {
        private TcpClient? _tcp;
        private NetworkStream? _stream;
        private readonly SemaphoreSlim _sendLock = new(1, 1);

        private sealed class PendingItem
        {
            public MessageType ExpectedType { get; init; }
            public TaskCompletionSource<MessageEnvelope> Tcs { get; init; } = default!;
        }

        private readonly ConcurrentDictionary<Guid, PendingItem> _pending = new();

        public event Action<MessageEnvelope>? OnEvent;

        public bool IsConnected => _tcp?.Connected == true && _stream != null;

        private static readonly JsonSerializerOptions JsonOpt = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(12);

        public async Task ConnectAsync(string host, int port, CancellationToken ct = default)
        {
            if (IsConnected) return;

            _tcp = new TcpClient();
            await _tcp.ConnectAsync(host, port, ct);
            _stream = _tcp.GetStream();

            _ = Task.Run(() => ReceiveLoopAsync(ct), ct);
        }

        private void EnsureConnected()
        {
            if (!IsConnected)
                throw new InvalidOperationException("Client chưa ConnectAsync()");
        }

        public async Task SendChatMessageAsync(
            int matchId,
            int playerId,
            string message,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            EnsureConnected();

            var env = new MessageEnvelope
            {
                MessageId = Guid.NewGuid(),
                Type = MessageType.SendChatMessageRequest,
                MatchId = matchId,
                PlayerId = playerId,
                Payload = JsonSerializer.Serialize(
                    new SendChatMessageRequest
                    {
                        MatchId = matchId,
                        PlayerId = playerId,
                        Message = message
                    },
                    JsonOpt
                )
            };

            var tcs = new TaskCompletionSource<MessageEnvelope>(
                TaskCreationOptions.RunContinuationsAsynchronously);

            _pending[env.MessageId] = new PendingItem
            {
                ExpectedType = MessageType.ChatMessageEvent,
                Tcs = tcs
            };

            await SendEnvelopeAsync(env, ct);

            try
            {
                using var timeoutCts = new CancellationTokenSource(RequestTimeout);
                using var linked = CancellationTokenSource.CreateLinkedTokenSource(ct, timeoutCts.Token);
                await tcs.Task.WaitAsync(linked.Token);
            }
            catch
            {
                _pending.TryRemove(env.MessageId, out _);
            }
        }


        public Task<LoginResponse> LoginAsync(string username, string password, CancellationToken ct = default)
            => RequestAsync<LoginRequest, LoginResponse>(
                MessageType.LoginRequest,
                MessageType.LoginResponse,
                new LoginRequest { Username = username, Password = password },
                null, null, ct);

        public Task<RegisterResponse> RegisterAsync(RegisterRequest req, CancellationToken ct = default)
            => RequestAsync<RegisterRequest, RegisterResponse>(
                MessageType.RegisterRequest,
                MessageType.RegisterResponse,
                req, null, null, ct);

        public Task<ForgotPasswordResponse> ForgotPasswordAsync(string email, CancellationToken ct = default)
            => RequestAsync<ForgotPasswordRequest, ForgotPasswordResponse>(
                MessageType.ForgotPasswordRequest,
                MessageType.ForgotPasswordResponse,
                new ForgotPasswordRequest { Email = email }, null, null, ct);

        public Task<VerifyOTPResponse> VerifyOTPAsync(string email, string otp, CancellationToken ct = default)
            => RequestAsync<VerifyOTPRequest, VerifyOTPResponse>(
                MessageType.VerifyOTPRequest,
                MessageType.VerifyOTPResponse,
                new VerifyOTPRequest { Email = email, OTP = otp }, null, null, ct);

        public Task<ResetPasswordResponse> ResetPasswordAsync(string email, string newPassword, CancellationToken ct = default)
            => RequestAsync<ResetPasswordRequest, ResetPasswordResponse>(
                MessageType.ResetPasswordRequest,
                MessageType.ResetPasswordResponse,
                new ResetPasswordRequest { Email = email, NewPassword = newPassword }, null, null, ct);

        public Task<GetMatchHistoryResponse> GetMatchHistoryAsync(int accountId, CancellationToken ct = default)
            => RequestAsync<GetMatchHistoryRequest, GetMatchHistoryResponse>(
                MessageType.GetMatchHistoryRequest,
                MessageType.GetMatchHistoryResponse,
                new GetMatchHistoryRequest { AccountId = accountId }, null, null, ct);

        public Task<CreateRoomResponse> CreateRoomAsync(int accountId, CancellationToken ct = default)
            => RequestAsync<CreateRoomRequest, CreateRoomResponse>(
                MessageType.CreateRoomRequest,
                MessageType.CreateRoomResponse,
                new CreateRoomRequest { AccountID = accountId }, null, null, ct);

        public Task<SearchRoomResponse> SearchRoomAsync(int roomId, CancellationToken ct = default)
            => RequestAsync<object, SearchRoomResponse>(
                MessageType.SearchRoomRequest,
                MessageType.SearchRoomResponse,
                new { }, roomId, null, ct);

        public Task<JoinRoomResponse> JoinRoomAsync(int roomId, int accountId, int characterIndex, CancellationToken ct = default)
            => RequestAsync<JoinRoomRequest, JoinRoomResponse>(
                MessageType.JoinRoomRequest,
                MessageType.JoinRoomResponse,
                new JoinRoomRequest
                {
                    RoomID = roomId,
                    AccountID = accountId,
                    CharacterIndex = characterIndex
                }, null, null, ct);

        public Task<PlayerLeftEvent> LeaveRoomAsync(int matchId, int playerId, CancellationToken ct = default)
            => RequestAsync<object, PlayerLeftEvent>(
                MessageType.LeaveMatchRequest,
                MessageType.PlayerLeftEvent,
                new { }, matchId, playerId, ct);

        public Task<StartMatchResponse> StartMatchAsync(int matchId, int playerId, CancellationToken ct = default)
            => RequestAsync<object, StartMatchResponse>(
                MessageType.StartMatchRequest,
                MessageType.StartMatchResponse,
                new { }, matchId, playerId, ct);

        public Task<object> RollDiceAsync(int matchId, int playerId, CancellationToken ct = default)
            => RequestAsync<RollDiceRequest, object>(
                MessageType.RollDiceRequest,
                MessageType.DiceRolledEvent,
                new RollDiceRequest { MatchId = matchId, PlayerId = playerId },
                matchId, playerId, ct);

        public Task EndTurnAsync(int matchId, int playerId)
            => SendEnvelopeAsync(new MessageEnvelope
            {
                MessageId = Guid.NewGuid(),
                Type = MessageType.EndTurnRequest,
                MatchId = matchId,
                PlayerId = playerId,
                Payload = "{}"
            });

        public Task PlayerSurrenderAsync(int matchId, int playerId)
            => SendEnvelopeAsync(new MessageEnvelope
            {
                MessageId = Guid.NewGuid(),
                Type = MessageType.PlayerSurrenderRequest,
                MatchId = matchId,
                PlayerId = playerId,
                Payload = "{}"
            });

        public Task<object> BuyDecisionAsync(int matchId, int playerId, int tileIndex, bool accept, CancellationToken ct = default)
            => RequestAsync<BuyDecisionRequest, object>(
                MessageType.BuyDecisionRequest,
                MessageType.PropertyUpdatedEvent,
                new BuyDecisionRequest { PropertyID = tileIndex, Accept = accept },
                matchId, playerId, ct);

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
            _pending[env.MessageId] = new PendingItem
            {
                ExpectedType = respType,
                Tcs = tcs
            };

            await SendEnvelopeAsync(env, ct);

            using var timeout = new CancellationTokenSource(RequestTimeout);
            using var linked = CancellationTokenSource.CreateLinkedTokenSource(ct, timeout.Token);

            MessageEnvelope resp;
            try
            {
                resp = await tcs.Task.WaitAsync(linked.Token);
            }
            catch
            {
                _pending.TryRemove(env.MessageId, out _);
                throw new TimeoutException($"Request timeout: {reqType}");
            }

            if (resp.Type == MessageType.ErrorResponse)
                throw new InvalidOperationException(resp.Payload);

            var obj = JsonSerializer.Deserialize<TResp>(resp.Payload, JsonOpt);
            return obj!;
        }

        public async Task SendEnvelopeAsync(MessageEnvelope env, CancellationToken ct = default)
        {
            if (!IsConnected) return;

            byte[] payload = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(env, JsonOpt));

            await _sendLock.WaitAsync(ct);
            try
            {
                await WriteFrameAsync(_stream!, payload, ct);
            }
            finally
            {
                _sendLock.Release();
            }
        }

        private async Task ReceiveLoopAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested && _stream != null)
                {
                    var bytes = await ReadFrameAsync(_stream, ct);
                    if (bytes == null) break;

                    var env = JsonSerializer.Deserialize<MessageEnvelope>(Encoding.UTF8.GetString(bytes), JsonOpt);
                    if (env == null) continue;

                    if (_pending.TryRemove(env.MessageId, out var item))
                    {
                        item.Tcs.TrySetResult(env);
                        continue;
                    }

                    foreach (var kv in _pending)
                    {
                        if (kv.Value.ExpectedType == env.Type)
                        {
                            if (_pending.TryRemove(kv.Key, out var item2))
                                item2.Tcs.TrySetResult(env);
                            goto NEXT;
                        }
                    }

                    OnEvent?.Invoke(env);

                NEXT:
                    continue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CLIENT] Receive error: " + ex.Message);
            }
        }

        public void Dispose()
        {
            try { _stream?.Close(); } catch { }
            try { _tcp?.Close(); } catch { }

            foreach (var p in _pending.Values)
                p.Tcs.TrySetCanceled();

            _pending.Clear();
            _stream = null;
            _tcp = null;
        }

        private static async Task WriteFrameAsync(NetworkStream stream, byte[] payload, CancellationToken ct)
        {
            await stream.WriteAsync(BitConverter.GetBytes(payload.Length), ct);
            await stream.WriteAsync(payload, ct);
            await stream.FlushAsync(ct);
        }

        private static async Task<byte[]?> ReadFrameAsync(NetworkStream stream, CancellationToken ct)
        {
            byte[] lenBuf = await ReadExactAsync(stream, 4, ct);
            if (lenBuf.Length == 0) return null;

            int len = BitConverter.ToInt32(lenBuf, 0);
            return await ReadExactAsync(stream, len, ct);
        }

        private static async Task<byte[]> ReadExactAsync(NetworkStream stream, int size, CancellationToken ct)
        {
            byte[] buf = new byte[size];
            int read = 0;
            while (read < size)
            {
                int n = await stream.ReadAsync(buf, read, size - read, ct);
                if (n == 0) return Array.Empty<byte>();
                read += n;
            }
            return buf;
        }
    }
}
