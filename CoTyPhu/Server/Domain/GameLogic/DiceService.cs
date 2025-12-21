using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.Game;

namespace Server.Domain.GameLogic
{
        public class DiceService
        {
            private readonly Dice dice1 = new Dice();
            private readonly Dice dice2 = new Dice();

            //Tổng số bước và kiểm tra có cặp hay không
            public (int total, bool isDouble) RollDice()
            {
                int d1 = dice1.faceValue[RandomNumber()];
                int d2 = dice2.faceValue[RandomNumber()];

                bool isDouble = (d1 == d2);

                return (d1 + d2, isDouble);
            }

            private int RandomNumber()
            {
                return Random.Shared.Next(0, 6);
            }
        }
}
