using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Game
{
    public class RollDiceRequest
    {
        public int MatchID { get; set; }
        public int PlayerID { get; set; }
        public int Roll1 { get; set; }
        public int Roll2 { get; set; }
    }
}
