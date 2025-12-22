using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.GameState
{
    public class MatchState
    {
        public int MatchId { get; set; }

        // PlayerId → PlayerState
        public Dictionary<int, PlayerState> Players { get; set; } = new();
        public Dictionary<int, PropertyState> Properties { get; set; } = new();

        // PlayerId đang tới lượt
        public int CurrentTurnPlayerId { get; set; }

        // trạng thái trận
        public int IsMatch { get; set; } // 1 = bắt đầu, 0 = chưa bắt đầu, 2 = kết thúc

        public MatchState() { }

        public MatchState(int matchId, Dictionary<int, PlayerState> players, Dictionary<int, PropertyState> properties)
        {
            MatchId = matchId;
            Players = players;
            Properties = properties;
            
        }
    }
}
