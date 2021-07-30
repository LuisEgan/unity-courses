using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] patrolTransforms;
    [SerializeField] private Transform playerTransform;

    private delegate void UpdateFunc();

    private UpdateFunc _currentState;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // _currentState();
    }

    private void Patrol()
    {
        // patrol
        StartCoroutine(Cor_Patrol());
        // then, when player is seen
        _currentState = Chase;
    }

    private void Chase()
    {
        // chase
        StartCoroutine(Cor_Chase());
        // then, when the player is out of reach
        _currentState = Patrol;
    }

    private void Start()
    {
        // when the Cor_patrol coroutine received args
        // StartCoroutine(Cor_Patrol(patrolTransforms.Select(t => t.position).ToArray()));

        StartCoroutine(Cor_Patrol());
    }

    IEnumerator Cor_Patrol()
    {
        foreach (var point in patrolTransforms)
        {
            _agent.SetDestination(point.position);
            yield return 0;

            // other coroutines methods
            // yield return new WaitUntil(() => agent.remainingDistance < 2); // both of these do the same
            // yield return new WaitWhile(() => agent.remainingDistance > 2); // both of these do the same
            // yield return new WaitForSeconds(3);
            // yield return new WaitForSecondsRealtime(3);
            // yield return StartCoroutine(Cor_Chase());
            // yield break;

            while (_agent.remainingDistance > 2)
            {
                if (CanSeePlayer())
                {
                    StartCoroutine(Cor_Chase());
                    yield break; // stop coroutine
                }

                yield return 0;
            }
        }

        patrolTransforms.Reverse().ToArray();
        StartCoroutine(Cor_Patrol());
    }

    IEnumerator Cor_Chase()
    {
        while (true)
        {
            _agent.SetDestination(playerTransform.position);

            if (!CanSeePlayer())
            {
                StartCoroutine(Cor_Patrol());
                yield break; // stop couroutine
            }

            yield return 0;
        }
    }

    private bool CanSeePlayer()
    {
        Vector3 dir = playerTransform.position - transform.position;
        float chaseRadio = 15;
        if (dir.sqrMagnitude < chaseRadio * chaseRadio)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, dir, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}