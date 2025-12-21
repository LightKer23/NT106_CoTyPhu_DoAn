using Common.Domain.Game.Enums;
using System;
using System.Collections.Generic;
public class Deck
{
    private readonly Random random = new Random();
    private List<Card> cards = new List<Card>();

    public Deck(List<Card> initialCards)
    {
        cards = initialCards;
    }
}
