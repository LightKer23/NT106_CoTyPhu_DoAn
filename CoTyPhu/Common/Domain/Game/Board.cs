public class Board
{
    //Attribute
    public List<Tile> Tiles { get; set; } = new();
    public int StartIndex { get; set; }
    public int JailPosition { get; set; }
    public int FreeParkingIndex { get; set; }
    public int GoToJailPosition { get; set; }

    //Method
    public Tile GetTile(int position)
    {
        return Tiles[position];
    }
}
