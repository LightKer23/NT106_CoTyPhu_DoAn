using Server.Domain.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.Game.Enums;

namespace Server.Domain.GameLogic
{
    public class CardService
    {
        private GameFlowService flowService;

        //Bán thẻ ra tù
        public void SellGetOutOfJailCard(PlayerState player)
        {
            bool hasGetOutOfJailCard = player.hasGetOutOfJailCard;
            if (hasGetOutOfJailCard)
            {
                player.hasGetOutOfJailCard = false;
                flowService.AddMoney(player, 200);
            }
        }

        //Tạo index rút bài
        public int randomCardIndex()
        {
            return Random.Shared.Next(0, 16);
        }

        //Rút một lá bài từ bộ bài Khí vận
        public void DrawCard(MatchState match, PlayerState player, CommunityChestDeckState communityChest)
        {
            int cardIndex = randomCardIndex();
            var card = communityChest.communityChestDeck[cardIndex];
            applyEffectPlayer(match, player, card);
        }

        //Rút một lá bài từ bộ bài Cơ hội
        public void DrawCard(MatchState match, PlayerState player, ChanceDeckState chanceChest)
        {
            int cardIndex = randomCardIndex();
            var card = chanceChest.chanceDeck[cardIndex];
            applyEffectPlayer(match, player, card);
        }

        public void applyEffectPlayer(MatchState match, PlayerState player, Card card)
        {
            if (card.Type == CardType.Chance)
            {
                switch (card.ChanceType)
                {
                    case ChanceCardType.EarnMoney:
                        {
                            flowService.AddMoney(player, card.Amount);
                            break;
                        }

                    case ChanceCardType.PayMoney:
                        {
                            flowService.DeductMoney(player, card.Amount);
                            break;
                        }

                    case ChanceCardType.GetOutOfJailFree:
                        {
                            player.hasGetOutOfJailCard = true;
                            break;
                        }
                    case ChanceCardType.PayEachPlayer:
                        {
                            foreach (var otherPlayer in match.Players.Values)
                            {
                                if (otherPlayer.PlayerId == player.PlayerId)
                                    continue;

                                if (otherPlayer.IsBankrupt)
                                    continue;

                                flowService.DeductMoney(player, card.Amount);
                                flowService.AddMoney(otherPlayer, card.Amount);
                            }
                            break;
                        }
                    case ChanceCardType.MoveToTile:
                        {
                            int oldPos = player.Position;
                            int newPos = card.MoveToTileIndex;

                            // đi qua GO
                            if (newPos < oldPos)
                            {
                                flowService.AddMoney(player, 200);
                            }

                            // cập nhật vị trí
                            player.Position = newPos;

                            // xử lý ô vừa đến (theo đúng HandleProperty hiện tại)
                            Tile tile = match.Board[player.Position];
                            flowService.HandleProperty(match, player, tile);

                            break;
                        }

                    case ChanceCardType.MoveBackSpaces:
                        {
                            int boardSize = match.Board.Count;

                            int newPos = player.Position - card.MoveBackSteps;

                            // lùi quá 0 → vòng bàn cờ
                            if (newPos < 0)
                            {
                                newPos += boardSize;
                            }

                            // cập nhật vị trí
                            player.Position = newPos;

                            // xử lý ô vừa đến
                            Tile tile = match.Board[player.Position];
                            flowService.HandleProperty(match, player, tile);

                            break;
                        }

                    case ChanceCardType.GoToJail:
                        {
                            player.Position = 10;
                            player.InJail = true;
                            break;
                        }

                    case ChanceCardType.MoveToNearestRailroad:
                        {
                            int[] railroadIndexes = { 5, 15, 25, 35 };

                            int currentPos = player.Position;
                            int newPos = -1;

                            foreach (int rr in railroadIndexes)
                            {
                                if (rr > currentPos)
                                {
                                    newPos = rr;
                                    break;
                                }
                            }

                            //Đi qua ô bắt đầu
                            if (newPos == -1)
                            {
                                newPos = railroadIndexes[0];
                                flowService.AddMoney(player, 200);
                            }

                            player.Position = newPos;

                            Tile tile = match.Board[newPos];

                            //Nếu Railroad đã có chủ x2 tiền thuê
                            if (tile is RailRoadTile rrTile &&
                                rrTile.PlayerOwnerId != null &&
                                rrTile.PlayerOwnerId != player.PlayerId)
                            {
                                flowService.PayRent(match, player, rrTile, 2);
                            }
                            else
                            {
                                flowService.HandleProperty(match, player, tile);
                            }

                            break;
                        }

                    case ChanceCardType.MoveToNearestUtility:
                        {
                            int[] utilityIndexes = { 12, 28 };

                            int currentPos = player.Position;
                            int newPos = -1;

                            foreach (int u in utilityIndexes)
                            {
                                if (u > currentPos)
                                {
                                    newPos = u;
                                    break;
                                }
                            }

                            //Đi qua ô bắt đầu
                            if (newPos == -1)
                            {
                                newPos = utilityIndexes[0];
                                flowService.AddMoney(player, 200);
                            }

                            // cập nhật vị trí
                            player.Position = newPos;

                            Tile tile = match.Board[newPos];

                            // nếu Utility đã có chủ dice x10
                            if (tile is UtilityTile uTile &&
                                uTile.PlayerOwnerId != null &&
                                uTile.PlayerOwnerId != player.PlayerId)
                            {
                                flowService.PayRent(match, player, uTile, 10);
                            }
                            else
                            {
                                flowService.HandleProperty(match, player, tile);
                            }

                            break;
                        }

                    case ChanceCardType.StreetRepairs:
                        {
                            int totalCost = 0;

                            foreach (var tile in match.Board)
                            {
                                if (tile is PropertyTile pTile && pTile.PlayerOwnerId == player.PlayerId)
                                {
                                    // 25 Đô mỗi nhà
                                    totalCost += pTile.houseCount * 25;

                                    // 100 Đô mỗi khách sạn
                                    if (pTile.hasHotel)
                                    {
                                        totalCost += 100;
                                    }
                                }
                            }

                            flowService.DeductMoney(player, totalCost);
                            flowService.CheckBankrupt(player);

                            break;
                        }
                }

            }
        }
    }
}
