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
    public System.Action<int,PlayerAction> onActionPlayed = delegate { };
    public System.Action<int> onCashUpdated = delegate { };
    public System.Action<int> onBetUpdated = delegate { };
 
    protected int m_PlayerCash = 0;
    protected int m_BetAmount = 0;
    protected int m_PlayerId = -1;
    protected BlackJackHand m_PlayerHand = new BlackJackHand();
    protected bool m_HasStanded = false;

    public bool Standed()
    {
        return m_HasStanded;
    }

    public int GetPlayerId()
    {
        return m_PlayerId;
    }

    public void SetPlayerId(int playerID)
    {
        m_PlayerId = playerID;
    }

    public void SetPlayerCash(int cashAmount)
    {
        m_PlayerCash = cashAmount;
        onCashUpdated(m_PlayerCash);
    }

    public void AddPlayerCash(int amount)
    {
        m_PlayerCash += amount;
        onCashUpdated(m_PlayerCash);
    }

    public bool IsBetSet()
    {
        return m_BetAmount > 0;
    }

    public void ResetBet()
    {
        m_BetAmount = 0;
    }

    public int GetBetAmount()
    {
        return m_BetAmount;
    }

    public void SetBetAmount(int betAmount)
    {
        if (m_BetAmount > m_PlayerCash)
            return;
        m_BetAmount = betAmount;
        onBetUpdated(m_BetAmount);
        AddPlayerCash(-m_BetAmount);
    }

    public BlackJackHand GetHand()
    {
        return m_PlayerHand;
    }   

    public void Reset()
    {
        GetHand().ResetHand();
        ResetBet();
        m_HasStanded = false;
    }

    public void DoAction(PlayerAction pAction)
    {
        switch(pAction)
        {
            case PlayerAction.Stand:
                m_HasStanded = true;
                break;
        }
        onActionPlayed(m_PlayerId, pAction);
    }
}
