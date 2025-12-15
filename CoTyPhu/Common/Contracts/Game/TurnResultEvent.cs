using Common.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Game
{
    public class TurnResultEvent
    {
        public int PlayerID { get; set; }   // player đang / vừa chơi

        // Các thực thể đã thay đổi
        public List<Player> UpdatedPlayers { get; set; }      // ví dụ: người trả tiền, người nhận tiền, người phá sản
        public List<Property> UpdatedProperties { get; set; } // đất đổi chủ, đổi level, reset,...
        public Match UpdatedMatch { get; set; }               // Turn, Status, NumberPlayer...

    }
}
