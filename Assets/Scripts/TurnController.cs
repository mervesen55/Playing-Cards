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

    [SerializeField] private int score;//score
    public int totalGainedCardNumber;

    [SerializeField] private bool isPlayer;

    [SerializeField] private TMP_Text BetText;
    [SerializeField] private TMP_Text BetText2;
    [SerializeField] private TMP_Text scoreText;
    

    [SerializeField] private int ID;

    private Bot bot;

    private int minBet=250;

    [SerializeField]
    private int myBet = 1000;

    public int MyBet
    {
        get { return myBet; }
        set
        {      
            myBet = value;
            if (myBet < minBet) myBet = minBet;
            if(BetText)BetText.text = myBet.ToString();
            SetProfileInfos();
            //SetPrefs();
            PlayerPrefs.SetInt("Bet" + ID, myBet);
        }
    }
    public int Score
    {
        get { return score; }
        set 
        {
            score  = value;
            SetProfileInfos();
            PlayerPrefs.SetInt("Score" + ID, score);
            //SetPrefs(); 
        }
    }
    private void Awake()
    {
        
        if (TryGetComponent<Bot>(out Bot _bot)) { bot = _bot; }
        if (PlayerPrefs.HasKey("PlayersWinCount" + ID))
        {
            myBet = PlayerPrefs.GetInt("Bet" + ID);
            
            WinCount = PlayerPrefs.GetInt("PlayersWinCount" + ID);
            LostCount = PlayerPrefs.GetInt("PlayersLostCount" + ID);
            score = PlayerPrefs.GetInt("Score" + ID);
        }
        else SetPrefs();
        if (BetText) BetText.text = myBet.ToString();
        if (isPlayer) UISettings.instance.SetProfile();
        
        
    }
    private void Start()
    {
        if(isPlayer)GameManager.Instance.chosenBet = myBet;
        bool continueRound = PlayerPrefs.GetInt("keepPlaying") == 1;
        if (!isPlayer && !continueRound)
        {
            DeletePrefs();
        }
        else if(isPlayer && !GameManager.Instance.ContiuneRound) PlayerPrefs.SetInt("Score" + ID, 0);
        SetProfileInfos();
    }
    public void ToggleTurn(bool _isMyTurn)
    {
        if(isPlayer) { Player.Instance.isMyTurn = _isMyTurn; }
        else bot.isMyTurn = _isMyTurn;
        myPointer.SetActive(_isMyTurn);
        if(!isPlayer && _isMyTurn) bot.Decide();
    }

    public void SetProfileInfos()
    {
        scoreText.text = Score.ToString();
        BetText2.text = myBet.ToString();
    }

    public void SetWinLost(bool win)
    {
        if (win) WinCount++;
        else LostCount++;
        SetPrefs();
        if(isPlayer)UISettings.instance.SetProfile();
    }

    public void DeletePrefs()
    {
        PlayerPrefs.SetInt("PlayersWinCount" + ID, 0);
        PlayerPrefs.SetInt("PlayersLostCount" + ID, 0);
        PlayerPrefs.SetInt("Bet" + ID, 1000);
        PlayerPrefs.SetInt("Score" + ID, 0);
    }
    private void SetPrefs()
    {
        PlayerPrefs.SetInt("PlayersWinCount" + ID, WinCount);
        PlayerPrefs.SetInt("PlayersLostCount" + ID, LostCount);
        PlayerPrefs.SetInt("Bet" + ID, myBet);
        PlayerPrefs.SetInt("Score" + ID, score);
    }

}
