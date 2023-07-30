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

    public string LastCard;
    public string CurrentCard;

    public List<Transform> Players = new List<Transform>();

    public GameObject PlayingArea;
    public GameObject PlayerPoint;

    [SerializeField]
    private List<Transform> PlayingOrder = new List<Transform>();


    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {

    }

    [ContextMenu("Set Order")]
    public void SetPlayingOrder()
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
    }
}
