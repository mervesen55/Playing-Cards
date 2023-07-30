using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TableCreator : MonoBehaviour
{
    public static TableCreator Instance;

    [SerializeField] private TMP_Text minBetText;
    [SerializeField] private TMP_Text maxBetText;
    [SerializeField] private TMP_Text currentBetText;

    [SerializeField] private Slider slider;

    [SerializeField] private int peopleNumber=2;

    [SerializeField] Vector2[] minMaxBetRanges;

    private int minBet;
    private int maxBet;
    private int currentBet;

    public int PeopleNumber
    {
        get { return peopleNumber; }
        set { peopleNumber = value; }
    }

    private void Awake()
    {
        Instance = this;
    }
    public void SetCurrentText()
    {
        currentBet = minBet + (int)((maxBet - minBet) *(slider.value));
        Player.Instance.myBet = currentBet;
        currentBetText.text = currentBet.ToString();
        
    }

    public void SetNumberofPeople(int _peopleNumber)
    {
        peopleNumber = _peopleNumber;
        
    }

    public void CreateTable()
    {
        GameManager.Instance.NumberofBots = peopleNumber-1;
        GameManager.Instance.TotalBet = peopleNumber * currentBet;//create table demese de betin hesaplanmasý için playe aktar to do
        gameObject.SetActive(false);
    }

    public void SetBets(int minMaxRangeIndex)
    {
        minBet = (int)(minMaxBetRanges[minMaxRangeIndex].x);
        maxBet = (int)minMaxBetRanges[minMaxRangeIndex].y;
        currentBet = minBet;
        SetBetTexts();
    }
    private void SetBetTexts()
    {
        slider.value = 0;
        minBetText.text = minBet.ToString();
        maxBetText.text = maxBet.ToString();
        currentBetText.text = minBet.ToString();
    }
}
