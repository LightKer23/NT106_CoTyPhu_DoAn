using Server.Infrastructure.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain.GameState;
using Server.Domain.GameState.Board;

namespace Server.Domain
{
    public static class ServerState
    {
        // PlayerId → Session (online / offline)
        public static Dictionary<int, PlayerSession> Sessions { get; } = new();

        // MatchId → MatchState
        public static Dictionary<int, MatchState> Matches { get; } = new();

        // Bàn cờ dùng chung
        public static List<Tile> Board { get; set; } = BoardLoader.LoadDefaultBoard();
    }
}
