using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.Game.Enums;

namespace Server.Domain.GameState.Board
{
    public static class CommunityChestDeckLoader
    {
        public static List<Card> LoadDefaultDeck()
        {
            var communityChestDeck = new List<Card>()
            {
                new Card("Tu do ra tu", CommunityChestCardType.GetOutOfJailFree),
                new Card("Dat giai nhi trong cuoc thi sac dep", CommunityChestCardType.EarnMoney)
                {
                    Amount = 10
                },
                new Card("Ngan hang lon tien", CommunityChestCardType.EarnMoney)
                {
                    Amount = 200
                },
                new Card("Ban co phieu", CommunityChestCardType.EarnMoney)
                {
                    Amount = 50
                },
                new Card("Bao hiem dao han", CommunityChestCardType.EarnMoney)
                {
                    Amount = 100
                },
                new Card("Hoan the thu nhap", CommunityChestCardType.EarnMoney)
                {
                    Amount = 20
                },
                new Card("Qua giang sinh", CommunityChestCardType.EarnMoney)
                {
                    Amount = 100
                },
                new Card("Lanh tien thua ke", CommunityChestCardType.EarnMoney)
                {
                    Amount = 100
                },
                new Card("Lam them gio", CommunityChestCardType.EarnMoney)
                {
                    Amount = 25
                },
                new Card("Tra tien vien phi", CommunityChestCardType.PayMoney)
                {
                    Amount = 100
                },
                new Card("Thanh toan hoc phi", CommunityChestCardType.PayMoney)
                {
                    Amount = 50
                },
                new Card("Tra tien kham benh", CommunityChestCardType.PayMoney)
                {
                    Amount = 50
                },
                new Card("Qua sinh nhat", CommunityChestCardType.CollectFromEachPlayer)
                {
                    Amount = 10
                },
                new Card("Di den o bat dau", CommunityChestCardType.MoveToTile)
                {
                    MoveToTileIndex = 0
                },
                new Card("Sua chua duong pho", CommunityChestCardType.StreetRepairs),
                new Card("Vao tu. Di thang vao tu", CommunityChestCardType.GoToJail)
            };

            return communityChestDeck;
        }
    }
}
