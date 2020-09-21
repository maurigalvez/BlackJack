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
/// Class used to define a card
/// </summary>
public class Card : MonoBehaviour
{  
    private Card_Suite m_CardSuit;
    private Card_Number m_CardNumber;

    public void InitializeCard(Card_Suite suite, Card_Number number)
    {
        // select random suit
        m_CardSuit = suite;
        m_CardNumber = number;
    }

    public Card_Suite GetCardSuit()
    {
        return m_CardSuit;
    }

    public int GetCardValue()
    {
        switch(m_CardNumber)
        {
            case Card_Number.A:
                return 11;

            case Card_Number.Jack:
            case Card_Number.Queen:
            case Card_Number.King:
                return 10;

             default:
                return (int)m_CardNumber;
        }
    }

}
