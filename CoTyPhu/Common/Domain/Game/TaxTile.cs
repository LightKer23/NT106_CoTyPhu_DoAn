using Common.Domain.Game.Enums;

public class TaxTile : Tile
{
    private int taxAmount;

    public TaxTile(string name, TaxType taxType)
        : base(name, TileType.Tax)
    {

    }

    public override void OnPlayerLand(Player player)
    {
        // Logic xử lý khi player đứng lên ô đất
    }
}
