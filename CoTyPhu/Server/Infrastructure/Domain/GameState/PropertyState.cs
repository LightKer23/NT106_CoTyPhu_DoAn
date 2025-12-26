using Common.Domain.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Game.Enums
{
    public enum PropertyType
    {
        Property,   // đất xây nhà / khách sạn
        RailRoad,   // ga tàu
        Utility     // công ty điện / nước
    }
}

namespace Server.Domain.GameState
{
    public class PropertyState
    {
        // vị trí trên bàn cờ
        public int TileIndex { get; set; }

        // loại tài sản
        public PropertyType type { get; set; }

        // null = chưa mua
        public int? PlayerOwnerId { get; set; }

        public int houseCount { get; set; } = 0;
        public bool hasHotel { get; set; } = false;
         
        // giá mua đất
        public int landPrice { get; set; }

        // giá mua mỗi nhà
        public int housePrice { get; set; }

        // giá mua khách sạn
        public int hotelPrice { get; set; }

        // tiền thuê theo số lượng nhà
        public int[] rentPrice { get; set; }


        // giá mua bến xe
        public int RailRoadBuyPrice { get; set; }

        // giá bán bến xe
        public int RailRoadSellPrice { get; set; }

        public int[] RailRoadRentPrice = { 25, 50, 100, 200 };

        // giá mua công ty
        public int UtilityBuyPrice { get; set; }

        // giá bán công ty
        public int UtilitySellPrice { get; set; }

        public int[] UtilityMultiply = { 4, 10 }; 
    }
}



