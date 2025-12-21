using Common.Constracts;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Infrastructure.Network
{
    public sealed class ClientConnection : IDisposable
    {
        private readonly TcpClient _tcp;
        private readonly NetworkStream _stream;
        private readonly SemaphoreSlim _sendLock = new(1, 1);

        private static readonly JsonSerializerOptions JsonOpt = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ClientConnection(TcpClient tcp)
        {
            _tcp = tcp;
            _stream = tcp.GetStream();
        }

        public bool IsConnected => _tcp.Connected;

        public async Task<MessageEnvelope?> ReceiveAsync(CancellationToken ct = default)
        {
            var bytes = await ReadFrameAsync(_stream, ct);
            if (bytes == null) return null;

            return JsonSerializer.Deserialize<MessageEnvelope>(Encoding.UTF8.GetString(bytes), JsonOpt);
        }

        public async Task SendAsync(MessageEnvelope env, CancellationToken ct = default)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(env, JsonOpt));

            await _sendLock.WaitAsync(ct);
            try { await WriteFrameAsync(_stream, bytes, ct); }
            finally { _sendLock.Release(); }
        }

        public void Dispose()
        {
            try { _stream.Close(); } catch { }
            try { _tcp.Close(); } catch { }
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
                if (n == 0) return Array.Empty<byte>();
                read += n;
            }
            return buf;
        }
    }
}