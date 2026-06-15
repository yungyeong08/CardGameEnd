using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 1. 충돌한 물체에 'CharacterController'나 'ExampleCharacter'가 있는지 검사
        // 유니티 공식 캐릭터 패키지는 보통 이 컴포넌트를 가지고 있습니다.
        bool isPlayer = other.GetComponent<CharacterController>() != null ||
                        other.gameObject.name.Contains("Character");

        if (isPlayer)
        {
            Debug.Log($"[성공] 플레이어가 코인을 먹었습니다: {other.gameObject.name}");

            // 점수 5점 추가 및 코인 파괴
            GameController.Instance.AddScore(5);
            Destroy(this.gameObject);
        }
        else
        {
            // 바닥(Floor)이나 다른 물체와 부딪혔을 때는 무시합니다.
            Debug.Log($"플레이어가 아닌 물체와 닿음: {other.gameObject.name}");
        }
    }
}