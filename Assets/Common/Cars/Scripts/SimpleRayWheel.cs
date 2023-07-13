using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRayWheel : MonoBehaviour
{
    Rigidbody rb;
    Drive driveScript;
    

    public Transform wheelModel;
    public float restDistance = 1f;
    public float springStrength = 50f;
    public float springDamper = 15f;
    public float rayDistance = 1;
    public float wheelModelOffset;
    

    Vector3 wheelPos;
    RaycastHit _hit;
    

    private void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
        driveScript = transform.root.GetComponent<Drive>();//Possible reference optimization
    }

    private void FixedUpdate()
    {
        
        float fdt = Time.fixedDeltaTime;

        if (Physics.Raycast(transform.position, -transform.up, out _hit, rayDistance))
        {
            Vector3 springDir = transform.up;

            //Make sure to project the forces relative to the track normal since
            //transform.up creates balance issues with the upward suspension forces because its fucking stupid

            //Edit: NEVER MIND; THE NORMALS CREATE ISSUES BECAUSE OF I DONT FUCKING KNOW AND THE CAR WILL BOUNCE EVEN WITH OVER 5.9 MILLION VERTICES
            //THIS WHOLE PROCESS IS STUPID JUST TO GET BASIC SUSPENSION
            //GAME FUCK STUPID

            Vector3 tireWorldVelocity = rb.GetPointVelocity(transform.position);
            float offset = restDistance - _hit.distance;                    
            float vel = Vector3.Dot(springDir, tireWorldVelocity);
            offset = Mathf.Clamp(offset, 0f, 0.5f);
            float force = (offset * springStrength) - (vel * springDamper);
           

            rb.AddForceAtPosition(springDir * force, transform.position);

            


            driveScript.isGrounded = true;
            driveScript.hit = _hit;

            wheelPos = transform.localPosition;
            wheelPos.y = transform.localPosition.y - _hit.distance + wheelModelOffset;

            wheelModel.transform.localPosition = wheelPos;


        }
        else 
        {
            driveScript.isGrounded = false;

            wheelPos = transform.localPosition;
            wheelPos.y = transform.localPosition.y - rayDistance + wheelModelOffset;
            wheelModel.transform.localPosition = wheelPos;

        }

    }

   
}
