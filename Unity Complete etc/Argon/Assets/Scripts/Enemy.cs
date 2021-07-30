using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathFX;
    [SerializeField] private int scoreHit = 1;
    [SerializeField] private int hitsToKill = 5;
    
    private ScoreBoard _scoreBoard;
    
    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
        _scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider coll = gameObject.AddComponent<BoxCollider>();
        coll.isTrigger = false;
    }

    private void OnParticleCollision(GameObject bullet)
    {
        _scoreBoard.ScoreHit(scoreHit);
        hitsToKill--;
        if (hitsToKill <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(fx, 5);
        Destroy(gameObject);
    }
}
