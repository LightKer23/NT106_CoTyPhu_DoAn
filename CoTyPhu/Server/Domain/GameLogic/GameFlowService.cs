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
        //Lấy vị trí ô đất hiện tại của người chơi
        public int GetCurrentTileIndex(PlayerState player)
        {
            return player.Position;
        }

        //Xử lý ô đất
        public PropertyType HandleProperty(MatchState match, PlayerState player, PropertyState property)
        {
            // Chưa có chủ → hỏi mua
            if (property.PlayerOwnerId == null)
            {
                match.WaitingForBuyDecision = true;
                match.PendingTileIndex = property.TileIndex;
                return property.type;
            }

            // Đất của người khác → trả tiền thuê
            if (property.PlayerOwnerId != player.PlayerId)
            {
                PayRent(match, player, property);
                return property.type;
            }

            // Đất của mình → không làm gì
            return property.type;
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
