using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRigidbody;
    [SerializeField] float mainThrust = 500f;
    [SerializeField] float rotationThrust = 50f;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Pressed SPACE - Thrusting.");
            rocketRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Pressed A - Rotating Left");
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Pressed D - Rotating Right");
            ApplyRotation(-rotationThrust);

        }
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rocketRigidbody.freezeRotation = true; // Freezing rotation, so we can rotate manually (collision bugs)
        rocketRigidbody.transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        // (Vector3.back) == (-Vector.forward)
        rocketRigidbody.freezeRotation = false; // Unfreezing rotation, so the physics system can take over
    }
}
