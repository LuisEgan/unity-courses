using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform cameraT;

    private void Awake()
    {
        cameraT = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 dir = cameraT.position - transform.position;
        Vector3 projectedDir = Vector3.ProjectOnPlane(dir, Vector3.up);
        transform.forward = projectedDir;
    }
}