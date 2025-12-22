using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Room
{
    public class StateRoomEvent
    {
        public int MatchId { get; set; }
        public List<int> TakenCharacters { get; set; } = new();
    }
}
