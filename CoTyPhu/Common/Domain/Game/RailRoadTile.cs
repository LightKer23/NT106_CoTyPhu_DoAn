using Common.Domain.Game.Enums;
public class RailRoadTile : Tile
{
    public int? PlayerOwnerId { get; set; } = null;
    public int buyPrice { get; set; }
    public int sellPrice {  get; set; }

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
