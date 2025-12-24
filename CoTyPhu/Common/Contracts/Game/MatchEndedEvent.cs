using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class MatchEndedEvent
    {
        public int WinnerPlayerId { get; set; }
        public string Name { get; set; }
    }
}
