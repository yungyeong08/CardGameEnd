using UnityEngine;
using KinematicCharacterController;

public class PlayerRespawn : MonoBehaviour
{
    public KinematicCharacterMotor Motor;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = Motor.TransientPosition;
        startRotation = Motor.TransientRotation;
    }

    void Update()
    {
        if (Motor.TransientPosition.y < -20f)
        {
            Motor.SetPositionAndRotation(startPosition, startRotation);
        }
    }
}