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
    protected int m_PlayerCash = 0;
    protected int m_PlayerId = -1;
    protected BlackJackHand m_PlayerHand;

    public void SetPlayerId(int playerID)
    {
        m_PlayerId = playerID;
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
