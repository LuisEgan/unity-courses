using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int speedHash;
    private int crouchHash;
    private Camera camera;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
        crouchHash = Animator.StringToHash("Crouch");

        camera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float c = Input.GetAxis("Crouch");

        Crouch(c);
        Rotate(h, v);
        MoveForward(h, v);
    }

    private void Rotate(float h, float v)
    {
        // Don't rotate if both v and h are 0
        // Because it will rotate to vector 0 and it breaks
        // And forward should never be 0
        if (h != 0 || v != 0)
        {
            // Get the projection of the camera's forward vector on the XZ plane
            // So the character doesn't move towards the ground, but parallel to it
            Vector3 cameraForward = camera.transform.forward;
            cameraForward = Vector3.ProjectOnPlane(cameraForward, Vector3.up);

            Vector3 cameraRight = camera.transform.right;

            Vector3 dir = (cameraRight * h) + (cameraForward * v);
            transform.forward = dir;
        }
    }

    private void MoveForward(float h, float v)
    {
        animator.SetFloat(speedHash, Mathf.Max(Mathf.Abs(h), Mathf
            .Abs(v)));
    }

    private void Crouch(float c)
    {
        animator.SetFloat(crouchHash, c);
    }
}