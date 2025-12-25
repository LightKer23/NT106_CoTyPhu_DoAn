using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class PlayerMoveEvent
    {
        public int PlayerId { get; set; }
        public int Roll1 { get; set; }
        public int Roll2 { get; set; }
        
        // ✅ THÊM FROM/TO TILE ĐỂ HỖ TRỢ CARD MOVEMENT ANIMATION
        public int? FromTile { get; set; }      // Vị trí trước khi di chuyển (null = tính từ current)
        public int? ToTile { get; set; }        // Vị trí sau khi di chuyển (null = tính từ Roll1+Roll2)
    }

}
