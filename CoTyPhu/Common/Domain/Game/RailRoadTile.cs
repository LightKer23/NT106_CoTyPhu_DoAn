using Common.Domain.Game.Enums;
public class RailRoadTile : Tile
{
    private Player? owner { get; set; } = null;
    private int buyPrice { get; set; }
    private int sellPrice {  get; set; }

    public RailRoadTile(
        string name,
        int buyPrice,
        int sellPrice)
        : base(name, TileType.Railroad)
    {
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
    }
}
