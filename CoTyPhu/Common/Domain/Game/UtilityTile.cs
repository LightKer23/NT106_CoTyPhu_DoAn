using Common.Domain.Game.Enums;
public class UtilityTile : Tile
{
    public int buyPrice { get; set; }
    public int? Owner { get; set; } = null;
    public int sellPrice { get; set; }

    private Dice dice = new Dice();
    public UtilityTile(string name, int buyPrice, int sellPrice)
        : base(name, TileType.Utility)
    {
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
    }
}
