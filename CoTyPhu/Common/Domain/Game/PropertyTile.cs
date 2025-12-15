using Common.Domain.Game.Enums;

public class PropertyTile : Tile
{
    private Player owner;

    private int landPrice;
    private int housePrice;
    private int hotelPrice;

    private int houseCount;
    private bool hasHotel;

    private int[] rentPrice;

    public PropertyTile(
        string name,
        int landPrice,
        int housePrice,
        int hotelPrice,
        int[] rentPrice)
        : base(name, TileType.Property)
    {
        this.owner = null;
        this.landPrice = landPrice;
        this.housePrice = housePrice;
        this.hotelPrice = hotelPrice;

        this.houseCount = 0;
        this.hasHotel = false;

        this.rentPrice = rentPrice;
    }

    public override void OnPlayerLand(Player player)
    {
        // Logic xử lý khi player đứng lên ô đất
    }
}
