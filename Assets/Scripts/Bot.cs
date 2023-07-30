using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bot : MonoBehaviour
{
    [HideInInspector] public bool isMyTurn;

    [HideInInspector] public int WinCount;
    [HideInInspector] public int LostCount;
    [HideInInspector] public int MyBet;

    [HideInInspector] public int GainedValue;

    public GameObject myPointer;

    public bool lastTurn;

    public List<Transform> myCardPoints = new List<Transform>();
    public List<Transform> cardsInMyHand = new List<Transform>();

    private List<Transform> temporaryList = new List<Transform>();

    private Transform chosenCard;

    public Transform GetAvailableCardPoint()
    {
        int random;
        random = Random.Range(0, cardsInMyHand.Count);
        Transform position = myCardPoints[random];
        return position;
    }

    public void Decide()
    {
        if(!CheckSnap()) 
        {
            //if don't have snap chose a random one
            int randomNumber = Random.Range(0, cardsInMyHand.Count);
            chosenCard = cardsInMyHand[randomNumber];
        }
        //take some time before playing 
        Invoke(nameof(PlayCard), Random.Range(0, 1.1f));
    }

    private void PlayCard()
    {
        cardsInMyHand.Remove(chosenCard);
        
        Transform availablePoint = GetAvailableCardPoint();
        float time = Vector2.Distance(availablePoint.position, transform.position) * 0.1f;
        availablePoint.DOMove(transform.position, time).SetEase(Ease.InCirc).OnComplete(() =>
        {
            availablePoint.gameObject.SetActive(false);
            temporaryList.Add(availablePoint);
            myCardPoints.Remove(availablePoint);
            if(lastTurn && myCardPoints.Count == 0)
            {            
                GameManager.Instance.ReDealCards();
            }
            chosenCard.gameObject.SetActive(true);
            chosenCard.GetComponent<Card>().MoveCard(transform);
        });
        
    }

    public void ResetCardPoints()
    {
        foreach (Transform t in temporaryList)
        {
            t.transform.DOLocalMove(Vector2.zero, 1);
            t.gameObject.SetActive(true);
            myCardPoints.Add(t);
        }
        temporaryList.Clear();
    }

    private bool CheckSnap()
    {
        //choose if have a snap 
        foreach (Transform card in cardsInMyHand)
        {
            Card CardScript = card.GetComponent<Card>();
            if (CardScript.Letter == GameManager.Instance.LastCard || CardScript.IsJack)
            {
                chosenCard = card;
                return true;
            }           
        }
        return false;
    }
}
