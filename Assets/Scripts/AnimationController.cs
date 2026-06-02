using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

public class AnimationController : MonoBehaviour
{
    public ExampleCharacterController character;
    public Animator animator;

    [SerializeField] private float animationSmoothTime = 0.1f;
    private float _animationBlend;

    // 이전 프레임의 지면 상태를 기억하기 위한 변수
    private bool _wasGrounded = true;

    private void Update()
    {
        // 1. 지면 상태 체크 (KCC 기능 활용)
        bool isGrounded = character.Motor.GroundingStatus.IsStableOnGround;
        animator.SetBool("Grounded", isGrounded);

        // 2. 점프 감지 (지면에 있다가 공중으로 순간 이동 && 위쪽 속도가 있을 때)
        // ※ 만약 CharacterController 스크립트에 점프 이벤트가 있다면 거기서 직접 trigger를 주셔도 됩니다.
        if (isGrounded && !_wasGrounded)
        {
            // 착지 순간에 필요한 처리가 있다면 여기에 작성
        }
        else if (!isGrounded && _wasGrounded && character.Motor.Velocity.y > 0.1f)
        {
            // 땅에 있었는데 지금은 공중이고, y축 속도가 위를 향한다면 -> 점프!
            animator.SetTrigger("Jump");
        }

        _wasGrounded = isGrounded;

        // 3. 기존 이동 속도 계산 로직
        Vector3 characterVelocity = character.Motor.Velocity;

        // 플랫폼(AttachedRigidbody) 속도 제거 (Unity 2021+ 기준 linearVelocity 사용 유지)
        if (character.Motor.AttachedRigidbody != null)
        {
            characterVelocity -= character.Motor.AttachedRigidbody.linearVelocity;
        }

        // 수평 속도만 사용
        float speed = new Vector3(characterVelocity.x, 0f, characterVelocity.z).magnitude;

        float maxSpeed = 5.335f;
        float normalizedSpeed = Mathf.Clamp01(speed / maxSpeed);

        _animationBlend = Mathf.Lerp(_animationBlend, normalizedSpeed, Time.deltaTime / animationSmoothTime);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        animator.SetFloat("Speed", _animationBlend);
    }
}