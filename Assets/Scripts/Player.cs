using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Transform[] cardPoints;

    public GameObject myPointer;

    [HideInInspector] public bool isMyTurn;

    private void Awake()
    {
        Instance = this; 
    }
   
}
