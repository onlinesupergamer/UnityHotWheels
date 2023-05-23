using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffects : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    GameObject player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");//This should check for the player tag

    }

    
    void Update()
    {
        if (player == null) 
        {
            
        }


        float playerSpeed = player.GetComponent<Drive>().speed;//Optimize, GetComponent is slow
        CinemachineBasicMultiChannelPerlin noiseIntensity = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        float shake = playerSpeed * Time.deltaTime;
        Mathf.Clamp(shake, 0.0f, 1.0f);

        noiseIntensity.m_AmplitudeGain = shake;


    }
}
