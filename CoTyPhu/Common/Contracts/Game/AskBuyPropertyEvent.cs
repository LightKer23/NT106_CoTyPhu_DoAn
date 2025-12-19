using Common.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class AskBuyPropertyEvent
    {
        public Property Tile { get; set; }  // thông tin ô đất
    }
}
