using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 500f;
    [SerializeField] float rotationThrust = 50f;
    [SerializeField] AudioClip mainThrustSFX;

    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rocketRigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Stop();
        }
    }

    void StartThrusting()
    {
        Debug.Log("Pressed SPACE - Thrusting.");
        rocketRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainThrustSFX);
        }
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }
    void RotateLeft()
    {
        Debug.Log("Pressed A - Rotating Left");
        ApplyRotation(rotationThrust);
        if (!rightThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    void RotateRight()
    {
        Debug.Log("Pressed D - Rotating Right");
        ApplyRotation(-rotationThrust);
        if (!leftThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Play();
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
