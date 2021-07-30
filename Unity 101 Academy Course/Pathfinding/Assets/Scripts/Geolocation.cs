using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Geolocation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI locationText;

    private IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            locationText.text = "Location not enabled by user";
            yield break;
        }

        Input.location.Start(.0001f, .0001f);

        yield return new WaitWhile(() => Input.location.status == LocationServiceStatus.Initializing);

        if (Input.location.status != LocationServiceStatus.Running)
        {
            locationText.text = $"Something went wrong :( ---> {Input.location.status.ToString()}";
            yield break;
        }
    }

    private void Update()
    {
        float latitude = Input.location.lastData.latitude;
        float longitude = Input.location.lastData.longitude;

        locationText.text = $"lat: {latitude} - long: {longitude}";
    }
}