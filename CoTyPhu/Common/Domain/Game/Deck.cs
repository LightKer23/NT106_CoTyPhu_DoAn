using Common.Domain.Game.Enums;

public class Deck
{
    private readonly Random random = new Random();
    private List<Card> cards = new List<Card>();

    public Deck(List<Card> initialCards)
    {
        cards = initialCards;
    }

    public Card Draw()
    {

    }

    public void Shuffle()
    {

    }

    public void ReturnCard(Card card)
    {

    }
}
