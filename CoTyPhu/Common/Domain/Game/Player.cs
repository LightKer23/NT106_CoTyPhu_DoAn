using Common.Domain.Game.Enums;

public class Player
{
    public string Name { get; set; }
    public int Money { get; set; }
    public int CurrentPosition { get; set; }

    public PlayerStatus Status { get; set; } = PlayerStatus.Playing;

    public List<PropertyTile> OwnedProperties { get; set; } = new();
    public List<RailroadTile> OwnedRailroads { get; set; } = new();
    public List<UtilityTile> OwnedUtilities { get; set; } = new();

    public bool HasGetOutOfJailCard { get; set; }
    public bool IsWinner { get; set; }

    private Dice dice = new Dice();

    public int RollDice()
    {
        int total = dice.Roll();

        return total;
    }

    public void Move(int steps)
    {

    }

    public void ReceiveMoney(int amount) => Money += amount;
    public void PayMoney(int amount) => Money -= amount;
}
