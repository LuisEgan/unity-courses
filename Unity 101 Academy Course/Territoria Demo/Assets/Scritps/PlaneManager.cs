using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlaneManager_ : MonoBehaviour
{
    [SerializeField] private Transform building;
    
    private Vector3 originalPos;
    private bool isOriginalPosSet = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HandleInteractiveHitTest(HitTestResult result)
    {
        if (!isOriginalPosSet)
        {
            originalPos = result.Position;
            isOriginalPosSet = true;
        }

        building.position = originalPos;
        print("OriginalPos --> " + originalPos);
        print("building.position --> " + building.position);
        print("RESULT --> " + result);
    }
}