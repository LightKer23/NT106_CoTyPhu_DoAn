using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Client.Network
{
    public static class ClientSession
    {
        public static TcpClientService Tcp { get; private set; }
        public static int AccountID { get; set; }


        public static int PlayerID { get; set; }   // 0,1,2,3
        public static int MatchID { get; set; }


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
    }
}
