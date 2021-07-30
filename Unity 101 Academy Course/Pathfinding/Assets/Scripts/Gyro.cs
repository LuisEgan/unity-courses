using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;

    private void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            debugText.text = $"attitude == {Input.gyro.attitude.eulerAngles.ToString()}";
            transform.rotation = Input.gyro.attitude;
        }
    }
}