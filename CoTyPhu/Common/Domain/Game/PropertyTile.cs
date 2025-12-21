using Common.Domain.Game.Enums;
using System.Collections.Generic;
public class PropertyTile : Tile
{
    public int? PlayerOwnerId = null;

    public int landPrice;
    public int housePrice;
    public int hotelPrice;

    public int houseCount;
    public bool hasHotel;

    public List<int> rentPrice;
    public int sellPrice;

    public PropertyTile(
        string name,
        int landPrice,
        int housePrice,
        int hotelPrice,
        List<int> rentPrice,
        int sellPrice)
        : base(name, TileType.Property)
    {
        this.landPrice = landPrice;
        this.housePrice = housePrice;
        this.hotelPrice = hotelPrice;

        this.houseCount = 0;
        this.hasHotel = false;

        this.rentPrice = rentPrice;
        this.sellPrice = sellPrice;
    }
}
