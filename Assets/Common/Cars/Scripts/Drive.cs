using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Drive : MonoBehaviour
{

    float throttleInput;
    float steeringInput;       

    PlayerInputs playerInputs;

    public float steeringMultiplier;
    public float torqueMultiplier;

    public Transform[] wheelTransforms;
    public Transform[] frontWheels;
    public Transform[] frontWheelModels;
    public bool isGrounded;

    Vector3 gravityVector;
    public Rigidbody rb;
    

    public RaycastHit hit;
    public float timeInAir;


    [HideInInspector]
    public float gravityForce;
    public float speed;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInputs = GetComponent<PlayerInputs>();

    }

    private void FixedUpdate()
    {
        throttleInput = playerInputs.throttleInput;
        steeringInput = playerInputs.steeringInput;
     

        speed = Vector3.Dot(transform.forward, rb.velocity);

        //Inverted steering needs to be fixed here
        if (speed < 0) steeringInput = steeringInput * -1f; else steeringInput = steeringInput * 1f;
     

        if (isGrounded)
        {

            gravityForce = 1500;

            if (throttleInput != 0f && speed >= 10f)
                gravityVector = -hit.normal;
            

            foreach (Transform wheel in wheelTransforms)
            {
                Vector3 steeringDir = wheel.transform.right;
                Vector3 tireWorldVelocity = rb.GetPointVelocity(wheel.position);
                float steeringVelocity = Vector3.Dot(steeringDir, tireWorldVelocity);
                float desiredVelocityChange = -steeringVelocity * 2.5f;                //The multiplier controls grip by a value between 0-5, Higher means more grip                
                float desiredAcceleration = desiredVelocityChange / Time.deltaTime;
                
                rb.AddForceAtPosition(steeringDir * 5 * desiredAcceleration, wheel.position);

                Vector3 driveVector = wheel.forward * throttleInput * torqueMultiplier;

                rb.AddForceAtPosition(driveVector, wheel.position);
                             
            }


            float sideSpeed = Vector3.Dot(rb.velocity, transform.right);
            Vector3 friction = -transform.right * (sideSpeed / Time.deltaTime / 25); //Friction Support
            rb.AddForce(friction, ForceMode.Acceleration);


            timeInAir = 0;
            rb.angularDrag = 0;

            if (rb.velocity.magnitude >= 100)
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 100);

        }

        else 
        {
            gravityForce = 2000;
            gravityVector = -Vector3.up;

            rb.angularDrag = 2.5f;
            timeInAir += Time.deltaTime;


        }         

        foreach (Transform wheel in frontWheels) 
        {
            float steeringAmount = Mathf.Clamp(steeringMultiplier / speed, -20, 20);
            wheel.transform.localRotation = Quaternion.Euler(Vector3.up * steeringInput * (steeringAmount / 1.25f)); //Add Lerp here as the steering force is applied to the rotation, smoothing the rotation may help handling

        }

        foreach (Transform wheel in frontWheelModels)
        {
            
            wheel.transform.localRotation = Quaternion.Euler(Vector3.up * steeringInput * 45f); //Add Lerp

        }

        rb.AddForce(gravityVector * gravityForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        
    }

}