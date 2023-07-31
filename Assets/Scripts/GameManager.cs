using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int NumberofBots;
    public int chosenBet;
    public int layerOrder=2;
    public int Timer;
    public int TotalBet;
    public int totalValue;

    public char LastCard;

    public string CurrentCard;

    public bool RoundSetted;
    public bool initialCardsGained;
    public bool ContiuneRound;

    public List<Transform> Players = new List<Transform>();
    public List<Transform> PlayedCards = new List<Transform>();
    public List<Transform> PlayingOrder = new List<Transform>();

    public GameObject PlayingArea;
    public GameObject PlayerPoint;
    public GameObject PlayerCanvas;
    public GameObject roomsPanel;
    public GameObject optionsPanel;
    public GameObject fakeInitialCards;
    public GameObject continueButton;//101 puanda kapat to do
    public GameObject tableOptionsIcon;
 

    private int turnCounter;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("keepPlaying"))
        {
            ContiuneRound = PlayerPrefs.GetInt("keepPlaying") == 1;
        }
        else PlayerPrefs.SetInt("keepPlaying", 0);
        if (ContiuneRound)
        {
            NumberofBots = PlayerPrefs.GetInt("botNumber");
            Invoke(nameof(Restart), 0.3f);
        }
    }
    //calling by play now button
    public void StartGame()
    {
        if(ContiuneRound)CardGenerator.instance.GenerateCards();
        PlayerCanvas.SetActive(true);
        SetPlayersByOrder();
        TotalBet = (NumberofBots + 1) * chosenBet;
        roomsPanel.SetActive(false);
        optionsPanel.SetActive(true);
        DeckController.Instance.DealCards();
        //take money from everyone
        foreach (Transform player in PlayingOrder)
        {
            player.GetComponent<TurnController>().MyBet -= chosenBet;
        }

        foreach (Transform _player in PlayingOrder)
        {
            _player.gameObject.SetActive(true);
        }
       
        SetPlayingOrder();     
    }

    [ContextMenu("Set Players by Order")]
    public void SetPlayersByOrder()
    {
        PlayingOrder.Clear();
        if(NumberofBots==1)
        {
            PlayingOrder.Add(Players[0]);            
            PlayingOrder.Add(Players[2]);            
        }
        else
        {
            foreach(Transform _player in Players)
            { 
                PlayingOrder.Add(_player);
            }
        }
        PlayingOrder[^1].GetComponent<Bot>().lastTurn = true;
    }
    public void SetPlayingOrder()
    {      
        turnCounter %= (NumberofBots + 1);
        for (int i =0; i < PlayingOrder.Count; i++)
        {
           
            if (i == turnCounter)
            {
                PlayingOrder[i].GetComponent<TurnController>().ToggleTurn(true);
            }
            else
            {
                PlayingOrder[i].GetComponent<TurnController>().ToggleTurn(false);
            }
        }
     
        turnCounter++;
    }
    public void ReDealCards()
    {
        if (DeckController.Instance.Cards.Count == 0)
        {   
            FinishRound();
            return;
        }
        DeckController.Instance.DealCards();
        for(int i=1; i < PlayingOrder.Count; i++)
        {
            PlayingOrder[i].GetComponent<Bot>().ResetCardPoints();
        }
    }

    [ContextMenu("Reset")]
    public void ResetGame()
    {
        ContiuneRound = false;
        SceneManager.LoadScene(0);
    }
    public void Snap(Vector2 callerPos)
    {
        if (fakeInitialCards.activeSelf)
        {
            fakeInitialCards.SetActive(false);
        }
        foreach(Transform playedCard in PlayedCards)
        {
            Vector2 target = callerPos;
            float time = Vector2.Distance(target, playedCard.transform.position)*0.1f;            
            playedCard.DOMove(target, time);
        }
        PlayedCards.Clear();
    }

    public void KeepPlaying()
    {
        PlayerPrefs.SetInt("botNumber", NumberofBots);
        SceneManager.LoadScene(0);
        ContiuneRound=true;
        PlayerPrefs.SetInt("keepPlaying", 1);
        //Restart();
        //Invoke(nameof(Restart), 1);
    }

    private void Restart()
    {
        
        Debug.Log(1);
        UISettings.instance.TogglePanels(tableOptionsIcon);
        StartGame();
        ContiuneRound = false;
        PlayerPrefs.SetInt("keepPlaying", 0);
        Debug.Log(2);
    }

    private void FinishRound()
    {
        int maxScore = 0;
        int maxCardNumber = 0;
        Transform winner = null;
        Transform playerWithMaxCards = null;
        foreach(Transform player in PlayingOrder)
        {
            TurnController turnController = player.GetComponent<TurnController>();            
            if(turnController.totalGainedCardNumber > maxCardNumber)
            {
                maxCardNumber = turnController.totalGainedCardNumber;
                playerWithMaxCards = player;
            }
        }
        //give more 3 points to player with max card number
        playerWithMaxCards.GetComponent<TurnController>().Score += 3;
        foreach (Transform player in PlayingOrder)
        {
            TurnController turnController = player.GetComponent<TurnController>();
            if (turnController.Score > maxScore)
            {
                winner = player;
                maxScore = turnController.Score;
            }

        }

        UISettings.instance.SetSaloonStats();
        winner.GetComponent<TurnController>().MyBet += TotalBet;
        foreach (Transform player in PlayingOrder)
        {
            if (player != winner) player.GetComponent<TurnController>().SetWinLost(false);
            else player.GetComponent<TurnController>().SetWinLost(true);
        }
        if (maxScore >= 101)
        {
            continueButton.SetActive(false);
        }

        UISettings.instance.SetScoreTableText(winner, maxScore);
       
    }



}
