using Common.Domain.Game.Enums;

public class FreeParkingTile : Tile
{
    public FreeParkingTile(string name) 
        :base(name, TileType.FreeParking)
    { }

    public void onPlayerLand(Player player)
    {

    }
}