using Common.Domain.Game.Enums;
public abstract class Tile
{
    //Attribute
    public string name { get; set; }
    public TileType type { get; set; }


    //Method
    public Tile(string name, TileType type)
    {
        this.name = name;
        this.type = type;
    }
}
