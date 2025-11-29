using Common.Domain.Game.Enums;

namespace Common.Domain.Game
{
    public class CommunityChestCard
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public CommunityChestCardType Type { get; set; }
        public int? Amount { get; set; }
        public int? TargetTileId { get; set; }  
    }
}
