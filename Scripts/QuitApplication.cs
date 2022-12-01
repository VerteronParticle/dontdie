using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            quit();
        }
    }
    void quit(){
    Debug.Log("Player wants to quit like a sissy.");
    Application.Quit();
    }  
}

