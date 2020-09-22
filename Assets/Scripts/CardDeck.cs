using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to set Card Suite
/// </summary>
public enum Card_Suite
{
    Clubs = 0,
    Diamonds,
    Hearts,
    Spades
}

/// <summary>
/// Used to define Cards' value
/// </summary>
public enum Card_Number
{
    A = 1,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King
}

/// <summary>
/// Class used to define a deck of cards
/// </summary>
public class CardDeck 
{

    /// <summary>
    /// Class used to define a card
    /// </summary>
    [System.Serializable]
    public class CardDefinition
    {
        public Card_Number Number;
        public Card_Suite Suite;
        public int Index;

        public CardDefinition(int index, int suite, int number)
        {
            Index = index;
            Suite = (Card_Suite)suite;
            Number = (Card_Number)number;           
        }
    }

    private List<CardDefinition> m_Cards;
    private const int SUITE_NUMBER = 4;
    private const int CARDS_PER_SUITE = 13;

    public void Initialize()
    {
        m_Cards = new List<CardDefinition>();
        for(int suite = 0; suite < SUITE_NUMBER; suite++)
        {
            for(int card= 0; card < CARDS_PER_SUITE; card++)
            {
                m_Cards.Add(new CardDefinition((suite * 13) + card, suite, card + 1));
            }
        }
    }

    public CardDefinition GetCardDefinition()
    {
        CardDefinition card = null;
        if(m_Cards.Count > 0)
        {
            int cardIndex = Random.Range(0, m_Cards.Count);
            card = m_Cards[cardIndex];
            m_Cards.RemoveAt(cardIndex);
        }
        return card;
    }
}
