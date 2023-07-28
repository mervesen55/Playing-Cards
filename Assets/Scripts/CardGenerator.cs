using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField]
    private DeckController deckController;

    private List<string> CardTypes = new List<string> {"Clubs", "Diamonds", "Hearts", "Spades"};
    private List<string> CardSubTypes = new List<string> {"Ace", "Numbered", "Jack", "Queen", "King"};

    private void Start()
    {
        GenerateCards();
    }

    //need for loops in order to combinate card types and card subtypes such as "ace of clubs" or "queen of diamonds"
    public void GenerateCards()
    {
        for(int i= 0; i < CardTypes.Count; i++)
        {
            for(int  j= 0; j < CardSubTypes.Count; j++)
            {
                //to avoid generating names like "numbered hearts", seperated into two sections
                //in these two sections, names is generated and then added a list in card controller class
                if(j == 1)
                {
                    //this loop is for generate number cards such as "2 of hearts"
                    for(int k = 2; k < 11; k++)
                    {
                        deckController.Cards.Add(k.ToString() + "of" + CardTypes[i]);
                    }
                }
                else
                {
                    deckController.Cards.Add(CardSubTypes[j] + "of" + CardTypes[i]);
                }
            }
        }
    }

}
