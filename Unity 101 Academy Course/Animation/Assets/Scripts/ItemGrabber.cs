using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    private Animator animator;
    private int speedHash;
    private int crouchHash;
    private float crouchTime = 1.3f;

    private Transform currentlyGrabbedItem;
    private float handIKWeight = 0;
    private float lookAtItemIK = 0;

    [Range(0, 7)] [SerializeField] private float detectRadius = 3;
    [SerializeField] private Transform holdingPoint;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
        crouchHash = Animator.StringToHash("Crouch");
    }

    // Start is called before the first frame update
    void Start()
    {
//        StartCoroutine(Cor_Test());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !currentlyGrabbedItem)
        {
            print("GRAB!");
            currentlyGrabbedItem = GetNearbyItem();
            if (currentlyGrabbedItem)
            {
                StartCoroutine(Cor_GrabItem());
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (currentlyGrabbedItem)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, currentlyGrabbedItem.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handIKWeight);

            animator.SetLookAtPosition(currentlyGrabbedItem.position);
            animator.SetLookAtWeight(lookAtItemIK);
        }
    }

    private Transform GetNearbyItem()
    {
        var cols = Physics.OverlapSphere(transform.position, detectRadius, 1 << LayerMask.NameToLayer("Item"));
        if (cols.Length > 0)
        {
            return cols[0].transform;
        }

        return null;
    }

    IEnumerator Cor_GrabItem()
    {
        // Disable player control
        GetComponent<PlayerMovement>().enabled = false;

        // Look at target
        transform.forward = Vector3.ProjectOnPlane(currentlyGrabbedItem.position - transform.position, Vector3.up);

        // Move towards target
        animator.SetFloat("Speed", 1);
        while (Vector3.ProjectOnPlane(currentlyGrabbedItem.position - transform.position, Vector3.up).magnitude > 1)
        {
            // yield return 0 waits 1 frame before continuing
            yield return 0;
        }

        // Stop walking
        animator.SetFloat("Speed", 0);

        // Look at item
        for (float time = 0; time < crouchTime; time += Time.deltaTime)
        {
            lookAtItemIK = time / crouchTime;
            yield return 0;
        }

        // Crouch + move hand to target
        for (float time = 0; time < crouchTime; time += Time.deltaTime)
        {
            handIKWeight = time / crouchTime;
            animator.SetFloat("Crouch", time / crouchTime);
            yield return 0;
        }

        // Attach item to hand
        currentlyGrabbedItem.SetParent(holdingPoint);
        currentlyGrabbedItem.localEulerAngles = new Vector3(-5.534f, -118.891f, 105.799f);
        currentlyGrabbedItem.localPosition = new Vector3(.4f, .4f, .04f);
        currentlyGrabbedItem.localScale = new Vector3(2.4f, 2.4f, 2.4f);


        // Stand up + stop moving hand
        for (float time = crouchTime; time > 0; time -= Time.deltaTime)
        {
            handIKWeight = time / crouchTime;
            animator.SetFloat("Crouch", time / crouchTime);
            yield return 0;
        }

        // Look up
        for (float time = crouchTime; time > 0; time -= Time.deltaTime)
        {
            lookAtItemIK = time / crouchTime;
            yield return 0;
        }

        // Enable player control
        GetComponent<PlayerMovement>().enabled = true;

        yield return 0;
    }

    IEnumerator Cor_Test()
    {
        bool goOn = false;

        while (!goOn)
        {
            yield return 0;
            goOn = Input.GetKey(KeyCode.K);
        }

        yield return new WaitForSeconds(3);
        yield return 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}