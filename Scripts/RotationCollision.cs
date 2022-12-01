using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCollision : MonoBehaviour{
    AudioSource audioSource;
    [SerializeField] public AudioClip[] audioClips;
    public AudioClip dripSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other){
        PlaySplatter();
        Debug.Log($"Urine collided with {other.tag}");
    }
    void PlaySplatter(){
        audioSource = GetComponent<AudioSource>();
        dripSound = audioClips[Random.Range(0, audioClips.Length)];        
        audioSource.PlayOneShot(dripSound);        
    }
}