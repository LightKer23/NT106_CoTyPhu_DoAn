using Common.Domain.Game.Enums;

public abstract class GoToJailTile : Tile
{
    private int jailPosition { get; set; };

    public abstract void OnPlayerLand(Player player)
    {

    }
}
