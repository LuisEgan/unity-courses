using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderRotation : MonoBehaviour
{
    private Vector3 originalPos;
    private Vector3 originalRot;

    [SerializeField] private Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        Transform originalTransform = transform;
        originalPos = originalTransform.position;
        originalRot = originalTransform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnRotation(float value)
    {
        float angles = value * 360;
        transform.rotation = Quaternion.Euler(originalRot.x, angles, originalRot.z);
    }

    public void OnCircularPath(float value)
    {
        transform.localPosition = new Vector3(
            (originalPos.x + pivot.position.x) - Mathf.Sin(Mathf.PI * (value * 2)),
            originalPos.y,
            (originalPos.z + pivot.position.z) - Mathf.Cos(Mathf.PI * (value * 2))
        );
    }
}