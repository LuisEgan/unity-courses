using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SharedFloatDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private SharedFloat sharedFloat;

    private void OnEnable()
    {
        sharedFloat.OnValueChanged += UpdateSharedFloat;
    }

    private void OnDisable()
    {
        sharedFloat.OnValueChanged -= UpdateSharedFloat;
    }

    private void UpdateSharedFloat(float newScore)
    {
        text.text = sharedFloat.Value.ToString(CultureInfo.InvariantCulture);
    }
}
