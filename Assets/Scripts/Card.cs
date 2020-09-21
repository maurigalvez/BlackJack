using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to display a card in Game
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
