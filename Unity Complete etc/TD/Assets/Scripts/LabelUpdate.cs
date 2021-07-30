using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LabelUpdate : MonoBehaviour
{
    [SerializeField] private TextMesh label;

    public void SetPositionLabel(Vector3 snapPos)
    {
        var newLabel = $"{snapPos.x},{snapPos.z}";
        label.text = newLabel;
        gameObject.name = newLabel;
    }
}