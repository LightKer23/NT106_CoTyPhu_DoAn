using System;
using System.Collections.Generic;
using System.Linq;
namespace Common.Domain.Game
{
    public class Dice
    {
        public int Dice1 { get; private set; }
        public int Dice2 { get; private set; }

        public int Total => Dice1 + Dice2;
        public bool IsDouble => Dice1 == Dice2;

        private readonly Random _random = new Random();

        public void Roll()
        {
            Dice1 = _random.Next(1, 7);
            Dice2 = _random.Next(1, 7);
        }
    }
}
