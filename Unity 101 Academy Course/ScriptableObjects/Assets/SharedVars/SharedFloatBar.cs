using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SharedFloatBar : MonoBehaviour
{
    [SerializeField] private SharedFloat sharedFloat;
    [SerializeField] private Slider bar;

    private void OnEnable()
    {
        sharedFloat.OnValueChanged += SetBarValue;
    }

    private void OnDisable()
    {
        sharedFloat.OnValueChanged -= SetBarValue;
    }

    private void SetBarValue(float newValue)
    {
        bar.value = newValue;
    }
}