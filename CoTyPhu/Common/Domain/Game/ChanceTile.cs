using Common.Domain.Game.Enums;

public class ChanceTile : Tile
{
    public ChanceTile(string name)
        : base(name, TileType.Chance)
    {
    }

    public void OnPlayerLand(Player player, ChanceDeck chanceDeck)
    {
        Card card = chanceDeck.DrawCard();

        if (!card.IsGetOutOfJailCard)
            chanceDeck.ReturnCard(card);
    }
}
