using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public float throttleInput;
    public float steeringInput;
    public bool bIsBoosting;
    public bool bIsBraking;

    public string throttleAxis = "Throttle";
    public string steeringAxis = "Steering";

    public GameManager gmManager;

    private void Update()
    {

        if (gmManager != null && gmManager.hasRaceEnded == false && gmManager.hasRaceStarted == true)
        {
            /*This probably isn't the best way to handle
            at all
            */


            throttleInput = Input.GetAxisRaw(throttleAxis);
            steeringInput = Input.GetAxisRaw(steeringAxis);
            bIsBoosting = Input.GetKey(KeyCode.LeftShift); //Switch to proper input
            bIsBraking = Input.GetKey(KeyCode.Space);
            


        }
        else
        {
            /*This is just for debugging
            */
            throttleInput = Input.GetAxisRaw(throttleAxis);
            steeringInput = Input.GetAxisRaw(steeringAxis);
            bIsBoosting = Input.GetKey(KeyCode.LeftShift);
            bIsBraking = Input.GetKey(KeyCode.Space);

            Debug.LogError("Debuging Player Inputs");
        }

    }
    

}
