using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Game
{
    public class Turn
    {
        public int TurnNumber { get; set; }
        public int PlayerId { get; set; }

        public int Dice1 { get; set; }
        public int Dice2 { get; set; }

        public int StartPosition { get; set; }
        public int EndPosition { get; set; }

        public List<string> Actions { get; set; } = new();
    }
}
