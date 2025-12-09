using Common.Domain.Game.Enums;

public class ChanceDeck : Deck
{
    public ChanceDeck(List<Card> initialCards)
    {
        foreach (var card in initialCards)
        {
            if (card.Type == CardType.Chance)
                cards.Add(card);
        }

        Shuffle();
    }
}