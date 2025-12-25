using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class PlayerReleasedFromJailEvent
    {
        public int PlayerId { get; set; }
        public string Method { get; set; }      // PayMoney | UseCard | RollDice | ForcedRelease
        public string Message { get; set; }     // Thông báo
    }
}
