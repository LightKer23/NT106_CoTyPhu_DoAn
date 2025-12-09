using Common.Domain.Game.Enums;

public class CommunityChestDeck : Deck
{
    public CommunityChestDeck(List<Card> initialCards)
    {
        foreach (var card in initialCards)
        {
            if (card.Type == CardType.CommunityChest)
                cards.Add(card);
        }

        Shuffle();
    }
}
