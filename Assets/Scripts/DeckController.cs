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



    public List<string> Cards
    {
        get { return cards; }
        set { cards = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void SpawnCards(string cardName, int index, bool isforBot, int BotNo)
    {
        Vector2 pos = Player.Instance.cardPoints[index].position;
        GameObject newCard = GameObject.Instantiate(cardPrefab, pos, Quaternion.identity);       
        newCard.GetComponent<Card>().CardName = cardName;
        newCard.name = cardName;
        if (isforBot)
        {
            newCard.gameObject.SetActive(false);
            Bot bot = GameManager.Instance.PlayingOrder[BotNo].GetComponent<Bot>();
            bot.cardsInMyHand.Add(newCard.transform);
            //Transform availableCard = bot.GetAvailablePosition();
            //newCard.transform.position = availableCard.position;
            newCard.transform.position = GameManager.Instance.PlayingOrder[BotNo].position;
        }
    }

    public void DealCards()
    {
        bool isBot;
        for (int j= 0; j<= GameManager.Instance.NumberofBots; j++)
        {
            if (j == 0) isBot = false;
            else isBot = true;
            //deal 4 card for each person
            for (int i = 0; i < 4; i++)
            {
                int randomNumber = UnityEngine.Random.Range(0, cards.Count);
                string dealtCard = Cards[randomNumber];
                Cards.Remove(dealtCard);
                SpawnCards(dealtCard, i, isBot, j);
            }
        }
        
    }

    

}
