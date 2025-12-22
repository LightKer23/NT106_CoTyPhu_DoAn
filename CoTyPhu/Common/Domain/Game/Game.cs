using Common.Domain.Game.Enums;
using System.Collections.Generic;
public class Game
{
    public List<int> Players { get; set; }
    public List<Tile> Board { get; set; }

    public ChanceDeck ChanceDeck { get; set; }
    public CommunityChestDeck ChestDeck { get; set; }

    public int CurrentPlayerIndex { get; set; } = 0;

    private Dice dice { get; set; }

}
