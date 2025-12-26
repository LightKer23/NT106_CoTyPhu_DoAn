using Common.Domain.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.GameState.Board
{
    public static class ChanceDeckLoader
    {
        public static List<Card> LoadDefaultDeck()
        {
            var chanceDeck = new List<Card>()
            {
                new Card("Tu do ra tu", ChanceCardType.GetOutOfJailFree),
                new Card("Sua chua nha cua", ChanceCardType.StreetRepairs),
                new Card("Chay qua toc do", ChanceCardType.PayMoney)
                {
                    Amount = 15
                },
                new Card("Duoc bau lam giam doc", ChanceCardType.PayEachPlayer)
                {
                    Amount = 50
                },
                new Card("Di lui 3 buoc", ChanceCardType.MoveBackSpaces)
                {
                    MoveBackSteps = 3
                },
                new Card("Tien den cong ty gan nhat", ChanceCardType.MoveToNearestUtility),
                new Card("Ngan hang tra lai", ChanceCardType.EarnMoney)
                {
                    Amount = 50
                },
                new Card("Ke gian moc tui", ChanceCardType.PayMoney)
                {
                    Amount = 15
                },
                new Card("Di den o Nguyen Tri Phuong", ChanceCardType.MoveToTile)
                {
                    MoveToTileIndex = 24
                },
                new Card("Gap lai co nhan", ChanceCardType.EarnMoney)
                {
                    Amount = 150
                },
                new Card("Di den o Nguyen Tat Thanh", ChanceCardType.MoveToTile)
                {
                    MoveToTileIndex = 11
                },
                new Card("Vao tu. Di thang vao tu", ChanceCardType.GoToJail),
                new Card("Tien den ben xe gan nhat", ChanceCardType.MoveToNearestRailroad),
                new Card("Tien den ben xe gan nhat", ChanceCardType.MoveToNearestRailroad),
                new Card("Di den o Tan Ky Tan Quy", ChanceCardType.MoveToTile)
                {
                    MoveToTileIndex = 39
                },
                new Card("Den o Ben xe Can Giuoc", ChanceCardType.MoveToTile)
                {
                    MoveToTileIndex = 5
                }
            };

            return chanceDeck;
        }
    }
}
