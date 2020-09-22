using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Used to handle player UI at the table
/// </summary>
public class BlackJackTableSpot : MonoBehaviour
{
    [Header("Cards")]
    [SerializeField] private Transform m_CardPivotPoint = null;
    [SerializeField] private Vector3 m_CardOffset = new Vector3(-0.25f, 0.25f, 0);
    [Header("Display")]
    [SerializeField] private Text m_PlayerCashAmount;
    [SerializeField] private Text m_PlayerBetAmount;
    [SerializeField] private GameObject m_PlayerHandDisplay;
    [SerializeField] private Text m_PlayerHandValue;    // used to display hand value
    [SerializeField] private Text m_PlayerStatus;       // used to displaye status in game (BUSTED, BJ, WIN, PUSH).
    [Header("Player Action Buttons")]
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
    /// Get current occupant
    /// </summary>
    public BlackJackPlayer Occupant
    {
        get { return m_SpotOccupant; }
    }

    /// <summary>
    /// Get Whether this spot is occupied or not
    /// </summary>
    /// <returns></returns>
    public bool IsOccupied()
    {
        return m_SpotOccupant != null;
    }

    /// <summary>
    /// Set occupant of this spot
    /// </summary>
    public void SetSpotOccupant(BlackJackPlayer occupant)
    {
        m_SpotOccupant = occupant;
        m_PlayerCashAmount.gameObject.SetActive(true);
        m_PlayerBetAmount.gameObject.SetActive(true);
        m_SpotOccupant.onBetUpdated += RefreshBetAmount;
        m_SpotOccupant.onCashUpdated += RefreshCashAmount;
        m_SpotOccupant.GetHand().onHandUpdated += RefreshHandValue;
        if(m_HitButton)
            m_HitButton.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.Hit));
        if(m_StandButton)
            m_StandButton.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.Stand));
        if(m_DoubleDownButton)
            m_DoubleDownButton.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.DoubleDown));
        if(m_Surrender)
            m_Surrender.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.Surrender));
    }

    public void Reset()
    {
        m_PlayerStatus.gameObject.SetActive(false);
        m_SpotOccupant.ResetBet();
        m_SpotOccupant.GetHand().ResetHand();
    }

    public void SetStatus(HandStatus status)
    {
        m_PlayerStatus.gameObject.SetActive(true);
        m_PlayerStatus.text = status.ToString();
    }

    public void TogglePlayerCardAction(bool isEnabled)
    {
        m_HitButton.gameObject.SetActive(isEnabled);
        m_StandButton.gameObject.SetActive(isEnabled);
    }

    /// <summary>
    /// Move Card To Hand
    /// </summary>
    public void MoveCardToSpot(Card card)
    {
        int handCount = m_SpotOccupant.GetHand().GetCardCount();   
        card.transform.SetParent(m_CardPivotPoint);
        StartCoroutine(MoveCardRoutine(card.transform, m_CardOffset* handCount));
        m_SpotOccupant.GetHand().AddCardToHand(card);
    }

    private IEnumerator MoveCardRoutine(Transform cardTransform, Vector3 offset)
    {
        Vector3 cardStartPos = cardTransform.localPosition;
        Quaternion cardStartRot = cardTransform.localRotation;
        Vector3 endPosition = offset;
        for(float t = 0; t < 1; t += Time.deltaTime)
        { 
            cardTransform.localPosition = Vector3.Lerp(cardStartPos, endPosition,t);
            cardTransform.localRotation = Quaternion.Lerp(cardStartRot, Quaternion.identity, t);
            yield return null;
        }
    }

    private void RefreshCashAmount(int cashAmount)
    {
        m_PlayerCashAmount.text = "$" + cashAmount.ToString("N0");
    }

    private void RefreshBetAmount(int betAmount)
    {
        m_PlayerBetAmount.text = "$" + betAmount.ToString("N0");
    }
    
    /// <summary>
    /// Refresh hand value on UI
    /// </summary>
    private void RefreshHandValue()
    {
        if (!m_PlayerHandDisplay.activeSelf)
            m_PlayerHandDisplay.SetActive(true);
        m_PlayerHandValue.text = m_SpotOccupant.GetHand().GetHandValue().ToString();
    }

    public void RemoveOccupant()
    {
        m_PlayerCashAmount.gameObject.SetActive(false);
        m_PlayerHandDisplay.SetActive(false);
        m_PlayerBetAmount.gameObject.SetActive(false);
        m_SpotOccupant.GetHand().onHandUpdated -= RefreshHandValue;
        m_SpotOccupant.onBetUpdated -= RefreshBetAmount;
        m_SpotOccupant.onCashUpdated -= RefreshCashAmount;
        m_SpotOccupant = null;
        m_HitButton.onClick.RemoveAllListeners();
        m_StandButton.onClick.RemoveAllListeners();
        m_DoubleDownButton.onClick.RemoveAllListeners();
        m_Surrender.onClick.RemoveAllListeners();
    }
}
