using Common.Domain.Game.Enums;

public class RailroadTile : Tile
{
    private Player owner { get; set; };
    private int price { get; set; };

    private Dice dice = new Dice();

    public RailroadTile(
        string name,
        int price)
        : base(name, TileType.Utility)
    {
        this.price = price;
        this.owner = null;
    }

    public int calculateRent(int numOwned)
    {
        //Tính số tiền phải trả tương ứng với số công ty người chơi hiện có và số điểm sau khi tung xúc xắc
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
