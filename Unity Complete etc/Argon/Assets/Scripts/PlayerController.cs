using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("m/s")] [SerializeField] private float xSpeed = 10f;
    [Tooltip("m/s")] [SerializeField] private float ySpeed = 15f;
    [SerializeField] private float xRange = 8.5f;
    [SerializeField] private float yRange = 3.8f;
    [SerializeField] private GameObject[] guns;
    
    [Header("Screen position")]
    [SerializeField] private float pitchFactor = -5f;
    [SerializeField] private float controlPitchFactor = -30f;
    
    [Header("Control throw")]
    [SerializeField] private float yawFactor = 2f;
    [SerializeField] private float rollFactor = -30f;

    private float xThrow, yThrow;
    private bool isControlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            MoveShip();
            RotateShip();
            ProccesFiring();
        }
    }

    private void ProccesFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    private void RotateShip()
    {
        // Quaternion originalRot = transform.localRotation;
        Vector3 originalPos = transform.localPosition;

        // * Pitch is the rotation around the X axis, so we want the ship
        // * to rotate up and down depending on where it is on the Y axis
        // * and depending if the user is pressing the up/down button
        
        // * By default the ship will always point to the center of the camera
        // * so with this we make it point forward so it can shoot enemies all over
        float pitchFromPos = originalPos.y * pitchFactor;
        // * This rotates the ship when moving, giving the effect of going up and down
        float pitchFromControl = yThrow * controlPitchFactor; 
        float pitch = pitchFromPos + pitchFromControl;
        
        // * Yaw is the rotation around the Y axis
        float yaw = originalPos.x * yawFactor;

        // * Roll is the rotation around the Z axis
        float roll = xThrow * rollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void MoveShip()
    {
        Vector3 originalPos = transform.localPosition;

        xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXpos = originalPos.x + xOffset;
        rawXpos = Mathf.Clamp(rawXpos, -xRange, xRange);

        yThrow = Input.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYpos = originalPos.y + yOffset;
        rawYpos = Mathf.Clamp(rawYpos, -yRange, yRange);

        transform.localPosition = new Vector3(rawXpos, rawYpos, originalPos.z);
    }
}