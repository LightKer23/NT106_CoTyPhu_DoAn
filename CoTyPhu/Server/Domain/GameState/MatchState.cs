using Common.Domain.Game.Enums;
using Server.Domain.GameState.Board;
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

        public MatchState()
        {
            Properties = CreateInitialProperties();
        }


        public MatchState(int matchId, Dictionary<int, PlayerState> players, Dictionary<int, PropertyState> properties)
        {
            MatchId = matchId;
            this.Players = players;
            this.Properties = properties;
            Properties = CreateInitialProperties();
        }

        public Dictionary<int, PropertyState> CreateInitialProperties()
        {
            var props = new Dictionary<int, PropertyState>();

            props[1] = new PropertyState { TileIndex = 1, type = PropertyType.Property, landPrice = 60, housePrice = 50, hotelPrice = 50, rentPrice = new[] { 2, 10, 30, 90, 160, 250 } };
            props[3] = new PropertyState { TileIndex = 3, type = PropertyType.Property, landPrice = 60, housePrice = 50, hotelPrice = 50, rentPrice = new[] { 4, 20, 60, 160, 320, 450 } };

            props[5] = new PropertyState { TileIndex = 5, type = PropertyType.RailRoad, RailRoadBuyPrice = 200 };

            props[6] = new PropertyState { TileIndex = 6, type = PropertyType.Property, landPrice = 100, housePrice = 200, hotelPrice = 200, rentPrice = new[] { 20, 120, 290, 550, 930, 1020 } };
            props[8] = new PropertyState { TileIndex = 8, type = PropertyType.Property, landPrice = 100, housePrice = 200, hotelPrice = 200, rentPrice = new[] { 28, 150, 450, 1000, 1200, 1400 } };
            props[9] = new PropertyState { TileIndex = 9, type = PropertyType.Property, landPrice = 120, housePrice = 200, hotelPrice = 200, rentPrice = new[] { 20, 120, 290, 550, 930, 1020 } };

            props[11] = new PropertyState { TileIndex = 11, type = PropertyType.Property, landPrice = 140, housePrice = 100, hotelPrice = 100, rentPrice = new[] { 20, 120, 290, 550, 930, 1020 } };
            props[12] = new PropertyState { TileIndex = 12, type = PropertyType.Utility, UtilityBuyPrice = 200 };
            props[13] = new PropertyState { TileIndex = 13, type = PropertyType.Property, landPrice = 140, housePrice = 100, hotelPrice = 100, rentPrice = new[] { 10, 50, 150, 450, 625, 750 } };
            props[14] = new PropertyState { TileIndex = 14, type = PropertyType.Property, landPrice = 160, housePrice = 100, hotelPrice = 100, rentPrice = new[] { 12, 60, 180, 500, 700, 900 } };

            props[15] = new PropertyState { TileIndex = 15, type = PropertyType.RailRoad, RailRoadBuyPrice = 200 };

            props[16] = new PropertyState { TileIndex = 16, type = PropertyType.Property, landPrice = 180, housePrice = 100, hotelPrice = 100, rentPrice = new[] { 16, 80, 220, 600, 800, 1000 } };
            props[18] = new PropertyState { TileIndex = 18, type = PropertyType.Property, landPrice = 180, housePrice = 150, hotelPrice = 150, rentPrice = new[] { 16, 80, 220, 600, 800, 1000 } };
            props[19] = new PropertyState { TileIndex = 19, type = PropertyType.Property, landPrice = 200, housePrice = 150, hotelPrice = 150, rentPrice = new[] { 20, 100, 300, 750, 925, 1100 } };

            props[21] = new PropertyState { TileIndex = 21, type = PropertyType.Property, landPrice = 220, housePrice = 150, hotelPrice = 150, rentPrice = new[] { 18, 90, 250, 700, 875, 1050 } };
            props[23] = new PropertyState { TileIndex = 23, type = PropertyType.Property, landPrice = 220, housePrice = 150, hotelPrice = 150, rentPrice = new[] { 18, 90, 250, 700, 875, 1050 } };
            props[24] = new PropertyState { TileIndex = 24, type = PropertyType.Property, landPrice = 240, housePrice = 150, hotelPrice = 150, rentPrice = new[] { 20, 100, 300, 750, 925, 1100 } };

            props[25] = new PropertyState { TileIndex = 25, type = PropertyType.RailRoad, RailRoadBuyPrice = 200 };

            props[26] = new PropertyState { TileIndex = 26, type = PropertyType.Property, landPrice = 260, housePrice = 150, hotelPrice = 150, rentPrice = new[] { 22, 110, 330, 800, 975, 1150 } };
            props[27] = new PropertyState { TileIndex = 27, type = PropertyType.Property, landPrice = 260, housePrice = 150, hotelPrice = 150, rentPrice = new[] { 22, 110, 330, 800, 975, 1150 } };
            props[28] = new PropertyState { TileIndex = 28, type = PropertyType.Utility, UtilityBuyPrice = 200 };
            props[29] = new PropertyState { TileIndex = 29, type = PropertyType.Property, landPrice = 280, housePrice = 150, hotelPrice = 150, rentPrice = new[] { 24, 120, 360, 850, 1025, 1200 } };

            props[31] = new PropertyState { TileIndex = 31, type = PropertyType.Property, landPrice = 300, housePrice = 200, hotelPrice = 200, rentPrice = new[] { 26, 130, 390, 900, 1100, 1275 } };
            props[32] = new PropertyState { TileIndex = 32, type = PropertyType.Property, landPrice = 300, housePrice = 200, hotelPrice = 200, rentPrice = new[] { 26, 130, 390, 900, 1100, 1275 } };
            props[34] = new PropertyState { TileIndex = 34, type = PropertyType.Property, landPrice = 320, housePrice = 200, hotelPrice = 200, rentPrice = new[] { 28, 150, 450, 1000, 1200, 1400 } };

            props[35] = new PropertyState { TileIndex = 35, type = PropertyType.RailRoad, RailRoadBuyPrice = 200 };

            props[37] = new PropertyState { TileIndex = 37, type = PropertyType.Property, landPrice = 350, housePrice = 170, hotelPrice = 200, rentPrice = new[] { 35, 175, 500, 1100, 1300, 1500 } };
            props[39] = new PropertyState { TileIndex = 39, type = PropertyType.Property, landPrice = 400, housePrice = 200, hotelPrice = 200, rentPrice = new[] { 50, 200, 600, 1400, 1700, 2000 } };


            return props;
        }

    }
}
