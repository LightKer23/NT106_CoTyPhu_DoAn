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

    public override void OnPlayerLand(Player player)
    {
        // Logic xử lý khi player đứng lên ô đất
    }
}
