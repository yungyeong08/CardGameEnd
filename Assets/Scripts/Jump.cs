using KinematicCharacterController.Examples;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float power = 40f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        ExampleCharacterController chracter = other.GetComponent<ExampleCharacterController>();

        chracter.AddVelocity(Vector3.up * power);
    }
}