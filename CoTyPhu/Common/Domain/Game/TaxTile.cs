using Common.Domain.Game.Enums;
public class TaxTile : Tile
{
    private int taxAmount;
    TaxType taxType;

    public TaxTile(string name, TaxType taxType)
        : base(name, TileType.Tax)
    {
        this.taxType = taxType;
    }
}
