using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movement = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    [Range(0, 1)]
    [SerializeField]
    float movementFactor;

    Vector3 initialPos;
    const float tau = Mathf.PI * 2f;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // comparing floats is bad because their decimals are unpredictable
        // Mathf.Epsilon is the lowest float possible, so if you are below, is as good as 0
        if (period <= Mathf.Epsilon) return;

        // The period is measured in seconds so if we divide all the time
        // that's passed since the Start() (regardless of FPS) and what we define as a period
        // we get the amount of cycles that's passed.
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);

        // because rawSinWave moves from -1 to 1, and we want to oscillate between 0 and 1
        // we first dive the sin wave by half, so it then moves from -0.5 to 0.5
        // then we add 0.5 to it, so it moves from 0 to 1
        movementFactor = rawSinWave / 2f + .5f;
        
        Vector3 newPos = initialPos + (movement * movementFactor);
        transform.position = newPos;
    }
}
