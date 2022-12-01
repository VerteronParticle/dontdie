using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCollision : MonoBehaviour{
    AudioSource audioSource;
    [SerializeField] public AudioClip[] audioClips;
    public AudioClip turdImpactSound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other){
        //playTurdImpact();
        Debug.Log($"Feces collided with {other.tag}");
    }
    void playTurdImpact(){
        audioSource = GetComponent<AudioSource>();
        turdImpactSound = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.PlayOneShot(turdImpactSound);
    }


}
