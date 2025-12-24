using Common.Constracts;
using Common.Contracts.Auth;
using Server.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Server.Infrastructure.Network
{

    public sealed class TcpServer
    {
        private readonly TcpListener _listener;
        private readonly IRequestDispatcher _dispatcher;

        public TcpServer(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);

            _dispatcher = new ServerDispatcher();
        }

        public async Task StartAsync(CancellationToken ct = default)
        {
            _listener.Start();

            while (!ct.IsCancellationRequested)
            {
                var tcp = await _listener.AcceptTcpClientAsync(ct);
                _ = Task.Run(() => ClientLoopAsync(tcp, ct), ct);
            }
        }

        private async Task ClientLoopAsync(TcpClient tcp, CancellationToken ct)
        {
            using var conn = new ClientConnection(tcp);

            while (!ct.IsCancellationRequested && conn.IsConnected)
            {
                ServerState.CurrentConnection = conn;

                var req = await conn.ReceiveAsync(ct);
                if (req == null) break;

                var resp = await _dispatcher.DispatchAsync(req);
                if (req.Type == MessageType.LoginRequest &&
                    resp.Type == MessageType.LoginResponse)
                {
                    var login = JsonSerializer.Deserialize<LoginResponse>(resp.Payload);
                    if (login?.Success == true && login.IDAccount != null)
                    {
                        ServerState.Sessions[login.IDAccount.Value] = new PlayerSession
                        {
                            AccountId = login.IDAccount.Value,
                            Connection = conn
                        };
                    }
                }


                resp.MessageId = req.MessageId;
                resp.MatchId ??= req.MatchId;
                resp.PlayerId ??= req.PlayerId;

                await conn.SendAsync(resp, ct);
            }
        }

    }

    public interface IRequestDispatcher
    {
        Task<MessageEnvelope> DispatchAsync(MessageEnvelope req);
    }
}