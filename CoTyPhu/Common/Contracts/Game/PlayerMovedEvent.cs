using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class PlayerMoveEvent
    {
        public int PlayerId { get; set; }
        public int Roll1 { get; set; }
        public int Roll2 { get; set; }
    }

}
