using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRB;
    AudioSource audioSource;
    [SerializeField] float rocketThrust = 1500f;
    [SerializeField] float rocketRotation = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){        
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StopThrusting()
    {
        mainEngineParticles.Stop();
        audioSource.Stop();
    }

    void StartThrusting()
    {
        rocketRB.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            PlayMainEngineFX();
        }
    }

    void PlayMainEngineFX()
    {
        mainEngineParticles.Play();
        audioSource.PlayOneShot(mainEngine);
    }

    void ProcessRotation(){
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
            StopRotation();
        }
    }

    void RotateLeft()
    {
        if (!rightThrust.isEmitting)
        {
            rightThrust.Play();
        }
        ApplyRotation(rocketRotation);
    }

    void StopRotation()
    {
        rightThrust.Stop();
        leftThrust.Stop();
    }

    void RotateRight()
    {
        if (!leftThrust.isEmitting)
        {
            leftThrust.Play();
        }
        ApplyRotation(-rocketRotation);
    }

    void ApplyRotation(float rotationThisFrame){
        rocketRB.freezeRotation = true;
        transform.Rotate(Vector3.left * rotationThisFrame * Time.deltaTime); 
        rocketRB.freezeRotation = false;       
    }
}
