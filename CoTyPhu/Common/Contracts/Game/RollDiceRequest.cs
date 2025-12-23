using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class RollDiceRequest
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
    }
}
