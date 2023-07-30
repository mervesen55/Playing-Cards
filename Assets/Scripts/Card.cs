using System;
using UnityEngine;
using DG.Tweening;


public class Card : MonoBehaviour
{
    private string cardName;

    private char letter;

    private int cardValue;
    private int number;

    private bool isJack;
    private bool numberCard;
    private bool played;

    public char Letter
    {
        get { return letter; }
        set { letter = value; }

    }
    public string CardName
    {
        get { return cardName; }
        set { cardName = value; }
    }
    public bool Played
    {
        get { return played; }
        set { played = value; }
    }

    public bool IsJack
    {
        get { return isJack; }
        set { isJack = value; }
    }
    private void Awake()
    {
        Invoke(nameof(SetNumber), 0.1f);     
        Invoke(nameof(SetSprite), 0.1f);     
    }

    private void OnMouseDown()
    {
        if (Played || !Player.Instance.isMyTurn) return;
        played = true;
        Vector2 targetPosition = (GameManager.Instance.PlayingArea.transform.position) + (UnityEngine.Random.Range(-0.5f,0.5f)*Vector3.right);
        float time = Vector2.Distance(targetPosition, transform.position)*0.1f;
        transform.DOMove(targetPosition, time);
        int randomAngle = UnityEngine.Random.Range(-7, 7);
        GetComponent<Renderer>().sortingOrder = GameManager.Instance.layerOrder;
        GameManager.Instance.layerOrder++;

        transform.DORotate(randomAngle * Vector3.forward, time).OnComplete(() =>
        {
            randomAngle += 2 * (randomAngle/Math.Abs(randomAngle));
            transform.DORotate(Vector3.forward * randomAngle, 0.2f);
        });
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
