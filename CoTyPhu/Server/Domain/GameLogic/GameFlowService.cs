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
        public TileType HandleProperty(MatchState match, PlayerState player, Tile tile)
        {

            if (tile is PropertyTile pTile)
            {
                // Chưa có chủ → hỏi mua
                if (pTile.PlayerOwnerId == null)
                {
                    match.WaitingForBuyDecision = true;
                    match.PendingTileIndex = GetCurrentTileIndex(player);
                    return TileType.Property;
                }

                // Đất của người khác → trả tiền thuê
                if (pTile.PlayerOwnerId != player.PlayerId)
                {
                    // Chưa có chủ → hỏi mua
                    if (pTile.PlayerOwnerId == null)
                    {
                        match.WaitingForBuyDecision = true;
                        match.PendingTileIndex = GetCurrentTileIndex(player);
                        return TileType.Property;
                    }

                    // Đất của người khác → trả tiền thuê
                    if (pTile.PlayerOwnerId != player.PlayerId)
                    {
                        PayRent(match, player, pTile);
                    }

                    return TileType.Property;

                }

                return TileType.Property;
            }

            return TileType.Property;
        }

        public bool BuyTile(MatchState match, PlayerState player)
        {
            int tileIndex = player.Position;
            Tile tile = match.Board[tileIndex];

            // Không ở trạng thái chờ mua
            if (!match.WaitingForBuyDecision)
                return false;

            // Không đủ tiền hoặc đã có chủ
            if (tile is PropertyTile pTile)
            {
                if (pTile.PlayerOwnerId != null || player.Money < pTile.landPrice)
                    return false;

                // Trừ tiền & gán chủ
                DeductMoney(player, pTile.landPrice);
                pTile.PlayerOwnerId = player.PlayerId;

                return true;
            }

            if (tile is RailRoadTile rrTile)
            {
                if (rrTile.PlayerOwnerId != null || player.Money < rrTile.buyPrice)
                    return false;

                DeductMoney(player, rrTile.buyPrice);
                rrTile.PlayerOwnerId = player.PlayerId;

                player.RailRoadCount++;
                return true;
            }

            if (tile is UtilityTile uTile)
            {
                if (uTile.PlayerOwnerId != null || player.Money < uTile.buyPrice)
                    return false;

                DeductMoney(player, uTile.buyPrice);
                uTile.PlayerOwnerId = player.PlayerId;

                player.UtilityCount++;
                return true;
            }

            return false;
        }


        //Tính tiền thuê
        private int getRentPrice(PlayerState player, Tile tile)
        {
            int RentPrice = 0;
            if(tile is PropertyTile pTile)
            {
                if(pTile.hasHotel)
                {
                    RentPrice = pTile.rentPrice[pTile.houseCount + 1];
                }
                else RentPrice = pTile.rentPrice[pTile.houseCount];
            }
            else if(tile is RailRoadTile railRoad)
            {
                switch(player.RailRoadCount)
                {
                    case 1:
                        {
                            RentPrice = 25;
                            break;
                        }
                    case 2:
                        {
                            RentPrice = 50;
                            break;
                        }
                    case 3:
                        {
                            RentPrice = 100;
                            break;
                        }
                    case 4:
                        {
                            RentPrice = 200;
                            break;
                        }
                }    
            }
            else if(tile is UtilityTile uTile)
            {
                DiceService diceService = new DiceService();
                (int total, bool isDouble) result = diceService.RollDice();
                int RollResult = result.total;
                switch (player.UtilityCount)
                {
                    case 1:
                        {
                            RentPrice = RollResult * 4;
                            break;
                        }
                    case 2:
                        {
                            RentPrice = RollResult * 4;
                            break;
                        }
                }    
            }    
                return RentPrice;
        }

        //Tính tiền thuê Công ty (trường hợp lá Cơ hội đến công ty gần nhất)
        private int getUtilityRentPrice(PlayerState player, Tile tile)
        {
            int RentPrice = 0;
            if (tile is UtilityTile uTile)
            {
                DiceService diceService = new DiceService();
                (int total, bool isDouble) result = diceService.RollDice();
                int RollResult = result.total;
                RentPrice = RollResult * 10;
            }
            return RentPrice;
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
        public void PayRent(MatchState match, PlayerState player, Tile tile)
        {
            int rentPrice = getRentPrice(player, tile);

            DeductMoney(player, rentPrice);

            if (tile is PropertyTile pTile)
            {
                var owner = match.Players[pTile.PlayerOwnerId.Value];
                AddMoney(owner, rentPrice);
            }
            else if(tile is RailRoadTile rrTile)
            {
                var owner = match.Players[rrTile.PlayerOwnerId.Value];
                AddMoney(owner, rentPrice);
            }
            else if(tile is UtilityTile uTile)
            {
                var owner = match.Players[uTile.PlayerOwnerId.Value];
                AddMoney(owner, rentPrice);
            }
        }

        // Trả tiền thuê có hệ số (Cơ hội, Khí vận)
        public void PayRent(MatchState match, PlayerState player, Tile tile, int multiplier)
        {
            int rentPrice = getRentPrice(player, tile) * multiplier;

            DeductMoney(player, rentPrice);

            if (tile is PropertyTile pTile)
            {
                var owner = match.Players[pTile.PlayerOwnerId.Value];
                AddMoney(owner, rentPrice);
            }
            else if (tile is RailRoadTile rrTile)
            {
                var owner = match.Players[rrTile.PlayerOwnerId.Value];
                AddMoney(owner, rentPrice);
            }
            else if (tile is UtilityTile uTile)
            {
                rentPrice = getUtilityRentPrice(player, tile);
                var owner = match.Players[uTile.PlayerOwnerId.Value];
                AddMoney(owner, rentPrice);
            }
        }



        //Kiểm tra phá sản
        public void HandleBankrupt(MatchState match, PlayerState player)
        {
            player.IsBankrupt = true;
            player.Money = 0;

            foreach (var tile in match.Board)
            {
                if (tile is PropertyTile p && p.PlayerOwnerId == player.PlayerId)
                    p.PlayerOwnerId = null;

                if (tile is RailRoadTile r && r.PlayerOwnerId == player.PlayerId)
                    r.PlayerOwnerId = null;

                if (tile is UtilityTile u && u.PlayerOwnerId == player.PlayerId)
                    u.PlayerOwnerId = null;
            }
        }

        private bool IsOwner(PlayerState player, Tile tile)
        {
            return tile switch
            {
                PropertyTile p => p.PlayerOwnerId == player.PlayerId,
                RailRoadTile r => r.PlayerOwnerId == player.PlayerId,
                UtilityTile u => u.PlayerOwnerId == player.PlayerId,
                _ => false
            };
        }

        public void SellSingleTile(MatchState match, PlayerState player, Tile tile)
        {
            if (tile is PropertyTile pTile)
            {
                pTile.PlayerOwnerId = null;
                
                if(pTile.hasHotel)
                {
                    AddMoney(player, (pTile.hotelPrice / 2));
                    pTile.hasHotel = false;
                }
                AddMoney(player, ((pTile.housePrice * pTile.houseCount) / 2));
                pTile.houseCount = 0;
            }
            else if (tile is RailRoadTile rrTile)
            {
                rrTile.PlayerOwnerId = null;
                AddMoney(player, rrTile.sellPrice);
            }
            else if (tile is UtilityTile uTile)
            {
                uTile.PlayerOwnerId = null;
                AddMoney(player, uTile.sellPrice);              
            }
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
    }
}
