/*This Script Is Outdated
 * DO NOT USE
 * 
 * 
 * 
 * 
 * 
*/
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;

public class DriveCustom : MonoBehaviour
{
    Rigidbody rb;

    float throttleInput;
    float steeringInput;

    public Transform[] frontWheels;
    public RayWheel[] wheels;
    public Transform[] wheelTransforms;

    public bool isGrounded;

    public float torqueMultiplier;
    public float steeringMultiplier;
    public LayerMask groundLayer;
    public float rayDistance;
    

    Vector3 gravityVector;
    bool recentlyfell;
    RaycastHit hit;

    [HideInInspector]
    public float gravityForce;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(0);
        }
    }


    void FixedUpdate()
    {

        throttleInput = Input.GetAxisRaw("Vertical");
        steeringInput = Input.GetAxisRaw("Horizontal");

        float speed = Vector3.Dot(transform.forward, rb.velocity);

        foreach (RayWheel wheels in wheels) 
        {
            if (wheels.isGrounded)
            {
                isGrounded = true;

            }
            else 
            {
                isGrounded = false;
            }
        }

        if (isGrounded)
        {

            float sideSpeed = Vector3.Dot(rb.velocity, transform.right);          
            Vector3 friction = -transform.right * (sideSpeed / Time.deltaTime / 12);
            rb.AddForce(friction, ForceMode.Acceleration);

            //This fix here adds a counter force to the body rotation, and I hate it
            rb.AddTorque(transform.up * -rb.angularVelocity.y * 3500);

            gravityForce = 500;
            gravityVector = -hit.normal;

            foreach (Transform wheel in wheelTransforms) 
            {
                Vector3 steeringDir = wheel.transform.right;
                Vector3 tireWorldVelocity = rb.GetPointVelocity(wheel.position);
                float steeringVelocity = Vector3.Dot(steeringDir, tireWorldVelocity);
                float desiredVelocityChange = -steeringVelocity * 0.35f;                //The multiplier controls grip by a value between 0-1                
                float desiredAcceleration = desiredVelocityChange / Time.deltaTime;

                
                

                rb.AddForceAtPosition(steeringDir * 5 * desiredAcceleration, wheel.position);

                
            }

            foreach (Transform wheel in wheelTransforms)
            {

                

                rb.AddForceAtPosition(wheel.forward * throttleInput * torqueMultiplier, wheel.position);
            }

            if (rb.velocity.magnitude >= 65)
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 65);

        }

        else 
        {
            gravityForce = 2000;
            gravityVector = -Vector3.up;

            if (!recentlyfell) 
            {
                rb.AddTorque(transform.right * 3000);
                recentlyfell = true;
            }

        }

        foreach (Transform wheelLocations in frontWheels)
        {
            float steeringAmount = Mathf.Clamp(steeringMultiplier / speed, -30, 30);
            

            wheelLocations.transform.localRotation = Quaternion.Euler(Vector3.up * steeringInput * steeringAmount); //Add Lerp

            print(steeringAmount);
        }   
        
        rb.AddForce(gravityVector * gravityForce * Time.fixedDeltaTime, ForceMode.Acceleration);

        
        

    }
}
