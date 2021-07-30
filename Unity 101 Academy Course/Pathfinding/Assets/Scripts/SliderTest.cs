using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderTest : MonoBehaviour
{
    public void OnChange(float value)
    {
        transform.localScale = Vector3.one * value;
    }
}