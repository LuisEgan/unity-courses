using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshLinkJumping : MonoBehaviour
{
    [SerializeField] float jumpHeight = 6f;

    private NavMeshAgent agent;
    private bool isJumping;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // if the object that's subscribed would be destroyed, we must unsubscribe from the event before (onDisable)
        // OJO >>>> Awake and OnEnabled happens together for each object, so we CAN'T be sure that some other object's
        // Awake or OnEnabled happened when we call Awake or OnEnabled
        // Destroy(this);
    }

    private void OnEnable()
    {
        // Recommended function to subscribe to events
        EventsTest.OnHitPlayer += RespondToHitPlayer;
    }

    private void OnDisable()
    {
        // Recommended function to unsubscribe to events
        EventsTest.OnHitPlayer -= RespondToHitPlayer;
    }

    void RespondToHitPlayer()
    {
        print("jumpling was notified to hit player event");
    }


    private void Update()
    {
        if (!isJumping && agent.isOnOffMeshLink)
        {
            StartCoroutine(Cor_Jump());
        }
    }

    IEnumerator Cor_Jump()
    {
        isJumping = true;

        Vector3 origin = agent.currentOffMeshLinkData.startPos;
        Vector3 end = agent.currentOffMeshLinkData.endPos;

        // "norrmalized" = value goes from 0 to 1;
        float normalizedTime = 0;
        float timeToTravel = 1;
        while (normalizedTime <= 1)
        {
            Vector3 pos = Vector3.Lerp(origin, end, normalizedTime);
            transform.position =
                pos + Vector3.up * (normalizedTime - (normalizedTime * normalizedTime)) * jumpHeight; // x - x^2

            // we divive what we sum to normalizedTime so it takes that much longer to reach 1
            // so, if we divide by 2, for example, it takes 2 seconds to reach the destination
            // because I'd be adding half the time to normalized each loop
            normalizedTime += Time.deltaTime / timeToTravel;

            yield return null;
        }

        // This is so the agent understands it's reached the target.
        agent.CompleteOffMeshLink();

        isJumping = false;
    }
}