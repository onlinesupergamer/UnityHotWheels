using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CameraEffects : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; 
    public CinemachineBasicMultiChannelPerlin noiseIntensity;
    public AdvancedRayCastDrive driveScript;


    public void InitializeComponents() 
    {
        noiseIntensity = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        driveScript = GameObject.FindWithTag("Player").GetComponent<AdvancedRayCastDrive>();//I don't like this line



    }

    void Update()
    {
        if (noiseIntensity == null) 
        {
            Debug.LogError("Perlin noise not found");
            return;
        }
        if (driveScript == null) 
        {
            Debug.LogError("Drive Script not found, Game stupid");
            return;
        }


        if(driveScript.isGrounded)
            noiseIntensity.m_AmplitudeGain = Mathf.Clamp(driveScript.speed / 2 * Time.deltaTime, 0, 1.0f);
        else
            noiseIntensity.m_AmplitudeGain = 0f;



    }
}
