using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Used to handle player UI at the table
/// </summary>
public class BlackJackTableSpot : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private Text m_PlayerHandValue;    // used to display hand value
    [SerializeField] private Text m_PlayerStatus;       // used to displaye status in game (BUSTED, BJ, WIN, PUSH).
    [Header("Action Buttons")]
    [SerializeField] private Button m_HitButton;
    [SerializeField] private Button m_StandButton;
    [SerializeField] private Button m_DoubleDownButton;
    [SerializeField] private Button m_Surrender;

    private BlackJackPlayer m_SpotOccupant;
    private Color m_BlackjackStatus = Color.yellow;
    private Color m_WinStatus = Color.green;
    private Color m_BustedStatus = Color.red;
    private Color m_PushStatus = Color.white;

    /// <summary>
    /// Set occupant of this spot
    /// </summary>
    public void SetSpotOccupant(BlackJackPlayer occupant)
    {
        m_SpotOccupant = occupant;
        m_SpotOccupant.onHandUpdated += RefreshHandValue;
        m_HitButton.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.Hit));
        m_StandButton.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.Stand));
        m_DoubleDownButton.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.DoubleDown));
        m_Surrender.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.Surrender));
    }
    
    private void RefreshHandValue()
    {
        m_PlayerHandValue.text = m_SpotOccupant.GetHandValue().ToString();
    }

    public void RemoveOccupant()
    {
        m_SpotOccupant.onHandUpdated -= RefreshHandValue;
        m_SpotOccupant = null;
        m_HitButton.onClick.RemoveAllListeners();
        m_StandButton.onClick.RemoveAllListeners();
        m_DoubleDownButton.onClick.RemoveAllListeners();
        m_Surrender.onClick.RemoveAllListeners();
    }
}
