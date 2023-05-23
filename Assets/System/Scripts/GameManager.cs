using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    
    public GameObject _PlayerCar;

    public bool hasRaceStarted = false;
    public bool hasRaceEnded = false;

    public Transform spawnLocation;
    public GameObject cameraSystem;

    public GameObject DefaultCar;
    public CinemachineVirtualCamera virtualCamera;


    void Start()
    {

        _PlayerCar = GlobalVariables.selectedCar;
        if (_PlayerCar == null) 
        {
            Debug.LogError("No Player Car Found, setting default model");

            _PlayerCar = DefaultCar;
        }
        _PlayerCar.GetComponent<PlayerInputs>().gmManager = this;



        GameObject cartmpbffr =  Instantiate(_PlayerCar, spawnLocation.transform.position, spawnLocation.transform.rotation);
        cartmpbffr.tag = "Player";

        GameObject liveTarget = GameObject.FindWithTag("CameraTarget");
        if (liveTarget == null || cartmpbffr == null)
            Debug.LogError("Camera is fucked, should not be null");

        virtualCamera.LookAt = liveTarget.transform;
        virtualCamera.Follow = liveTarget.transform;
        if (virtualCamera.LookAt == null)
            Debug.LogError("Still null, dipshit");


    }


    void PrepareRace()
    {
       
        
    }

    void PreRace() 
    {
        
        

    }

    private void StartRace()
    {

    }


}
