using Common.Constracts;
using Server.Domain.GameState;
using System.Text.Json;
using Common.Domain.Game.Enums;
using System.Linq;
using Common.Contracts.Game;

namespace Server.Domain.GameLogic
{
    public class GameFlowService
    {
        private static readonly JsonSerializerOptions JsonOpt = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        //Lấy vị trí ô đất hiện tại của người chơi
        public int GetCurrentTileIndex(PlayerState player)
        {
            return player.Position;
        }

        //Xử lý ô đất
        private TileType HandleProperty(
            MatchState match,
            PlayerState player,
            Tile tile)
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

        //Cộng / Trừ tiền
        public void ChangeMoney(PlayerState player, int amount)
        {
            player.Money += amount;
        }

        //Trả tiền thuê
        public void PayRent(MatchState match, PlayerState player, Tile tile)
        {
            int rentPrice = getRentPrice(player, tile);

            player.Money -= rentPrice;

            if (tile is PropertyTile pTile)
            {
                var owner = match.Players[pTile.PlayerOwnerId.Value];
                owner.Money += rentPrice;
            }
            else if(tile is RailRoadTile rrTile)
            {
                var owner = match.Players[rrTile.PlayerOwnerId.Value];
                owner.Money += rentPrice;
            }
            else if(tile is UtilityTile uTile)
            {
                var owner = match.Players[uTile.PlayerOwnerId.Value];
                owner.Money += rentPrice;
            }
            

        }

        //Kiểm tra phá sản
        public bool CheckBankrupt(PlayerState player)
        {
            if (player.Money < 0)
            {
                player.IsBankrupt = true;
                player.Money = 0;
                return true;
            }
            return false;
        }

        public void sellTile(PlayerState player, Tile tile)
        {
            if (tile is PropertyTile pTile)
            {
                pTile.PlayerOwnerId = null;
                
                if(pTile.hasHotel)
                {
                    player.Money += (pTile.hotelPrice / 2);
                    pTile.hasHotel = false;
                }
                player.Money += ((pTile.housePrice * pTile.houseCount) / 2);
                pTile.houseCount = 0;
            }
            else if (tile is RailRoadTile rrTile)
            {
                rrTile.PlayerOwnerId = null;
                player.Money += rrTile.sellPrice;
            }
            else if (tile is UtilityTile uTile)
            {
                uTile.PlayerOwnerId = null;
                player.Money += uTile.sellPrice;
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

        //Gửi Event di chuyển
        public MessageEnvelope SendPlayerMovedEvent(int matchId, int playerId, int newPos)
        {
            var movedEvent = new PlayerMovedEvent()
            {
                MatchId = matchId,
                PlayerId = playerId,
                NewPosition = newPos
            };

            return Wrap(MessageType.PlayerMovedEvent, movedEvent);
        }

        private static MessageEnvelope Wrap<T>(MessageType type, T body)
        => new MessageEnvelope
        {
            Type = type,
            Payload = JsonSerializer.Serialize(body, JsonOpt)
        };
    }
}
