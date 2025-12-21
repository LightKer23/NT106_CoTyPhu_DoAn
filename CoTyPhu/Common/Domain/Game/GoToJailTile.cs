using Common.Domain.Game.Enums;
public class GoToJailTile : Tile
{
    private int jailPosition { get; set; }
    public GoToJailTile()
        :base("Vao Tu", TileType.GoToJail)
    {

    }
}
