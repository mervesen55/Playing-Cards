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

    private void SpawnCards(string cardName, int index)
    {
        
            Vector2 pos = Player.Instance.cardPoints[index].position;
            GameObject newCard = GameObject.Instantiate(cardPrefab, pos, Quaternion.identity);
            newCard.GetComponent<Card>().CardName = cardName;
            newCard.name = cardName;
             
    }

    [ContextMenu("Deal")]
    private void DealCards()
    {
        
        for (int i = 0; i < 4; i++)
        {
            int randomNumber = UnityEngine.Random.Range(0, cards.Count);
            string dealtCard = Cards[randomNumber];
            Cards.Remove(dealtCard);
            SpawnCards(dealtCard, i);
        }
            
    }

    

}
