using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private Text touches;
    [SerializeField] private Transform building;

    private Vector3 initialScale;
    private float initialFingersDistance;

    private void Start()
    {
        initialScale = building.transform.localScale;
    }

    void Update()
    {
        touches.text = Input.touchCount.ToString();

        if (Input.touchCount == 2 && building)
        {
            Touch t1 = Input.touches[0];
            Touch t2 = Input.touches[1];

            if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
            {
                initialFingersDistance = Vector2.Distance(t1.position, t2.position);
                initialScale = building.transform.localScale;
            }
            else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
            {
                var currentFingersDistance = Vector2.Distance(t1.position, t2.position);
                var scaleFactor = currentFingersDistance / initialFingersDistance;
                // touches.text = currentFingersDistance.ToString();

                building.transform.localScale = initialScale * scaleFactor;
            }
        }
    }
}