using Common.Domain.Game.Enums;
public class StartTile : Tile
{
    public int BonusAmount = 200;

    public StartTile()
        :base("Bat Dau", TileType.Start)
    { }
}