using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int NumberofBots;

    [HideInInspector] public int layerOrder;

    public int Timer;
    public int TotalBet;

    public char LastCard;
    public string CurrentCard;

    public bool RoundSetted;

    public List<Transform> Players = new List<Transform>();
    public List<Transform> PlayedCards = new List<Transform>();

    public GameObject PlayingArea;
    public GameObject PlayerPoint;
    public GameObject roomsPanel;
    public GameObject optionsPanel;
    public GameObject fakeInitialCards;


    public List<Transform> PlayingOrder = new List<Transform>();

    private int turnCounter;


    private void Awake()
    {
        Instance = this;
    }

    //calling by play now button
    public void StartGame()
    {
        SetPlayersByOrder();
        roomsPanel.SetActive(false);
        optionsPanel.SetActive(true);
        DeckController.Instance.DealCards();

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
        DeckController.Instance.DealCards();
        for(int i=1; i < PlayingOrder.Count; i++)
        {
            PlayingOrder[i].GetComponent<Bot>().ResetCardPoints();
        }
    }  

    public void Snap(Vector2 callerPos)
    {
        if(fakeInitialCards.activeSelf) fakeInitialCards.SetActive(false);
        foreach(Transform playedCard in PlayedCards)
        {
            Vector2 target = callerPos;
            float time = Vector2.Distance(target, playedCard.transform.position)*0.1f;            
            playedCard.DOMove(target, time);
        }
        PlayedCards.Clear();
    }

    private void DealInitialCards()
    {
        
    }
}
