using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TableCreator : MonoBehaviour
{

    [SerializeField] private TMP_Text minBetText;
    [SerializeField] private TMP_Text maxBetText;
    [SerializeField] private TMP_Text currentBetText;

    [SerializeField] private int minBet;
    [SerializeField] private int maxBet;

    [SerializeField] private Slider slider;

    private int peopleNumber;

    private void Start()
    {
        SetBetTexts();
    }

    public void SetCurrentText()
    {
        int currentBet = minBet + (int)((maxBet - minBet) *(slider.value));
        currentBetText.text = currentBet.ToString();
        
    }

    public void SetNumberofPeople(int _peopleNumber)
    {
        peopleNumber = _peopleNumber;
    }

    public void CreateTable()
    {
        GameManager.Instance.NumberofBots = peopleNumber;
        gameObject.SetActive(false);
    }

    private void SetBetTexts()
    {
        minBetText.text = minBet.ToString();
        maxBetText.text = maxBet.ToString();
        currentBetText.text = minBet.ToString();
    }
}
