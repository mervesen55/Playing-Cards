using System;
using UnityEngine;
using DG.Tweening;


public class Card : MonoBehaviour
{
    private string cardName;
    [SerializeField]
    private char letter;

    private int cardValue;
    private int number;

    private bool isJack;
    private bool numberCard;
    private bool played;
    private bool isInitial;

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

    public bool IsInitial
    {
        get { return isInitial; }
        set { isInitial = value; }

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
        if (Played || !Player.Instance.isMyTurn) 
        {
            Debug.Log("it's not your turn ");
            return;
        }
        Player.Instance.isMyTurn = false;
        //Player.Instance.transform.GetComponent<TurnController>().ToggleTurn(false);
        played = true;
        MoveCard(Player.Instance.transform);
    }


    public void MoveCard(Transform player)
    {
        Vector2 targetPosition = (GameManager.Instance.PlayingArea.transform.position) + (UnityEngine.Random.Range(-0.5f, 0.5f) * Vector3.right);
        float time = Vector2.Distance(targetPosition, transform.position) * 0.1f;
        transform.DOMove(targetPosition, time);
        int randomAngle = UnityEngine.Random.Range(-7, 7);
        GetComponent<Renderer>().sortingOrder = GameManager.Instance.layerOrder;
        GameManager.Instance.layerOrder++;

        transform.DORotate(randomAngle * Vector3.forward, time).OnComplete(() =>
        {
            if (randomAngle == 0) randomAngle = 1;
            randomAngle += 2 * (randomAngle / Math.Abs(randomAngle));
            transform.DORotate(Vector3.forward * randomAngle, 0.2f).OnComplete(() =>
            {
                GameManager.Instance.PlayedCards.Add(transform);
                GameManager.Instance.totalValue += cardValue;
                //snap control
                if ((letter == GameManager.Instance.LastCard || isJack) && GameManager.Instance.PlayedCards.Count > 1)
                {
                    //take points
                    player.GetComponent<TurnController>().Score += GameManager.Instance.totalValue;
                    //super snap control
                    if(GameManager.Instance.PlayedCards.Count == 2 && (!isJack == (GameManager.Instance.LastCard != "J".ToCharArray()[0])))
                    {
                        //take 10 more points
                        player.GetComponent<TurnController>().Score += 10;
                     
                    }
                    int initialCardCount = 0;
                    if (!GameManager.Instance.initialCardsGained)
                    {
                        initialCardCount = 3;
                        GameManager.Instance.initialCardsGained = true;
                    }
                    //add gained card numbers
                    player.GetComponent<TurnController>().totalGainedCardNumber += GameManager.Instance.PlayedCards.Count + initialCardCount;
                    GameManager.Instance.totalValue = 0;
                    GameManager.Instance.Snap(player.position);
                }



                GameManager.Instance.LastCard = letter;
                GameManager.Instance.SetPlayingOrder();
            });
          
            
        });
    }
    private void SetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
    }

    private void SetNumber()
    {
        char firstChar = cardName[0];
        if (Char.IsDigit(firstChar))
        {
            numberCard = true;
            number = (int)firstChar;
        }
        //else
        {
            letter = firstChar;
            char J = "J".ToCharArray()[0];
            if(firstChar == J) IsJack = true;
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

        //GameManager.Instance.totalValue += cardValue;
    }
   

}
