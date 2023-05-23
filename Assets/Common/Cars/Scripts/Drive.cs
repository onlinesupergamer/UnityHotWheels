using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Drive : MonoBehaviour
{

    float throttleInput;
    float steeringInput;
    
    public PlayerInputs playerInputs;


    public float steeringMultiplier;
    public float torqueMultiplier;

    public Transform[] wheelTransforms;
    public Transform[] frontWheels;
    public bool isGrounded;

    Vector3 gravityVector;
    Rigidbody rb;

    public RaycastHit hit;

    [HideInInspector]
    public float gravityForce;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInputs = GetComponent<PlayerInputs>();

    }

    private void FixedUpdate()
    {
        throttleInput = playerInputs.throttleInput;
        steeringInput = playerInputs.steeringInput;
        


        float speed = Vector3.Dot(transform.forward, rb.velocity);


        //The code here makes no sense, I don't know how this is working, but it fixed the inverted steering and I don't care why at this point
        if (speed < 0) steeringInput = steeringInput * -1f; else steeringInput = steeringInput * 1f;

        

        if (isGrounded)
        {

            gravityForce = 50;

            if (throttleInput != 0f && speed >= 10f)
                gravityVector = -hit.normal;
            

            foreach (Transform wheel in wheelTransforms)
            {
                Vector3 steeringDir = wheel.transform.right;
                Vector3 tireWorldVelocity = rb.GetPointVelocity(wheel.position);
                float steeringVelocity = Vector3.Dot(steeringDir, tireWorldVelocity);
                float desiredVelocityChange = -steeringVelocity * 2.5f;                //The multiplier controls grip by a value between 0-1, Higher means more grip                
                float desiredAcceleration = desiredVelocityChange / Time.deltaTime;
                
                rb.AddForceAtPosition(steeringDir * 5 * desiredAcceleration, wheel.position);

                Vector3 driveVector = wheel.forward * throttleInput * torqueMultiplier;

                rb.AddForceAtPosition(driveVector, wheel.position);
                             
            }


            float sideSpeed = Vector3.Dot(rb.velocity, transform.right);
            Vector3 friction = -transform.right * (sideSpeed / Time.deltaTime / 4); //Friction Support
            rb.AddForce(friction, ForceMode.Acceleration);

            //rb.AddTorque(transform.up * -rb.angularVelocity.y * 3500);

            if (rb.velocity.magnitude >= 100)
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 100);

        }

        else 
        {
            gravityForce = 2000;
            gravityVector = -Vector3.up;
        }

        foreach (Transform wheel in frontWheels) 
        {
            float steeringAmount = Mathf.Clamp(steeringMultiplier / speed, -30, 30);
            wheel.transform.localRotation = Quaternion.Euler(Vector3.up * steeringInput * steeringAmount); //Add Lerp

        }

        rb.AddForce(gravityVector * gravityForce * Time.fixedDeltaTime, ForceMode.Acceleration);



            


    }

}


