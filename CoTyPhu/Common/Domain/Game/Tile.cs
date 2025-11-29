using Common.Domain.Game.Enums;

namespace Common.Domain.Game
{
    public abstract class Tile
    {
        public int Id { get; set; }           
        public string Name { get; set; }      
        public TileType Type { get; set; }

        public abstract void OnPlayerLanded(Player player, GameSession session);
    }
}
