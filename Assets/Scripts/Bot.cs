using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [HideInInspector] public bool isMyTurn;

    [HideInInspector] public int WinCount;
    [HideInInspector] public int LostCount;

    public Transform[] myCardPoints;

    public string[] cardsInMyHand;


    public void Decide()
    {

    }

    private void PlayCard()
    {

    }

}
