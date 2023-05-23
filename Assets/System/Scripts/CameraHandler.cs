using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    public GameObject playerCar;
    public Rigidbody playerRb;
    public Drive driveScript;
    


    public CinemachineVirtualCamera virtualCamera;


    float baseFOV = 55f;
    float fastFOVTarget = 70f;
    float boostFOVTarget = 75f;


    public void InitSettings() 
    {
 
        
        

    }
   

    private void Update()
    {
        if (driveScript == null) 
        {
            Debug.LogError("Player script class not found");
        }
        CameraChange();


    }

    void CameraChange() 
    {
       
    }



}
