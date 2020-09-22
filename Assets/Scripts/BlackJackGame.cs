﻿using System.Collections;
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
        SET_CASH,
        SET_BET,
        DEAL_CARDS,
        PLAYER_ACTION,
        DEALER_ACTION,
        HAND_END
    }
    [Header("Prefabs")]
    [SerializeField] private Card m_CardPrefab;
    [SerializeField] private Transform m_CardSpawnPoint = null;
    [SerializeField] private Sprite m_BackSideSprite;
    [SerializeField] private Sprite[] m_CardSprites;
    [Header("Table Setup")]
    [SerializeField] private BlackJackTableSpot m_DealerSpot;
    [SerializeField] private BlackJackTableSpot[] m_TableSpots;
    [Header("Add Player")]
    [SerializeField] private GameObject m_NewPlayerDisplay = null;
    [SerializeField] private Text m_PlayerAddMessage;
    [SerializeField] private InputField m_PlayerCashStart;

    private CardDeck m_CurrentDeck = new CardDeck();
    private int m_NumberOfDecks = 8;
    private List<BlackJackPlayer> m_Players = new List<BlackJackPlayer>();
    private BlackJackDealer m_DealerPlayer;
    private int m_CurrentPlayerTurn = 0;
    private int m_DeckNumber = 1;
    private Coroutine m_CurrentRoutine = null;
    private List<Card> m_InstantiatedCards = new List<Card>();
    private GameState m_CurrentGameState = GameState.NONE;

    private void Start()
    {
        m_DealerPlayer = new BlackJackDealer();
        m_DealerSpot.SetSpotOccupant(m_DealerPlayer);
        AddNewPlayer(0);
        SetGameState(GameState.SET_CASH);
        // Get Player Cash Amount
        m_NewPlayerDisplay.SetActive(true);
        m_PlayerAddMessage.text = "Set Initial Cash Amount";    
    }

    public void ReadInputAmount()
    {
        switch(m_CurrentGameState)
        {
            case GameState.SET_CASH:
                SetPlayerCashAmount(0, int.Parse(m_PlayerCashStart.text));
                break;
            case GameState.SET_BET:
                SetBetForCurrentPlayer(int.Parse(m_PlayerCashStart.text));
                break;
        }
    }

    private void SetPlayerCashAmount(int playerIndex, int cashAmount)
    {
        m_Players[playerIndex].SetPlayerCash(cashAmount);
        SetGameState(GameState.SET_BET);
    }

    private void SetBetForCurrentPlayer(int betAmount)
    {
        m_Players[m_CurrentPlayerTurn].SetBetAmount(betAmount);
    }


    public void StartNewGame()
    {
        m_DeckNumber = 1;
        m_CurrentDeck.Initialize();
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
        m_CurrentGameState = state;
        switch(state)
        {
            case GameState.SET_BET:
                m_NewPlayerDisplay.SetActive(true);             
                m_CurrentRoutine = StartCoroutine(SetBet());
                break;
            case GameState.DEAL_CARDS:
                m_NewPlayerDisplay.SetActive(false);
                m_CurrentRoutine = StartCoroutine(DealCards());
                break;
            case GameState.PLAYER_ACTION:
                break;
            case GameState.DEALER_ACTION:
                break;
            case GameState.HAND_END:
                break;
        }
    }

    private IEnumerator SetBet()
    {
        Debug.Log("Betting Turn");
        for(int sIndex = 0; sIndex < m_TableSpots.Length; sIndex++)
        {
            if(!m_TableSpots[sIndex].IsOccupied())
            {
                continue;
            }
            m_PlayerAddMessage.text = "Enter Bet for Player" + m_TableSpots[sIndex].Occupant.GetPlayerId() + "Cash Amount";
            while (!m_TableSpots[sIndex].Occupant.IsBetSet())
            {
                yield return null;
            }
        }
        m_CurrentRoutine = null;
        StartNewGame();
    }

    private void DealCardToSpot(BlackJackTableSpot spot)
    {
        Debug.Log("Dealing Cards");
        // get card definition
        CardDeck.CardDefinition cardDefinition = m_CurrentDeck.GetCardDefinition();
        if(cardDefinition == null && m_DeckNumber < m_NumberOfDecks)
        {
            m_DeckNumber++;
            m_CurrentDeck.Initialize();
            cardDefinition = m_CurrentDeck.GetCardDefinition();
        }
        // Spawn card Display and give to player
        Card newCard = GameObject.Instantiate(m_CardPrefab, m_CardSpawnPoint.position, m_CardSpawnPoint.rotation);
        newCard.InitializeCard(m_CardSprites[cardDefinition.Index],cardDefinition.Suite, cardDefinition.Number);
        spot.MoveCardToSpot(newCard);
    }

    private IEnumerator DealCards()
    {
        int cardsDealt = 0;
        // iterate through players to deal cards
        while (cardsDealt < 2)
        {
            // deal card to player
            for (int sIndex = 0; sIndex < m_TableSpots.Length; sIndex++)
            {
                if (!m_TableSpots[sIndex].IsOccupied())
                {
                    continue;
                }
                DealCardToSpot(m_TableSpots[sIndex]);
                yield return new WaitForSeconds(2);
            }
            cardsDealt++;
            DealCardToSpot(m_DealerSpot);
            yield return new WaitForSeconds(2);
        }
    }
}
