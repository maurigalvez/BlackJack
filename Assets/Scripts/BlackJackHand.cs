using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackHand 
{
    public System.Action onHandUpdated = delegate { };
    protected List<Card> m_Hand = null;
    protected int m_CardsValue = 0;

    public void AddCardToHand(Card card)
    {
        if(m_Hand == null)
        {
            m_Hand = new List<Card>();
        }
        m_Hand.Add(card);
        m_CardsValue += card.GetCardValue();
    }

    public void ResetHand()
    {
        m_Hand.Clear();
        m_CardsValue = 0;
    }

    public int GetHandValue()
    {
        return m_CardsValue;
    }
}
