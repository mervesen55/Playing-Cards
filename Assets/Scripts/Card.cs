using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string cardName;

    private int cardValue;
    private int number;

    private bool numberCard;

    public string CardName
    {
        get { return cardName; }
        set { cardName = value; }
    }
    private void Awake()
    {
        Invoke(nameof(SetNumber), 0.1f);     
        Invoke(nameof(SetSprite), 0.1f);     
    }

    private void SetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
    }

    private void SetNumber()
    {
        char firstChar = cardName[0];
        Debug.Log("first char: " + firstChar);
        Debug.Log("first char is digit: " + Char.IsDigit(firstChar));
        if (Char.IsDigit(firstChar))
        {
            numberCard = true;
            number = (int)firstChar;
        }

        SetValue();
    }
    private void SetValue()
    {
        if (cardName.StartsWith("Ace") || cardName.StartsWith("Jack"))
        {
            cardValue = 1;
        }
        if (numberCard)
        {
            switch (cardName)
            {
                case "2ofClubs":
                    cardValue = 2;
                    break;

                case "10ofDiamonds":
                    cardValue = 3;
                    break;

                default: break;
            }
        }
    }
   

}
