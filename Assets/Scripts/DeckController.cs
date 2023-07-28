using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private List<string> cards = new List<string>();

    public List<string> Cards
    {
        get { return cards; }
        set { cards = value; }
    }

    private void SpawnCards(string cardName)
    {
        GameObject newCard = GameObject.Instantiate(cardPrefab);
       newCard.GetComponent<Card>().CardName = cardName;
       newCard.GetComponent<Card>().name = cardName;
    }

    [ContextMenu("Deal")]
    private void DealCards()
    {
        int randomNumber = UnityEngine.Random.Range(0, cards.Count);
        string dealtCard = Cards[randomNumber];
        Cards.Remove(dealtCard);
        SpawnCards(dealtCard);
    }

    

}
