using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Class used to display a card in Game
/// </summary>
public class Card : MonoBehaviour
{
    [SerializeField] private Image m_CardSpriteDisplay = null;
    private CardDeck.CardDefinition m_CardDefinition;

    public void InitializeCard(Sprite cardSprite, CardDeck.CardDefinition cardDefinition)
    {
        // select random suit
        m_CardDefinition = cardDefinition;
        m_CardSpriteDisplay.sprite = cardSprite; 
    }

    public CardDeck.CardDefinition GetCardDefinition()
    {
        return m_CardDefinition;
    }

    public Card_Suite GetCardSuit()
    {
        return m_CardDefinition.Suite;
    }

    public int GetCardValue()
    {
        switch(m_CardDefinition.Number)
        {
            case Card_Number.A:
                return 11;

            case Card_Number.Jack:
            case Card_Number.Queen:
            case Card_Number.King:
                return 10;

             default:
                return (int)m_CardDefinition.Number;
        }
    }

}
