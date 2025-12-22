using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class PlayerMovedEvent
    {
        public int PlayerId { get; set; }
        public int NewPosition { get; set; }
    }
}
