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


        public static int PlayerID { get; set; }  
        public static int MatchID { get; set; }

        public static ClientGameState GameState { get; } = new ClientGameState();


        public static async Task ConnectAsync()
        {
            if (Tcp != null && Tcp.IsConnected)
                return;

            string host = ConfigurationManager.AppSettings["ServerHost"];
            int port = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);

            // Tạo và kết nối TCP
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

        public class ClientPropertyState
        {
            public int TileIndex { get; set; }
            public int? OwnerPlayerId { get; set; }
            public int Level { get; set; } 
        }
    }
}
