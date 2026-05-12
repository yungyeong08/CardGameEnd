using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    public float rotateSpeed = 10f; // 기존 rotateY 대신 속도 조절용 변수
    public bool isFront = false;    // 시작할 때는 뒷면(false)이어야 합니다.
    public int number;
    public bool isMatched = false;
    public CardGame cardGame;

    public GameObject cardBack;     // 뒷면 이미지 오브젝트
    public GameObject cardFront;    // 앞면 이미지 오브젝트 (추가 권장)
    public Image frontImage;        // 캐릭터가 그려지는 Image 컴포넌트

    private Quaternion flipRotation = Quaternion.Euler(0, 180f, 0);
    private Quaternion originalRotation = Quaternion.Euler(0, 0, 0);

    private void Update()
    {
        // 1. 목표 각도로 부드럽게 회전
        Quaternion targetRot = isFront ? flipRotation : originalRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

        // 2. 카드의 각도가 90도를 넘어가는 시점에 앞뒤 오브젝트를 교체
        // y축 회전값이 90도~270도 사이면 앞면이 보이게 설정
        float currentY = transform.rotation.eulerAngles.y;

        if (currentY > 90f && currentY < 270f)
        {
            cardBack.SetActive(false);
            if (cardFront != null) cardFront.SetActive(true);
        }
        else
        {
            cardBack.SetActive(true);
            if (cardFront != null) cardFront.SetActive(false);
        }
    }

    public void ClickCard()
    {
        if (isMatched || isFront) return; // 이미 맞춰졌거나 앞면이면 무시
        cardGame.onClickCard(this);
    }

    public void Flip(bool front)
    {
        isFront = front;
    }

    public void SetCardNumber(int newNumber)
    {
        number = newNumber;
    }

    public void SetImage(Sprite sprite)
    {
        frontImage.sprite = sprite;
    }

    public void ChangeColor(Color newColor)
    {
        frontImage.color = newColor;
    }
}