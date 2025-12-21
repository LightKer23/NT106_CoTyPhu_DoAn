using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.GameState
{
    public class PlayerState
    {
        public int PlayerId { get; set; }

        // vị trí trên bàn cờ
        public int Position { get; set; } = 0;

        public int CharacterIndex { get; set; } = 0;
        // tiền hiện tại
        public int Money { get; set; } = 1500;

        // trạng thái
        public bool IsBankrupt { get; set; }
        public bool InJail { get; set; }

        // số lượt ở tù (optional)
        public int JailTurns { get; set; }
    }
}
