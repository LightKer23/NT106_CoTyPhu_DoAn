using Common.Domain.Game.Enums;

public class StartTile : Tile
{
    private int BonusAmount = 200;

    public StartTile()
        :base("Start", TileType.Start)
    { }

    public void onPlayerLand(Player player)
    {

    }
}