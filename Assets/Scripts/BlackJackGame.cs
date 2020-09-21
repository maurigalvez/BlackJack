using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// In charge of handling BlackJack game flow
/// </summary>
public class BlackJackGame : MonoBehaviour
{
    /// <summary>
    /// Defines game state
    /// </summary>
    public enum GameState
    {
        NONE = 0,
        SET_BET,
        DEAL_CARDS,
        PLAYER_ACTION,
        DEALER_ACTION,
        HAND_END
    }
    [SerializeField] private BlackJackTableSpot m_DealerSpot;
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
    private BlackJackDealer m_DealerPlayer = new BlackJackDealer();
    private int m_CurrentPlayerTurn = 0;

    private void Start()
    {
        m_DealerSpot.SetSpotOccupant(m_DealerPlayer);
        AddNewPlayer(0);
    }

    public void SetPlayerCashAmount(int cashAmount)
    {
        m_Players[0].SetPlayerCash(cashAmount);
        SetGameState(GameState.SET_BET);
    }

    public void StartNewGame()
    {        
        SetGameState(GameState.DEAL_CARDS);
    }  

    public void AddNewPlayer(int spotIndex)
    {
        BlackJackPlayer newPlayer = new BlackJackPlayer();
        newPlayer.SetPlayerId(m_Players.Count + 1);
        m_Players.Add(newPlayer);
        m_TableSpots[spotIndex].SetSpotOccupant(newPlayer);
    }

    public void RemovePlayer(int spotIndex)
    {

    }

    private void SetGameState(GameState state)
    {
        switch(state)
        {
            case GameState.SET_BET:
                break;
            case GameState.DEAL_CARDS:
                break;
            case GameState.PLAYER_ACTION:
                break;
            case GameState.DEALER_ACTION:
                break;
            case GameState.HAND_END:
                break;
        }
    }

    public void SetBetForCurrentPlayer(int betAmount)
    {
        m_Players[m_CurrentPlayerTurn].SetBetAmount(betAmount);
    }

    private IEnumerator SetBet()
    {
        for(m_CurrentPlayerTurn = 0; m_CurrentPlayerTurn < m_Players.Count; m_CurrentPlayerTurn++)
        {
            while(m_Players[m_CurrentPlayerTurn] == null)
            {
                yield return null;
            }
        }
        SetGameState(GameState.DEAL_CARDS);
    }


    private IEnumerator DealCards()
    {
        // iterate through players to deal cards
        for(int pIndex = 0; pIndex < m_Players.Count; pIndex++)
        {
            yield return null;
        }
    }
}
