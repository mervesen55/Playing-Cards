using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public GameObject myPointer;

    [SerializeField] private bool isPlayer;

    private Bot bot;

    private void Awake()
    {
        if (TryGetComponent<Bot>(out Bot _bot)) { bot = _bot; }
    }

    public void ToggleTurn(bool _isMyTurn)
    {
        if(isPlayer) { Player.Instance.isMyTurn = _isMyTurn; }
        else bot.isMyTurn = _isMyTurn;
        myPointer.SetActive(_isMyTurn);
        if(!isPlayer && _isMyTurn) bot.Decide();
    }
}
