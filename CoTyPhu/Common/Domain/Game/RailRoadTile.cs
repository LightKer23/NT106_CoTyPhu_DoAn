using Common.Domain.Game.Enums;

public class RailroadTile : Tile
{
    private Player owner { get; set; };
    private int price { get; set; };

    public RailroadTile(
        string name,
        int price)
        : base(name, TileType.Railroad)
    {
        this.price = price;
        this.owner = null;
    }


    public int calculateRent(int numOwned)
    {
        //Tính số tiền phải trả tương ứng với số bến xe người chơi hiện có
    }

    public void sellAsset()
    {
        //Bán ô
    }
    public override void OnPlayerLand(Player player)
    {
        // Logic xử lý khi player đứng lên ô Bến xe
    }
}
