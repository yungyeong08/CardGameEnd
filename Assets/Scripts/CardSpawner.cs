using UnityEngine;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;
    public int cardCount = 8; // 짝수로 맞춰주세요!
    public float spacing = 150f; // UI 카드 간격 (픽셀 단위)
    public CardGame cardGame;

    void Start()
    {
        // 만약 CardGame의 cards 리스트가 비어있지 않다면 초기화 (중복 방지)
        cardGame.cards.Clear();

        for (int i = 0; i < cardCount; i++)
        {
            // 1. 생성 (부모를 이 스크립트가 붙은 오브젝트로 설정)
            GameObject obj = Instantiate(cardPrefab, transform);

            // 2. 위치 계산 (i값에 따라 옆으로 이동)
            // 시작 위치를 왼쪽으로 당기려면 (i - (cardCount-1)/2f) 처럼 계산하면 됩니다.
            float posX = i * spacing;
            obj.transform.localPosition = new Vector3(posX, 0, 0);

            // 3. 카드 정보 설정
            Card card = obj.GetComponent<Card>();
            if (card != null)
            {
                card.cardGame = cardGame;
                cardGame.cards.Add(card);
            }
        }

        // 모든 카드가 생성된 후 게임 시작!
        cardGame.StartGame();
    }
}