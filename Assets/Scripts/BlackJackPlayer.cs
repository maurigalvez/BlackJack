﻿using System.Collections;
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
    protected BlackJackHand m_PlayerHand;

    public void SetPlayerId(int playerID)
    {
        m_PlayerId = playerID;
    }

    public void SetPlayerCash(int cashAmount)
    {
        m_PlayerCash = cashAmount;
        onCashUpdated(m_PlayerCash);
    }

    public void SetBetAmount(int betAmount)
    {
        m_BetAmount = betAmount;
        onBetUpdated(m_BetAmount);
    }

    public BlackJackHand GetHand()
    {
        return m_PlayerHand;
    }   

    public void DoAction(PlayerAction pAction)
    {
        onActionPlayed(m_PlayerId,pAction);
    }
}
