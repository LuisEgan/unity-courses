using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadProjectsList()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadProject()
    {
        SceneManager.LoadScene(2);
    }
}