using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.GameState
{
    public class PlayerState
    {
        public int AccountId { get; set; }

        public int PlayerId { get; set; }

        //Chọn nhân vật
        public int CharacterIndex { get; set; }
        // vị trí trên bàn cờ
        public int Position { get; set; } = 0;

        // tiền hiện tại
        public int Money { get; set; } = 1500;

        // trạng thái
        public bool IsBankrupt { get; set; }
        public bool InJail { get; set; }
        
        // ✅ SỐ LƯỢT CÒN LẠI TRONG TÙ (0 = không trong tù, 1-3 = số lượt còn lại)
        public int JailTurnsRemaining { get; set; } = 0;

        public bool hasGetOutOfJailCard {  get; set; }

        public int RailRoadCount { get; set; }

        public int UtilityCount { get; set;}
    }
}
