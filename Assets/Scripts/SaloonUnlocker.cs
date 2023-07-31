using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaloonUnlocker : MonoBehaviour
{
    [SerializeField] private Image lockImage;

    [SerializeField] private Button playButton;
    [SerializeField] private Button createTableButton;

    [SerializeField] private int minBet;

    private void Start()
    {
        UnlockSaloon();
    }

    public void UnlockSaloon()
    {
        if (minBet > Player.Instance.GetComponent<TurnController>().MyBet) return;
        lockImage.enabled = false;
        playButton.interactable = true;
        createTableButton.interactable = true;
    }

}
