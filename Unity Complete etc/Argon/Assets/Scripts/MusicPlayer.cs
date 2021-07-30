using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int allMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (allMusicPlayers > 1)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(LoadGame), 3);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}