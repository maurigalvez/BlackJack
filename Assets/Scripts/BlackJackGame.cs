using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlackJackGame : MonoBehaviour
{
    public enum GameState
    {
        NONE = 0,
        DEAL_CARDS,
        PLAYER_ACTION,
        DEALER_ACTION,
        HAND_END
    }
    [SerializeField] private BlackJackTableSpot[] m_TableSpots;
    [Header("Add Player")]
    [SerializeField] private GameObject m_NewPlayerDisplay = null;
    [SerializeField] private Text m_PlayerAddMessage;
    [SerializeField] private InputField m_PlayerCashStart;
    [Header("Bet Amount")]
    [SerializeField] private RectTransform m_BetDisplayPoints;

    private Stack<CardDeck> m_Decks = new Stack<CardDeck>();
    private int m_NumberOfDecks = 8;
    private List<BlackJackPlayer> m_Players = new List<BlackJackPlayer>();
   
}
