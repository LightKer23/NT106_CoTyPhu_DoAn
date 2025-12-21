using Common.Domain.Game.Enums;
public class Player
{
    public string Name { get; set; }
    public int Money { get; set; }
    public int CurrentPosition { get; set; }

    public PlayerStatus Status { get; set; } = PlayerStatus.Playing;

    public bool HasGetOutOfJailCard { get; set; }
    public bool IsWinner { get; set; }

    private Dice dice = new Dice();

    public int RollDice()
    {
        int total = dice.Roll();

        return total;
    }
}
