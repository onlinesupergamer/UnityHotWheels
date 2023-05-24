using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/*This whole file needs to be fixed, the component init seems to be wrong
 * 
 * 
 * 
 * */

public class CameraEffects : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    GameObject player;
    Rigidbody playerRb;
    Drive driveScript;
    CinemachineTransposer transposer;
    CinemachineBasicMultiChannelPerlin noiseIntensity;


    void Start()
    {
        player = GameObject.FindWithTag("Player");//This should check for the player tag
        playerRb = player.GetComponent<Rigidbody>();
        
        driveScript = player.GetComponent<Drive>();

        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        noiseIntensity = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
       
    }
 
    void Update()
    {
        if (player == null) 
        {
            
        }

        
        float playerSpeed = player.GetComponent<Drive>().speed;//Optimize, Everything in the method is slow

        float shake = playerSpeed * Time.deltaTime;
        Mathf.Clamp(shake, 0.0f, 1.0f);

        if (transposer == null) 
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        if (noiseIntensity == null) 
        {
            noiseIntensity = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        }


        if (!driveScript.isGrounded)
        {
            shake = 0.0f;
            noiseIntensity.m_AmplitudeGain = shake;
            transposer.m_FollowOffset.y = 2 * driveScript.timeInAir;
            


        }



            transposer.m_FollowOffset.y = 0.85f;



    }
}
