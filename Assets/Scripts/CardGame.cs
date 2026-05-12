using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardGame : MonoBehaviour
{

    public List<Card> cards = new List<Card>();
    public List<Sprite> sprite = new List<Sprite>();
    private Card firstCard = null;
    private Card secondCard = null;
    private bool isChecking = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        List<int> pairNumbers = GenerratePairNumbers(cards.Count);

        for (int i = 0; i < pairNumbers.Count; i++)
        {
            cards[i].SetCardNumber(pairNumbers[i]);
            cards[i].SetImage(sprite[pairNumbers[i]]);
        }
        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].isFront = false;
        }
    }

    void CheakCard()
    {
        isChecking = true;

        if (firstCard.number == secondCard.number)
        {
            firstCard.ChangeColor(Color.pink);
            secondCard.ChangeColor(Color.pink);

            firstCard.isMatched = true;
            secondCard.isMatched = true;

            firstCard = null;
            secondCard = null;

            isChecking = false;

        }
        else
        {
            Invoke("HideCard", 1.0f);
        }
    }

    public void onClickCard(Card card)
    {
        if (isChecking)
        {
            return;
        }

        if (card == firstCard)
        {
            return;
        }

        if (firstCard == null)
        {
            firstCard = card;
            firstCard.isFront = true;
        }
        else
        {
            secondCard = card;
            secondCard.Flip(true);
        }

        if (firstCard != null && secondCard != null)
        {
            CheakCard();
        }

    }

    void HideCard()
    {
        firstCard.isFront = false;
        secondCard.isFront = false;

        firstCard.Flip(false);
        secondCard.Flip(false);

        firstCard = null;
        secondCard = null;

        isChecking = false;
    }

    List<int> GenerratePairNumbers(int cardCount)
    {
        if (cardCount % 2 != 0)
        {
            Debug.LogError("짝수여야 합니다.");
            return null;
        }

        int pairCount = cardCount / 2;

        List<int> newCardNumbers = new List<int>();

        for (int i = 0; i < pairCount; ++i)
        {
            newCardNumbers.Add(i);
            newCardNumbers.Add(i);
        }

        for (int i = newCardNumbers.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);

            int temp = newCardNumbers[i];

            newCardNumbers[i] = newCardNumbers[rnd];
            newCardNumbers[rnd] = temp;
        }

        return newCardNumbers;
    }
}