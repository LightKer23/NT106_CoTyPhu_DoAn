using Common.Domain.Game.Enums;

public class Dice
{
    private readonly int[] faceValue = { 1, 2, 3, 4, 5, 6 };
    private bool isDouble { get; set; };
    private int dice1 { get; set; };
    private int dice2 { get; set; };
    private readonly Random random = new Random();

    public int Roll()
    {
        dice1 = faceValue[random.Next(0, 6)];
        dice2 = faceValue[random.Next(0, 6)];
        if(dice1 == dice2) isDouble = true;
        else isDouble = false;

        return dice1 + dice2;
    }
}