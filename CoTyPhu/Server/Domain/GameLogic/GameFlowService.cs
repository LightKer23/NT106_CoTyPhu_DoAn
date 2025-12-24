using Common.Constracts;
using Common.Contracts.Game;
using Common.Domain.Game.Enums;
using Server.Domain.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.Domain.GameLogic
{
    public class GameFlowService
    {
        private readonly CardService _cardService;

        // ✅ CALLBACK async để hỗ trợ delay
        public Func<MatchState, int, string, int, string, Task>? OnDrawCard { get; set; }

        public GameFlowService()
        {
            _cardService = new CardService(this);
        }

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

            if(property.PlayerOwnerId == null)
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

            if (!match.Properties.TryGetValue(tileIndex, out var property))
            {
                return false;
            }

            if (match.PendingTileIndex.HasValue && match.PendingTileIndex.Value != tileIndex)
            {
                return false;
            }

            if (property.PlayerOwnerId == null)
            {
                int price = property.type switch
                {
                    PropertyType.Property => property.landPrice,
                    PropertyType.RailRoad => property.RailRoadBuyPrice,
                    PropertyType.Utility => property.UtilityBuyPrice,
                    _ => 0
                };

                if (player.Money < price)
                {
                    return false;
                }

                DeductMoney(player, price);
                property.PlayerOwnerId = player.PlayerId;

                if (property.type == PropertyType.RailRoad)
                    player.RailRoadCount++;

                if (property.type == PropertyType.Utility)
                    player.UtilityCount++;

                return true;
            }

            if (property.PlayerOwnerId == player.PlayerId &&
                property.type == PropertyType.Property)
            {
                // nâng nhà
                if (!property.hasHotel)
                {
                    int upgradeCost =
                        property.houseCount < 4
                            ? property.housePrice
                            : property.hotelPrice;

                    if (player.Money < upgradeCost)
                    {
                        return false;
                    }

                    DeductMoney(player, upgradeCost);

                    if (property.houseCount < 4)
                    {
                        property.houseCount++;
                    }
                    else
                    {
                        property.houseCount = 0;
                        property.hasHotel = true;
                    }

                    return true;
                }
            }

            return false;
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

            // ĐẤT CỦA NGƯỜI KHÁC → TRẢ TIỀN
            if (property.PlayerOwnerId != null &&
                property.PlayerOwnerId != player.PlayerId)
            {
                PayRent(match, player, property);
                return;
            }

            // ĐẤT CHƯA CÓ CHỦ → HỎI MUA
            if (property.PlayerOwnerId == null)
            {
                match.WaitingForBuyDecision = true;
                match.PendingTileIndex = tileIndex;
                return;
            }

            // ĐẤT CỦA CHÍNH MÌNH → HỎI NÂNG CẤP
            if (property.PlayerOwnerId == player.PlayerId &&
                property.type == PropertyType.Property)
            {
                // còn nâng cấp được
                if (!property.hasHotel)
                {
                    match.WaitingForBuyDecision = true;
                    match.PendingTileIndex = tileIndex;
                }
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
        private async void HandleChance(MatchState match, PlayerState player)
        {
            Console.WriteLine($"[HandleChance] Player {player.PlayerId} entered Chance tile");

            // ✅ XÁC ĐỊNH CARD INDEX
            if (match.NextChanceCardIndex == -1)
            {
                match.NextChanceCardIndex = Random.Shared.Next(1, 17);
                Console.WriteLine($"[HandleChance] First draw, random cardIndex = {match.NextChanceCardIndex}");
            }
            else
            {
                Console.WriteLine($"[HandleChance] Using existing cardIndex = {match.NextChanceCardIndex}");
            }
            
            int cardIndex = match.NextChanceCardIndex;

            // ✅ RÚT THẺ TỪ DECK (index từ 1-16 → array index 0-15)
            int deckIndex = (cardIndex - 1) % match.ChanceDeck.Count;
            var card = match.ChanceDeck[deckIndex];
            
            Console.WriteLine($"[HandleChance] Card = {card.Description}, deckIndex = {deckIndex}");
            
            // ✅ BROADCAST DRAW CARD EVENT (với delay)
            if (OnDrawCard != null)
            {
                Console.WriteLine($"[HandleChance] Calling OnDrawCard callback...");
                await OnDrawCard(match, player.PlayerId, "Chance", cardIndex, card.Description);
                Console.WriteLine($"[HandleChance] OnDrawCard callback completed");
            }
            else
            {
                Console.WriteLine($"[HandleChance] ERROR: OnDrawCard is NULL!");
            }

            // ✅ TĂNG INDEX CHO LẦN SAU (1-16 quay vòng)
            match.NextChanceCardIndex = (cardIndex % 16) + 1;
            Console.WriteLine($"[HandleChance] Next cardIndex = {match.NextChanceCardIndex}");

            // ✅ XỬ LÝ HIỆU ỨNG THẺ (gọi CardService hoặc xử lý trực tiếp)
            _cardService.applyEffectPlayer(match, player, card);
        }


        //Xử lý lá bài Khí vận
        private async void HandleCommunityChest(MatchState match, PlayerState player)
        {
            Console.WriteLine($"[HandleCommunityChest] Player {player.PlayerId} entered CommunityChest tile");

            // ✅ XÁC ĐỊNH CARD INDEX
            if (match.NextCommunityChestCardIndex == -1)
            {
                match.NextCommunityChestCardIndex = Random.Shared.Next(1, 17);
                Console.WriteLine($"[HandleCommunityChest] First draw, random cardIndex = {match.NextCommunityChestCardIndex}");
            }
            else
            {
                Console.WriteLine($"[HandleCommunityChest] Using existing cardIndex = {match.NextCommunityChestCardIndex}");
            }
            
            int cardIndex = match.NextCommunityChestCardIndex;

            // ✅ RÚT THẺ TỪ DECK (index từ 1-16 → array index 0-15)
            int deckIndex = (cardIndex - 1) % match.CommunityChestDeck.Count;
            var card = match.CommunityChestDeck[deckIndex];
            
            Console.WriteLine($"[HandleCommunityChest] Card = {card.Description}, deckIndex = {deckIndex}");
            
            // ✅ BROADCAST DRAW CARD EVENT (với delay)
            if (OnDrawCard != null)
            {
                Console.WriteLine($"[HandleCommunityChest] Calling OnDrawCard callback...");
                await OnDrawCard(match, player.PlayerId, "CommunityChest", cardIndex, card.Description);
                Console.WriteLine($"[HandleCommunityChest] OnDrawCard callback completed");
            }
            else
            {
                Console.WriteLine($"[HandleCommunityChest] ERROR: OnDrawCard is NULL!");
            }

            // ✅ TĂNG INDEX CHO LẦN SAU (1-16 quay vòng)
            match.NextCommunityChestCardIndex = (cardIndex % 16) + 1;
            Console.WriteLine($"[HandleCommunityChest] Next cardIndex = {match.NextCommunityChestCardIndex}");

            // ✅ XỬ LÝ HIỆU ỨNG THẺ
            _cardService.applyEffectPlayer(match, player, card);
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
                match.CurrentPlayerIndex =
                    (match.CurrentPlayerIndex + 1) % totalPlayers;
            }
            while (match.Players[match.CurrentPlayerIndex].IsBankrupt);
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