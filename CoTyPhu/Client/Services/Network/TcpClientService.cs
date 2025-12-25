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

        // MessageId -> waiter
        private readonly ConcurrentDictionary<Guid, TaskCompletionSource<MessageEnvelope>> _pending = new();

        // Server push (events)
        public event Action<MessageEnvelope>? OnEvent;

        public bool IsConnected => _tcp?.Connected == true && _stream != null;

        private static readonly JsonSerializerOptions JsonOpt = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // Nếu muốn đổi timeout, set property này
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
            if (!IsConnected) throw new InvalidOperationException("Client chưa ConnectAsync()");
        }

        public Task<LoginResponse> LoginAsync(string username, string password, CancellationToken ct = default)
            => RequestAsync<LoginRequest, LoginResponse>(
                MessageType.LoginRequest,
                MessageType.LoginResponse,
                new LoginRequest { Username = username, Password = password },
                matchId: null, playerId: null, ct: ct);

        public Task<RegisterResponse> RegisterAsync(RegisterRequest req, CancellationToken ct = default)
            => RequestAsync<RegisterRequest, RegisterResponse>(
                MessageType.RegisterRequest,
                MessageType.RegisterResponse,
                req,
                matchId: null, playerId: null, ct: ct);

        public Task<ForgotPasswordResponse> ForgotPasswordAsync(string email, CancellationToken ct = default)
            => RequestAsync<ForgotPasswordRequest, ForgotPasswordResponse>(
                MessageType.ForgotPasswordRequest,
                MessageType.ForgotPasswordResponse,
                new ForgotPasswordRequest { Email = email },
                matchId: null, playerId: null, ct: ct);

        public Task<VerifyOTPResponse> VerifyOTPAsync(string email, string otp, CancellationToken ct = default)
            => RequestAsync<VerifyOTPRequest, VerifyOTPResponse>(
                MessageType.VerifyOTPRequest,
                MessageType.VerifyOTPResponse,
                new VerifyOTPRequest { Email = email, OTP = otp },
                matchId: null, playerId: null, ct: ct);

        public Task<ResetPasswordResponse> ResetPasswordAsync(string email, string newPassword, CancellationToken ct = default)
            => RequestAsync<ResetPasswordRequest, ResetPasswordResponse>(
                MessageType.ResetPasswordRequest,
                MessageType.ResetPasswordResponse,
                new ResetPasswordRequest { Email = email, NewPassword = newPassword },
                matchId: null, playerId: null, ct: ct);

        public Task<GetMatchHistoryResponse> GetMatchHistoryAsync(int accountId, CancellationToken ct = default)
            => RequestAsync<GetMatchHistoryRequest, GetMatchHistoryResponse>(
                MessageType.GetMatchHistoryRequest,
                MessageType.GetMatchHistoryResponse,
                new GetMatchHistoryRequest { AccountId = accountId },
                matchId: null, playerId: null, ct: ct);




        // ================== ROOM (CHUẨN FLOW) ==================

        // Create: server tạo roomId (SQL), host = playerId 1
        public Task<CreateRoomResponse> CreateRoomAsync(int accountId, CancellationToken ct = default)
            => RequestAsync<CreateRoomRequest, CreateRoomResponse>(
                MessageType.CreateRoomRequest,
                MessageType.CreateRoomResponse,
                new CreateRoomRequest { AccountID = accountId },
                matchId: null, playerId: null, ct: ct);

        // Search: client truyền roomId qua MessageEnvelope.MatchId (body không cần)
        public Task<SearchRoomResponse> SearchRoomAsync(int roomId, CancellationToken ct = default)
            => RequestAsync<object, SearchRoomResponse>(
                MessageType.SearchRoomRequest,
                MessageType.SearchRoomResponse,
                new { },
                matchId: roomId, playerId: null, ct: ct);

        // Join: body có roomId + accountId + characterIndex. matchId/playerId để null.
        public Task<JoinRoomResponse> JoinRoomAsync(int roomId, int accountId, int characterIndex, CancellationToken ct = default)
            => RequestAsync<JoinRoomRequest, JoinRoomResponse>(
                MessageType.JoinRoomRequest,
                MessageType.JoinRoomResponse,
                new JoinRoomRequest
                {
                    RoomID = roomId,
                    AccountID = accountId,
                    CharacterIndex = characterIndex
                },
                matchId: null, playerId: null, ct: ct);

        // Leave
        public Task<PlayerLeftEvent> LeaveRoomAsync(int matchId, int playerId, CancellationToken ct = default)
            => RequestAsync<object, PlayerLeftEvent>(
                MessageType.LeaveMatchRequest,
                MessageType.PlayerLeftEvent,
                new { },
                matchId: matchId, playerId: playerId, ct: ct);

        // Start (host only). Server trả StartMatchResponse (bạn phải có MessageType này)
        public Task<StartMatchResponse> StartMatchAsync(int matchId, int playerId, CancellationToken ct = default)
            => RequestAsync<object, StartMatchResponse>(
                MessageType.StartMatchRequest,
                MessageType.StartMatchResponse,
                new { },
                matchId: matchId, playerId: playerId, ct: ct);

        // ================== GAME ==================
        public Task<object> RollDiceAsync(int matchId, int playerId, CancellationToken ct = default)
            => RequestAsync<RollDiceRequest, object>(
                MessageType.RollDiceRequest,
                MessageType.DiceRolledEvent,   // server của bạn đang trả DiceRolledEvent {Success=true} (tạm)
                new RollDiceRequest { MatchId = matchId, PlayerId = playerId },
                matchId: matchId,
                playerId: playerId,
                ct: ct);

        public Task<GetOutOfJailResponse> GetOutOfJailAsync(
                int matchId,
                int playerId,
                string method,  
                CancellationToken ct = default)
                => RequestAsync<GetOutOfJailRequest, GetOutOfJailResponse>(
                    MessageType.GetOutOfJailRequest,
                    MessageType.GetOutOfJailRequest,  
                    new GetOutOfJailRequest { Method = method },
                    matchId: matchId,
                    playerId: playerId,
                    ct: ct);

        public Task EndTurnAsync(int matchId, int playerId)
        {
            var env = new MessageEnvelope
            {
                MessageId = Guid.NewGuid(),
                Type = MessageType.EndTurnRequest,
                MatchId = matchId,
                PlayerId = playerId,
                Payload = "{}"
            };

            return SendEnvelopeAsync(env);
        }

        public Task PlayerSurrenderAsync(int matchId, int playerId)
        {
            return SendEnvelopeAsync(new MessageEnvelope
            {
                MessageId = Guid.NewGuid(),
                Type = MessageType.PlayerSurrenderRequest,
                MatchId = matchId,
                PlayerId = playerId,
                Payload = JsonSerializer.Serialize(new PlayerSurrenderRequest
                { }
                )
            });
        }



        public Task<object> BuyDecisionAsync(int matchId, int playerId, int tileIndex, bool accept, CancellationToken ct = default)
        => RequestAsync<BuyDecisionRequest, object>(
            MessageType.BuyDecisionRequest,
            MessageType.PropertyUpdatedEvent,   
            new BuyDecisionRequest
            {
                PropertyID = tileIndex,
                Accept = accept
             },
            matchId: matchId,
            playerId: playerId,
            ct: ct);

        // ================== CHAT ==================
        public async Task SendChatMessageAsync(int matchId, int playerId, string message, CancellationToken ct = default)
        {
            var env = new MessageEnvelope
            {
                MessageId = Guid.NewGuid(),
                Type = MessageType.SendChatMessageRequest,
                MatchId = matchId,
                PlayerId = playerId,
                Payload = JsonSerializer.Serialize(new SendChatMessageRequest
                {
                    MatchId = matchId,
                    PlayerId = playerId,
                    Message = message
                }, JsonOpt)
            };

            var tcs = new TaskCompletionSource<MessageEnvelope>(TaskCreationOptions.RunContinuationsAsynchronously);
            _pending[env.MessageId] = tcs;

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

        // ================== CORE REQUEST ==================
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

            // timeout + cancel
            using var timeoutCts = new CancellationTokenSource(RequestTimeout);
            using var linked = CancellationTokenSource.CreateLinkedTokenSource(ct, timeoutCts.Token);

            MessageEnvelope respEnv;
            try
            {
                respEnv = await tcs.Task.WaitAsync(linked.Token);
            }
            catch
            {
                _pending.TryRemove(env.MessageId, out _);
                throw new TimeoutException($"Request timeout: {reqType}");
            }

            if (respEnv.Type == MessageType.ErrorResponse)
                throw new InvalidOperationException($"Server error: {respEnv.Payload}");

            if (respEnv.Type != respType)
                throw new InvalidOperationException($"Expected {respType} but got {respEnv.Type}");

            var obj = JsonSerializer.Deserialize<TResp>(respEnv.Payload, JsonOpt);
            if (obj == null) throw new InvalidOperationException("Response payload is null/invalid JSON");
            return obj;
        }

        public async Task SendEnvelopeAsync(MessageEnvelope env, CancellationToken ct = default)
        {
            if (!IsConnected) return; // <== đừng throw nữa, vì có thể server vừa đóng

            byte[] payload = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(env, JsonOpt));

            await _sendLock.WaitAsync(ct);
            try
            {
                // _stream có thể bị null nếu vừa Dispose
                var stream = _stream;
                if (stream == null) return;

                await WriteFrameAsync(stream, payload, ct);
            }
            catch (IOException)
            {
                // Server đóng socket -> đóng client cho sạch, tránh crash
                Dispose();
            }
            catch (ObjectDisposedException)
            {
                // Stream/socket đã bị dispose -> bỏ qua
            }
            finally
            {
                _sendLock.Release();
            }
        }


        // ================== RECEIVE LOOP ==================
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

                    // Server push event
                    OnEvent?.Invoke(env);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CLIENT] ReceiveLoop error: " + ex.Message);
            }
        }

        // ================== DISPOSE ==================
        public void Dispose()
        {
            try { _stream?.Close(); } catch { }
            try { _tcp?.Close(); } catch { }

            _stream = null;
            _tcp = null;

            foreach (var kv in _pending)
                kv.Value.TrySetCanceled();
            _pending.Clear();
        }

        // ================== FRAMING ==================
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
            if (length <= 0 || length > 10_000_000)
                throw new InvalidOperationException("Invalid frame length");

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
