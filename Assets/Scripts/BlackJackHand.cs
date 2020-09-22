using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HandStatus
{
    NONE = 0,
    BJ,
    BUST,
    WIN,
    LOSE,
    PUSH
}

[System.Serializable]
public class BlackJackHand 
{
    public System.Action onHandUpdated = delegate { };
    protected List<Card> m_Hand = null;
    protected int m_CardsValue = 0;
    private bool m_IsBlackjack = false;

    public bool IsBlackjack()
    {
        return m_IsBlackjack;
    }

    public void AddCardToHand(Card card)
    {
        if(m_Hand == null)
        {
            m_Hand = new List<Card>();
        }
        m_Hand.Add(card);
        m_CardsValue += card.GetCardValue();
        onHandUpdated();
    }

    public void ResetHand()
    {
        for(int cIndex = 0; cIndex < m_Hand.Count; cIndex++)
        {
            GameObject.Destroy(m_Hand[cIndex].gameObject);
        }
        m_Hand.Clear();
        m_CardsValue = 0;
        onHandUpdated();
    }

    public int GetCardCount()
    {
        if (m_Hand == null)
        {
            return 0;
        }
        return m_Hand.Count;
    }

    public int GetHandValue()
    {
        return m_CardsValue;
    }
}
