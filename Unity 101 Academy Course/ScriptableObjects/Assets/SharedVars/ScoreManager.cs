using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private SharedFloat score;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score.Value++;
        }
    }
}
