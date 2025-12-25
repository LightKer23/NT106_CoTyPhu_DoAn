using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class PropertyUpdatedEvent
    {
        public int PlayerId { get; set; }

        public int? PropertyTileIndex { get; set; }
        
        // ✅ THÊM MESSAGE ĐỂ HIỂN THỊ CHO USER
        public string? Message { get; set; }
        public bool Success { get; set; } = true;
    }
}
