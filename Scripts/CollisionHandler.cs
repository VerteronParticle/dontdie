using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float failureDelay = 1f;
    [SerializeField] float nextLevelDelay = 2f;
    [SerializeField] AudioClip failureSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] public float radius = 50f;
    [SerializeField] public float power = 10f;

    public bool godModeActive = false;
    public bool collisionDisabled = false;

    bool isTransitioning = false;
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    void Update() {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.C))
        {
            ToggleGodMode();
        }
        else if(Input.GetKeyDown(KeyCode.L)){
            StartNextLevelSequence();
        }else if(Input.GetKeyDown(KeyCode.V))
        {
            ToggleCollision();
        }
    }

    void ToggleCollision()
    {
        collisionDisabled = !collisionDisabled;
        Debug.Log($"Collision disabled {collisionDisabled}");
    }

    void ToggleGodMode()
    {
        godModeActive = !godModeActive;
        Debug.Log($"God mode {godModeActive}.");
    }

    void OnCollisionEnter(Collision other) {
        if(isTransitioning || collisionDisabled){return;}
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log($"Friendly expected: {other.gameObject.tag} is the real value.");
                break;
            case "Finish":                
                StartNextLevelSequence();
                break;            
            default:
                if (godModeActive == false){
                    StartCrashSequence();
                }                
                break;
        }
    }
    void DisableMovement(){
        GetComponent<Movement>().enabled = false;
    }
    void StartNextLevelSequence(){        
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        isTransitioning = true;
        DisableMovement();
        Invoke("LoadNextLevel", nextLevelDelay);
    }
    void StartCrashSequence(){        
        // todo add SFX upon crash
        // todo add particle effect upon crash    
        audioSource.Stop();            
        audioSource.PlayOneShot(failureSound);
        crashParticles.Play();
        isTransitioning = true;
        DisableMovement();
        GameObject originalGameObject = GameObject.Find("Capsule");
        for(int i = 0; i < originalGameObject.transform.childCount; i++){
            GameObject child = originalGameObject.transform.GetChild(i).gameObject;
            child.AddComponent<Rigidbody>();          
        }
        Invoke("ReloadLevel", failureDelay);
    }
    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;        
        SceneManager.LoadScene(currentSceneIndex);
    }
    
}
