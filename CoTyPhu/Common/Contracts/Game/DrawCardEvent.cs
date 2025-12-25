using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class DrawCardEvent
    {
        public int PlayerId { get; set; }
        public string CardType { get; set; }  // "Chance" hoặc "CommunityChest"
        public int CardIndex { get; set; }     // 1-16
        public string Description { get; set; }  // Mô tả thẻ
    }
}
