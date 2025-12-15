using Common.Domain.Game.Enums;

public class CommunityChestTile : Tile
{
    public CommunityChestTile(string name)
        : base(name, TileType.CommunityChest)
    {
    }

    public void OnPlayerLand(Player player, CommunityChestDeck chestDeck)
    {
        Card card = chestDeck.DrawCard();

        if (!card.IsGetOutOfJailCard)
            chestDeck.ReturnCard(card);
    }
}
