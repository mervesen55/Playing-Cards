using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController Instance;
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private List<string> cards = new List<string>();

    private int cardPointsIndex;
    private int botNo;

    private int counter;

    public List<string> Cards
    {
        get { return cards; }
        set { cards = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void SpawnCards(string cardName, bool isforBot, bool isInitialCard)
    {
        Vector2 pos = Player.Instance.cardPoints[cardPointsIndex].position;
        GameObject newCard = GameObject.Instantiate(cardPrefab, pos, Quaternion.identity);       
        newCard.GetComponent<Card>().CardName = cardName;
        newCard.name = cardName;
        if (isforBot)
        {
            newCard.gameObject.SetActive(false);
            Bot bot = GameManager.Instance.PlayingOrder[botNo].GetComponent<Bot>();
            bot.cardsInMyHand.Add(newCard.transform);
            newCard.transform.position = GameManager.Instance.PlayingOrder[botNo].position;
        }
        else if (isInitialCard)
        {
            GameManager.Instance.PlayedCards.Add(newCard.transform);
            if (counter<3) newCard.gameObject.SetActive(false);
            else
            {
                newCard.GetComponent<BoxCollider2D>().enabled = false;
                newCard.transform.position = GameManager.Instance.PlayingArea.transform.position + Vector3.right * UnityEngine.Random.Range(-0.5f, 0.5f);
                GameManager.Instance.LastCard = cardName[0];
                GameManager.Instance.fakeInitialCards.SetActive(true);
            }                
            
           
            counter++;
        }
    }
    private string ChoseCard()
    {
        int randomNumber = UnityEngine.Random.Range(0, cards.Count);
        string dealtCard = Cards[randomNumber];
        Cards.Remove(dealtCard);
        return dealtCard;
    }

    public void DealCards()
    {
        string dealtCard;
        if (!GameManager.Instance.RoundSetted)
        {
            GameManager.Instance.RoundSetted = true;
            for (int i = 0; i < 4; i++)
            {
                dealtCard = ChoseCard();
                SpawnCards(dealtCard, false, true);
            }
        }
        bool isBot;
        for (int j= 0; j<= GameManager.Instance.NumberofBots; j++)
        {
            botNo = j;
            if (j == 0) isBot = false;
            else isBot = true;
            //deal 4 card for each person
            for (int i = 0; i < 4; i++)
            {
                dealtCard = ChoseCard();
                cardPointsIndex = i;
                SpawnCards(dealtCard, isBot, false);
            }
        }
        
    }

    

}
