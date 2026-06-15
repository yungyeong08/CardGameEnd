using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

public class AnimtionController : MonoBehaviour
{
    public ExampleCharacterController character;

    public Animator animator;

    [SerializeField] private float animationSmoothTime = 0.1f;
    private float _animationBlend;

    private void Update()
    {
        Vector3 characterVelocity = character.Motor.Velocity;

        // 플랫폼(AttachedRigidbody) 속도 제거
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

        animator.SetFloat("Speed", new Vector3(character.Motor.Velocity.x, 0, character.Motor.Velocity.z).magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
        else
        {
            animator.ResetTrigger("Jump");
        }
    }
}
