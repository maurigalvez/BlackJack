/// <summary>
/// Class used to save game state
/// </summary>
[System.Serializable]
public class PlayerSave
{
    public int BetAmount;
    public int CashAmount;
    public CardDeck.CardDefinition[] PlayerHand;
    public CardDeck.CardDefinition[] DealerHand;
    public BlackJackGame.GameState GameState;
    public int DeckNumber;
}
