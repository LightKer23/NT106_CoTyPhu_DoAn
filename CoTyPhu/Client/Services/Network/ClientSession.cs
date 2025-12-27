using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Client.Services.Network
{
    public static class ClientSession
    {
        public static TcpClientService Tcp { get; private set; }
        public static int AccountID { get; set; }
        public static int HostID { get; set; }

        public static int PlayerID { get; set; }  
        public static int MatchID { get; set; }
        public static string Username { get; private set; }
        public static string DisplayName { get; private set; }
        public static string Email { get; private set; }

        public static ClientGameState GameState { get; } = new ClientGameState();


        public static async Task ConnectAsync()
        {
            if (Tcp != null && Tcp.IsConnected)
                return;

            string host = ConfigurationManager.AppSettings["ServerHost"]
                          ?? throw new InvalidOperationException("ServerHost missing");
            string portStr = ConfigurationManager.AppSettings["ServerPort"]
                          ?? throw new InvalidOperationException("ServerPort missing");

            int port = int.Parse(portStr);

            if (Tcp == null)
                Tcp = new TcpClientService();

            await Tcp.ConnectAsync(host, port);
        }

        public static void Disconnect()
        {
            // Dispose = đóng socket + stream
            Tcp?.Dispose();
            Tcp = null;
        }

        public class ClientGameState
        {
            public Dictionary<int, ClientPlayerState> Players { get; } = new();
            public Dictionary<int, ClientPropertyState> Properties { get; } = new();

            public int CurrentTurnPlayerId { get; set; }

            public void Reset()
            {
                Players.Clear();
                Properties.Clear();
                CurrentTurnPlayerId = 0;
            }
        }

        public class ClientPlayerState
        {
            public int PlayerId { get; set; }
            public int Position { get; set; }
            public int Money { get; set; }
            public bool IsInJail { get; set; }
        }

        public static void SetLoginInfo(int accountId, string username, string displayName, string email)
        {
            AccountID = accountId;
            Username = username;
            DisplayName = displayName;
            Email = email;
        }


        public class ClientPropertyState
        {
            public int TileIndex { get; set; }
            public int? OwnerPlayerId { get; set; }
            public int Level { get; set; } 
        }
    }
}
