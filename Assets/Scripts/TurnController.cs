using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TMPro;


public class TurnController : MonoBehaviour
{
    public GameObject myPointer;

    public int LostCount;
    public int WinCount;

    /*[HideInInspector]*/ public int Score;//score
    public int totalGainedCardNumber;

    [SerializeField] private bool isPlayer;

    [SerializeField] private TMP_Text BetText;

    [SerializeField] private int ID;

    private Bot bot;

    private int minBet=250;

    private int myBet = 1000;

    public int MyBet
    {
        get { return myBet; }
        set
        {      
            myBet = value;
            if (myBet < minBet) myBet = minBet;
            if(BetText)BetText.text = myBet.ToString();
            SetPrefs();
        }
    }

    private void Awake()
    {
        
        if (TryGetComponent<Bot>(out Bot _bot)) { bot = _bot; }
        if (PlayerPrefs.HasKey("PlayerWinCount" + ID))
        {
            myBet = PlayerPrefs.GetInt("Bet" + ID);
            WinCount = PlayerPrefs.GetInt("PlayersWinCount" + ID);
            LostCount = PlayerPrefs.GetInt("PlayersLostCount" + ID);
        }
        else SetPrefs();
        if (BetText) BetText.text = myBet.ToString();
    }

    public void ToggleTurn(bool _isMyTurn)
    {
        if(isPlayer) { Player.Instance.isMyTurn = _isMyTurn; }
        else bot.isMyTurn = _isMyTurn;
        myPointer.SetActive(_isMyTurn);
        if(!isPlayer && _isMyTurn) bot.Decide();
    }

    public void SetWinLost(bool win)
    {
        if (win) WinCount++;
        else LostCount++;
        SetPrefs();
    }
    private void SetPrefs()
    {
        PlayerPrefs.SetInt("PlayersWinCount" + ID, WinCount);
        PlayerPrefs.SetInt("PlayersLostCount" + ID, LostCount);
        PlayerPrefs.SetInt("Bet" + ID, MyBet);
    }

}
