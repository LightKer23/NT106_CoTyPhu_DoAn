namespace Common.Domain.Game
{
    public class PropertyTile : Tile
    {
        public int Price { get; set; }                
        public int BaseRent { get; set; }             
        public int HouseRent { get; set; }            
        public int HotelRent { get; set; }           
        public int HouseCount { get; set; }
        public bool HasHotel { get; set; }

        public int? OwnerPlayerId { get; set; }       

        public string ColorGroup { get; set; }

        public override void OnPlayerLanded(Player player, GameSession session)
        {
            throw new NotImplementedException();
        }
    }
}
