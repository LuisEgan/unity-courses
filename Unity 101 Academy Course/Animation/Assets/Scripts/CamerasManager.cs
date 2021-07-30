using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Cameras;
        
    // Make dictionary
    // SubscribeCam -> check if vcam is not already in the dict then add using hashes as id
    // this is meant to be called from the other classes who want to set a cam
    // SetCam -> takes the camera hash and gives a priority of 1000
    
    public GameObject[] GetAllCameras
    {
        get => Cameras;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
