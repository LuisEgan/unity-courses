using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var o = new ShowOptions {resultCallback = r => print(r)};
            Advertisement.Show(o);
        }
    }
}