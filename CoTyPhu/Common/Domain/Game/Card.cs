using Common.Domain.Game.Enums;
public class Card
{
    public string Description { get; set; }
    public CardType Type { get; set; }

    public ChanceCardType? ChanceType { get; set; }
    public CommunityChestCardType? ChestType { get; set; }

    public int Amount { get; set; }
    public int MoveToTileIndex { get; set; }
    public int MoveBackSteps { get; set; }
    public bool IsGetOutOfJailCard { get; set; }

    // Constructor tạo thẻ Chance
    public Card(string description, ChanceCardType type)
    {
        Type = CardType.Chance;
        Description = description;
        ChanceType = type;

        if (type == ChanceCardType.GetOutOfJailFree)
            IsGetOutOfJailCard = true;
    }

    // Constructor tạo thẻ Community Chest
    public Card(string description, CommunityChestCardType type)
    {
        Type = CardType.CommunityChest;
        Description = description;
        ChestType = type;

        if (type == CommunityChestCardType.GetOutOfJailFree)
            IsGetOutOfJailCard = true;
    }
}
