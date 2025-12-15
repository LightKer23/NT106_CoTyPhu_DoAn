using Common.Domain.Game.Enums;

public class Game
{
    public List<Player> Players { get; set; }
    public List<Tile> Board { get; set; }

    public ChanceDeck ChanceDeck { get; set; }
    public CommunityChestDeck ChestDeck { get; set; }

    public int CurrentPlayerIndex { get; set; } = 0;

    private Dice dice { get; set; };

    public Game(
        List<Player> players,
        List<Tile> board,
        List<Card> chanceCards,
        List<Card> chestCards)
    {
        Players = players;
        Board = board;

        ChanceDeck = new ChanceDeck(chanceCards);
        ChestDeck = new CommunityChestDeck(chestCards);
    }

    public Player CurrentPlayer => Players[CurrentPlayerIndex];

    private void NextTurn()
    {

    }

    // Xử lý khi Player roll dice
    public void PlayerRoll(Player player)
    {
        int steps = dice.Roll;

        player.Move(steps);

        Tile tile = Board[player.Position];
        HandleTileAction(player, tile);

        NextTurn();
    }


    private void HandleTileAction(Player player, Tile tile)
    {
        // Xử lý Player dừng trên ô nào đó
    }
}
