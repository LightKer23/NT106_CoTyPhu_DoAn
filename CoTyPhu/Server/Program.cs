using Server.Infrastructure.Network;
using System;
using System.Threading.Tasks;

internal class Program
{
    static async Task Main(string[] args)
    {

        var server = new TcpServer(7777);

        await server.StartAsync();
    }
}
