using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Transform[] cardPoints;

    public GameObject myPointer;

    [HideInInspector] public bool isMyTurn;

    public int WinCount;
    public int LostCount;
    public int myBet;

    private void Awake()
    {
        Instance = this; 
    }
   
}
