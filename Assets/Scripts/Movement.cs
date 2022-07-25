using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem thrustEffect;
    [SerializeField] ParticleSystem rightthrustEffect;
    [SerializeField] ParticleSystem leftthrustEffect;


    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    // float mytime=Time.time;

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
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();

        }
        else
        {
            StopRotating();
        }
    }

    void StopThrusting()
    {
        thrustEffect.Stop();
        audioSource.Stop();
    }

    void StartThrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!thrustEffect.isPlaying)
        {
            thrustEffect.Play();
        }
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        // audioSource.Stop();
    }





    private void StopRotating()
    {
        leftthrustEffect.Stop();
        rightthrustEffect.Stop();
    }

    private void RotateRight()
    {
        if (!leftthrustEffect.isPlaying)
        {
            leftthrustEffect.Play();
        }
        ApplyRotation(-rotateThrust);
    }



    private void RotateLeft()
    {
        if (!rightthrustEffect.isPlaying)
        {
            rightthrustEffect.Play();
        }
        ApplyRotation(rotateThrust);
    }




    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;//freezing rotation so that we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing so that physics system can take over

    }



}
