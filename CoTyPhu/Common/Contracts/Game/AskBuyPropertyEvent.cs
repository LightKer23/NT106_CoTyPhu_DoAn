using Common.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Game
{
    public class AskBuyPropertyEvent
    {
        public int MatchID { get; set; }
        public int PlayerID { get; set; }

        public Property Tile { get; set; }  // thông tin ô đất
    }
}
