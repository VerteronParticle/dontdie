using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class depthOscillator : MonoBehaviour
{
    Vector3 startingPosition;    
    [SerializeField] Vector3 movementVector;    
    float movementFactor;
    [SerializeField] float period = 4f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
        movementVector = new Vector3 (10,0,0);
    }
    // Update is called once per frame
    void Update()    
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSineWave + 1f) / 2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
