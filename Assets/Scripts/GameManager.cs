using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public int NumberofBots;
    [HideInInspector] public int layerOrder;



    public GameObject PlayingArea;
    public GameObject PlayerPoint;



    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {

    }
}
