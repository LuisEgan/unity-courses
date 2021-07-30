using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    [SerializeField] [Range(1, 30)] private int gridSize = 10;
    [SerializeField] private LabelUpdate labelUpdate;

    private void Update()
    {
        Vector3 snapPos;
        var pos = transform.position;
        snapPos.x = Mathf.RoundToInt(pos.x / gridSize) * gridSize;
        snapPos.y = Mathf.RoundToInt(pos.y / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(pos.z / gridSize) * gridSize;

        labelUpdate.SetPositionLabel(snapPos);

        transform.position = snapPos;
    }
}