using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackGame : MonoBehaviour
{
    [SerializeField] private BlackJackTableSpot[] m_TableSpots;

    private List<BlackJackPlayer> m_Players = new List<BlackJackPlayer>();
}
