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

   
    public void SetCurrentText()
    {
        //currentBet = minBet + (int)((maxBet - minBet) *(slider.value));
        slider.minValue = minBet;
        slider.maxValue = maxBet;
        if(Player.Instance.GetComponent<TurnController>().MyBet < slider.value)
        {
            slider.value = Player.Instance.GetComponent<TurnController>().MyBet;
        }
        currentBet = (int)slider.value;

        GameManager.Instance.chosenBet = currentBet;
        currentBetText.text = currentBet.ToString();
        
    }

    public void SetNumberofPeople(int _peopleNumber)
    {
        peopleNumber = _peopleNumber;
        
    }

    public void CreateTable()
    {
        GameManager.Instance.NumberofBots = peopleNumber-1;
        //GameManager.Instance.TotalBet = peopleNumber * currentBet;//create table demese de betin hesaplanmas� i�in playe aktar to do
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
