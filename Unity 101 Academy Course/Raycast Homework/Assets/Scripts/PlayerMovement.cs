using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private bool doJump = false;
    private Rigidbody rb;
    private RaycastHit hit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (doJump && OnGround())
        {
            rb.velocity = Vector3.up * 3;
        }

        doJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doJump = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * .7f);
    }

    private Collider GetGroundCollider()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, Vector3.down, out hitInfo, .7f);
            // out is the keyword for pointer to pass the value by reference
            // RaycastHit is an struct, which means is a value type
            // opposed to other a class which would be a reference type
            // hence the use of out
        return hitInfo.collider;
    }

    private bool OnGround()
    {
        return GetGroundCollider();
    }
}