using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayWheel : MonoBehaviour
{
    private Rigidbody rb;
    AdvancedRayCastDrive scr_drive;

    public Transform wheelModel;
    public float wheelOffset;

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
    public float inAirRotation;

    private Vector3 suspensionForce;

    
    

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
        scr_drive = transform.root.GetComponent<AdvancedRayCastDrive>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void FixedUpdate()
    {

        Vector3 wheelPosition;

        wheelPosition = wheelModel.transform.localPosition;


        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength))
        {
            lastLength = springLength;

            springLength = hit.distance;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;
            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * hit.normal;

            hasFallen = false;
            isGrounded = true;
            scr_drive.isGrounded = true;

            rb.AddForceAtPosition(suspensionForce, hit.point);
            wheelPosition = new Vector3(0, -springLength + wheelOffset, 0);

            wheelModel.transform.localPosition = wheelPosition;

            wheelModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
            

        }
        else 
        {
            isGrounded = false;
            scr_drive.isGrounded = false;
            wheelPosition = new Vector3(0, -maxLength + wheelOffset, 0);
            float refVelocity = 0f;

            wheelModel.transform.localPosition = wheelPosition;

            
                
            wheelModel.transform.localRotation = Quaternion.Euler(0, 0, Mathf.SmoothDamp(0, inAirRotation, ref refVelocity, 2f * Time.deltaTime));


            if (!hasFallen)
            {
                rb.AddForceAtPosition(-Vector3.up * 3000, transform.position);
                hasFallen = true;
                
            }

        }         
    }
}