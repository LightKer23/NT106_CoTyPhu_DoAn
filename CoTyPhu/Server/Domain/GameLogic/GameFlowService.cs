using Common.Constracts;
using Common.Contracts.Game;
using Common.Domain.Game.Enums;
using Server.Domain.GameState;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Server.Domain.GameLogic
{
    public class GameFlowService
    {
        CardService cardService = new CardService();
        //Lấy vị trí ô đất hiện tại của người chơi
        public int GetCurrentTileIndex(PlayerState player)
        {
            return player.Position;
        }

        //Trả về loại đất
        public PropertyType? GetTilePropertyKind(MatchState match, PlayerState player)
        {
            int tileIndex = player.Position;

            // Không phải ô tài sản
            if (!match.Properties.TryGetValue(tileIndex, out var property))
                return null;

            // Trả về đúng enum sẵn có
            return property.type;
        }

        //Kiểm tra đã có chủ chưa
        public bool HasOwner(PropertyState property)
        {
            return property.PlayerOwnerId != null;
        }

        public bool HandleOwnedProperty(MatchState match, PlayerState player)
        {
            int tileIndex = player.Position;
            
            //Kiểm tra có phải ô đất không
            if (!match.Properties.TryGetValue(tileIndex, out var property))
                return true;

            //Ô đất của mình, không làm gì
            if (property.PlayerOwnerId == player.PlayerId)
                return true;

            //Ô của người khác thì trả tiền thuê
            PayRent(match, player, property);
            return false;
        }

        //Mua đất
        public bool BuyTile(MatchState match, PlayerState player)
        {
            int tileIndex = player.Position;

            if (match.Properties.TryGetValue(tileIndex, out var property))
            {
                if (!match.WaitingForBuyDecision)
                    return false;


            if (property.PlayerOwnerId != null)
                    return false;

                int price = property.type switch
                {
                    PropertyType.Property => property.landPrice,
                    PropertyType.RailRoad => property.RailRoadBuyPrice,
                    PropertyType.Utility => property.UtilityBuyPrice,
                    _ => 0
                };

                if (player.Money < price)
                    return false;

                DeductMoney(player, price);
                property.PlayerOwnerId = player.PlayerId;

                if (property.type == PropertyType.RailRoad || player.RailRoadCount < 5)
                    player.RailRoadCount++;

                if (property.type == PropertyType.Utility || player.UtilityCount < 3)
                    player.UtilityCount++;

                return true;

            }
            else
            {
                return false;
            }
        }

        //Xử lý khi người chơi dừng ở một ô
        public void HandlePlayerLanded(MatchState match, PlayerState player)
        {
            int tileIndex = player.Position;

            // 1. Lấy loại ô (tĩnh)
            TileType tileType = ServerState.Board[tileIndex].type;

            switch (tileType)
            {
                case TileType.Start:
                    {
                        // Không làm gì khi chỉ "đứng" vào Start
                        return;
                    }
                    

                case TileType.GoToJail:
                    {
                        SendPlayerToJail(match, player);
                        return;
                    }
                    

                case TileType.Tax:
                    {
                        HandleTax(match, player, tileIndex);
                        return;
                    }
                    

                case TileType.Chance:
                    {
                        HandleChance(match, player);
                        return;
                    }
                    

                case TileType.CommunityChest:
                    {
                        HandleCommunityChest(match, player);
                        return;
                    }
                    

                case TileType.Property:
                case TileType.Railroad:
                case TileType.Utility:
                    {
                        HandlePropertyTile(match, player, tileIndex);
                        return;
                    }
                    

                default:
                    return;
            }
        }

        //Xử lý ô đất
        private void HandlePropertyTile(MatchState match, PlayerState player, int tileIndex)
        {
            var property = match.Properties[tileIndex];

            // Đã có chủ → trả tiền thuê (hoặc không)
            bool noRentPaid = HandleOwnedProperty(match, player);
            if (!noRentPaid)
                return;

            // Chưa có chủ → hỏi mua
            if (!HasOwner(property))
            {
                match.WaitingForBuyDecision = true;
                match.PendingTileIndex = tileIndex;
            }
        }


        //Tính tiền thuê
        private int GetRentPrice(PlayerState player, PropertyState property)
        {
            switch (property.type)
            {
                case PropertyType.Property:
                    {
                        if (property.hasHotel)
                            return property.rentPrice[property.houseCount + 1];

                        return property.rentPrice[property.houseCount];
                    }


                case PropertyType.RailRoad:
                    {
                        return property.RailRoadRentPrice[player.RailRoadCount - 1];
                    }                
                    

                case PropertyType.Utility:
                    {
                        DiceService dice = new DiceService();
                        var (total, _) = dice.RollDice();
                        return total * property.UtilityMultiply[player.UtilityCount - 1];
                    }

            }

            return 0;
        }

        //Cộng tiền
        public void AddMoney(PlayerState player, int amount)
        {
            player.Money += amount;
        }

        public void DeductMoney(PlayerState player, int amount)
        {
            player.Money -= amount;
        }

        //Trả tiền thuê
        public void PayRent(MatchState match, PlayerState player, PropertyState property)
        {
            var owner = match.Players[property.PlayerOwnerId.Value];
            int rent = GetRentPrice(owner, property);

            DeductMoney(player, rent);
            AddMoney(owner, rent);
        }

        //Kiểm tra phá sản
        public void HandleBankrupt(MatchState match, PlayerState player)
        {
            player.IsBankrupt = true;
            player.Money = 0;

            foreach (var kv in match.Properties)
            {
                var property = kv.Value;

                if (property.PlayerOwnerId == player.PlayerId)
                {
                    property.PlayerOwnerId = null;
                }
            }
        }


        //Bán một ô đất
        public void SellSingleProperty(MatchState match, PlayerState player, PropertyState property)
        {
            if (property.PlayerOwnerId != player.PlayerId)
                return;

            property.PlayerOwnerId = null;

            switch (property.type)
            {
                case PropertyType.Property:
                    if (property.hasHotel)
                    {
                        AddMoney(player, property.hotelPrice / 2);
                        property.hasHotel = false;
                    }

                    AddMoney(player, (property.housePrice * property.houseCount) / 2);
                    property.houseCount = 0;
                    break;

                case PropertyType.RailRoad:
                    player.RailRoadCount--;
                    AddMoney(player, property.RailRoadBuyPrice / 2);
                    break;

                case PropertyType.Utility:
                    player.UtilityCount--;
                    AddMoney(player, property.UtilityBuyPrice / 2);
                    break;
            }
        }

        //Đi thẳng vào tù
        private void SendPlayerToJail(MatchState match, PlayerState player)
        {
            // Ô Jail mặc định index = 10
            player.Position = 10;
            player.InJail = true;
        }

        //Trả tiền thuế
        private void HandleTax(MatchState match, PlayerState player, int tileIndex)
        {
            var tile = ServerState.Board[tileIndex];

            if (tile is TaxTile taxTile)
            {
                DeductMoney(player, taxTile.taxAmount);
                HandleBankrupt(match, player);
            }
        }

        //Xử lý lá bài cơ hội
        private void HandleChance(MatchState match, PlayerState player)
        {
            cardService.DrawChanceCard(match, player, ServerState.chanceDeck);
        }


        //Xử lý lá bài Khí vận
        private void HandleCommunityChest(MatchState match, PlayerState player)
        {
            cardService.DrawCommunityChestCard(match, player, ServerState.communityChestDeck);
        }


        //Bán thẻ ra khỏi tù
        public bool SellGetOutOfJailCard(PlayerState player)
        {
            if (!player.hasGetOutOfJailCard)
                return false;

            player.hasGetOutOfJailCard = false;
            AddMoney(player, 200);

            return true;
        }


        //Chuyển lượt
        public void NextTurn(MatchState match)
        {
            int totalPlayers = match.Players.Count;

            do
            {
                match.CurrentPlayerIndex = (match.CurrentPlayerIndex + 1) % totalPlayers;
            } while (match.Players[match.CurrentPlayerIndex].IsBankrupt);
        }

        public PropertyState? ConvertTiletoPropertyState(Tile tile, int tileIndex)
        {
            if (tile is PropertyTile p)
            {
                return new PropertyState
                {
                    TileIndex = tileIndex,
                    type = PropertyType.Property,

                    landPrice = p.landPrice,
                    housePrice = p.housePrice,
                    hotelPrice = p.hotelPrice,
                    rentPrice = p.rentPrice.ToArray()
                };
            }

            if (tile is RailRoadTile r)
            {
                return new PropertyState
                {
                    TileIndex = tileIndex,
                    type = PropertyType.RailRoad,

                    RailRoadBuyPrice = r.buyPrice,
                    RailRoadSellPrice = r.sellPrice
                };
            }

            if (tile is UtilityTile u)
            {
                return new PropertyState
                {
                    TileIndex = tileIndex,
                    type = PropertyType.Utility,

                    UtilityBuyPrice = u.buyPrice,
                    UtilitySellPrice = u.sellPrice
                };
            }

            // Không phải ô mua được
            return null;
        }

    }
}
