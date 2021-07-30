using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analytics : MonoBehaviour
{
    private void Update()
    {
        
        #if !UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.Analytics.Analytics.CustomEvent("Player pressed space");

            UnityEngine.Analytics.Analytics.CustomEvent("Player leveled up", new Dictionary<string, object>()
            {
                {"exp", 1000},
                {"deaths", 3}
            });
        }
        #endif
    }
}
