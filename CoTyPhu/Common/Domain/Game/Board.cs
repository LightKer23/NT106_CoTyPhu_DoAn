namespace Common.Domain.Game
{
    public class Board
    {
        public IList<Tile> Tiles { get; set; } = new List<Tile>();

        public int Size => Tiles.Count;

        public Tile GetTile(int position)
        {
            if (position < 0 || position >= Tiles.Count)
                throw new ArgumentOutOfRangeException(nameof(position));
            return Tiles[position];
        }

        public IEnumerable<PropertyTile> GetProperties()
            => Tiles.OfType<PropertyTile>();
    }
}
