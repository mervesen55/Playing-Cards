using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettings : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject createTablePanel;
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private GameObject tableOptionsPanel;


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

    public void RestartGame()
    {

    }

}
