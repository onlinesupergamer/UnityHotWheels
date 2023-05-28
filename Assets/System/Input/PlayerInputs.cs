using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public float throttleInput;
    public float steeringInput;

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



        }

    }
    

}
