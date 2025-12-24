using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class PropertyOwnershipChangedEvent
    {
        public int TileIndex { get; set; }              // Vị trí ô đất (1-39)
        public int? OwnerId { get; set; }               // PlayerId của chủ (null = chưa có chủ)
        public int HouseCount { get; set; }             // Số nhà (0-4)
        public bool HasHotel { get; set; }              // Có khách sạn không
        public string PropertyType { get; set; }        // "Property", "RailRoad", "Utility"
    }
}
