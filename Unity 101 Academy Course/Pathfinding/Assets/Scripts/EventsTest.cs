using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EventsTest : MonoBehaviour
{
    public delegate void HitPLayerResponder();

    // static means that yoou don't need a new object of this class to access this variable
    // Enemy.OnHitPlayer is enough, you don't need Enemy enemy = new Enemy();
    
    // "event" prevent from deleting all possible stacked functions like so
    // in the other script: Enemy.OnHitPlayer = null would fuck everything up
    // so it only lets you do something like Enemy.OnHitPlayer += ... (only += or -=)
    public static event HitPLayerResponder OnHitPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Fire event
            OnHitPlayer?.Invoke();
        }
    }


    delegate int MathFunc(int a, int b);

    private MathFunc sum;
    private MathFunc take;

    private void Start()
    {
        sum = A;
        take = B;
        sum += B;
        // print(sum(2, 2));
        // this will run both sum and B, but will return whatever B returns
        // and both funcs get the same params
    }

    int A(int a, int b)
    {
        return a + b;
    }

    int B(int a, int b)
    {
        return a - b;
    }
}