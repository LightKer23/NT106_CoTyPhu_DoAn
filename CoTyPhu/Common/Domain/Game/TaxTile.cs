using Common.Domain.Game.Enums;
public class TaxTile : Tile
{
    public int taxAmount;
    public TaxType taxType;

    public TaxTile(string name, TaxType taxType)
        : base(name, TileType.Tax)
    {
        this.taxType = taxType;
    }
}
