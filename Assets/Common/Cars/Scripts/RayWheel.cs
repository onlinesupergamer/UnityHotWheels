using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayWheel : MonoBehaviour
{
    private Rigidbody rb;
    DriveCustom driveScript;

    
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;
    public bool isGrounded;

    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springForce;
    private float damperForce;
    private float springVelocity;
    bool hasFallen = false;

    private Vector3 suspensionForce;

    
    public float wheelRadius;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
        driveScript = transform.root.GetComponent<DriveCustom>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void FixedUpdate()
    {


        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;

            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;
            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * hit.normal;

            hasFallen = false;
            isGrounded = true;

            rb.AddForceAtPosition(suspensionForce, hit.point);

        }
        else 
        {
            isGrounded = false;

            if (!hasFallen)
            {
                rb.AddForceAtPosition(-Vector3.up * 3000, transform.position);
                hasFallen = true;
                
            }

        }         
    }
}