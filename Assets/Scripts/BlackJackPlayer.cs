using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class used to define a BJ Player
/// </summary>
[System.Serializable]
public class BlackJackPlayer 
{
    public enum PlayerAction
    {
        Hit =0,
        Stand,
        DoubleDown,
        Surrender
    }

    public System.Action onHandUpdated = delegate { };
    protected int m_PlayerCash = 0;
    protected int m_PlayerId = -1;
    protected List<Card> m_Hand = null;
    protected int m_CardsValue = 0;

    public void SetPlayerId(int playerID)
    {
        m_PlayerId = playerID;
    }

    public void ResetPlayerCard()
    {
        m_Hand.Clear();
        m_CardsValue = 0;
    }

    public void AddCardToHand(Card card)
    {
        m_Hand.Add(card);
        m_CardsValue += card.GetCardValue();
    }

    public int GetHandValue()
    {
        return m_CardsValue;
    }

    public void DoAction(PlayerAction pAction)
    {

    }
}
