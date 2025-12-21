using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.GameState
{
    public class PropertyState
    {
        public int TileIndex { get; set; }
        public int? OwnerPlayerId { get; set; } // null = chưa mua
        public int Level { get; set; } = 0;     // nhà / khách sạn
    }
}
