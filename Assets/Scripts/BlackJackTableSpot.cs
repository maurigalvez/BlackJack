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
        m_SpotOccupant.GetHand().onHandUpdated += RefreshHandValue;
        m_HitButton.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.Hit));
        m_StandButton.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.Stand));
        m_DoubleDownButton.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.DoubleDown));
        m_Surrender.onClick.AddListener(() => m_SpotOccupant.DoAction(BlackJackPlayer.PlayerAction.Surrender));
    }

    /// <summary>
    /// Move Card To Hand
    /// </summary>
    public void MoveCardToSpot(Card card)
    {
        int handCount = m_SpotOccupant.GetHand().GetCardCount();
        m_SpotOccupant.GetHand().AddCardToHand(card);
        StartCoroutine(MoveCardRoutine(card.transform, m_CardOffset* handCount));
    }

    private IEnumerator MoveCardRoutine(Transform cardTransform, Vector3 offset)
    {
        Vector3 cardStartPos = cardTransform.position;
        Quaternion cardStartRot = cardTransform.rotation;
        Vector3 endPosition = m_CardPivotPoint.position + offset;
        for(float t = 0; t < 1; t++)
        {
            t += Time.deltaTime;
            cardTransform.position = Vector3.Lerp(cardStartPos, endPosition,t);
            cardTransform.rotation = Quaternion.Lerp(cardStartRot, m_CardPivotPoint.rotation, t);
            yield return null;
        }
    }
    
    /// <summary>
    /// Refresh hand value on UI
    /// </summary>
    private void RefreshHandValue()
    {
        m_PlayerHandValue.text = m_SpotOccupant.GetHand().GetHandValue().ToString();
    }

    public void RemoveOccupant()
    {
        m_SpotOccupant.GetHand().onHandUpdated -= RefreshHandValue;
        m_SpotOccupant = null;
        m_HitButton.onClick.RemoveAllListeners();
        m_StandButton.onClick.RemoveAllListeners();
        m_DoubleDownButton.onClick.RemoveAllListeners();
        m_Surrender.onClick.RemoveAllListeners();
    }
}
