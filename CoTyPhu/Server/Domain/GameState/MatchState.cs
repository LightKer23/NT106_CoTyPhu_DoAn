using Server.Domain.Board;
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

        //Vị trí hiện tại của người chơi
        public int CurrentPlayerIndex { get; set; }
        
        //Khởi tạo Bàn Game
        public List<Tile> Board { get; set; } = BoardLoader.LoadDefaultBoard();

        //Mua hay không
        public bool WaitingForBuyDecision { get; set; }

        //Index ô Server đợi người chơi quyết định mua hay không
        public int? PendingTileIndex { get; set; }
    }
}
