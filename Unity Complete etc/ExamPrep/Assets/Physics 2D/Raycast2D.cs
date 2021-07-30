using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast2D : MonoBehaviour
{
    private void Start()
    {
        var hitInfo = Physics2D.Raycast(transform.position, Vector2.right);

        if (hitInfo.collider)
        {
            print(hitInfo.collider.name);
        }
    }
}
