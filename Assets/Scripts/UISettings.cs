using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISettings : MonoBehaviour
{
    public static UISettings instance;

    [SerializeField] private GameObject[] panels;

    [SerializeField] private GameObject scoreInfoPanel;

    [SerializeField] private TMP_Text ScoreTableText;

    [SerializeField] private SaloonUnlocker[] saloonUnlockers;

    [SerializeField] private TMP_Text playerWinText;
    [SerializeField] private TMP_Text playerLostText;
    [SerializeField] private TMP_Text playerBetText;

    private void Awake()
    {
        instance = this;
    }

    public void CreateTable()
    {

    }
    public void TogglePlayerCount()
    {

    }
    public void TogglePanels(GameObject panelToOpen)
    {
        foreach(GameObject panel in panels)
        {
            if(panel != panelToOpen)  panel.SetActive(false); 
            else panel.SetActive(true);
        }
    }

    public void SetScoreTableText(Transform winner, int score)
    {
        scoreInfoPanel.SetActive(true);
        ScoreTableText.text =  "Round is finished, " + winner + " winned! with " + score + " points!";
    }

    public void SetSaloonStats()
    {
        foreach(SaloonUnlocker _saloonUnlocker in saloonUnlockers)
        {
            _saloonUnlocker.UnlockSaloon();
        }
    }
    public void SetProfile()
    {
        TurnController turnController = Player.Instance.GetComponent<TurnController>();
        playerBetText.text = turnController.MyBet.ToString();
        playerLostText.text = turnController.LostCount.ToString();
        playerWinText.text = turnController.WinCount.ToString();


    }

}
