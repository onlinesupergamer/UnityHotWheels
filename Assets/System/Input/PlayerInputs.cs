using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public float throttleInput;
    public float steeringInput;

    public GameManager gmManager;

    private void Update()
    {

        if (gmManager != null && gmManager.hasRaceEnded == false && gmManager.hasRaceStarted == true)
        {

            throttleInput = Input.GetAxisRaw("Throttle");
            steeringInput = Input.GetAxisRaw("Steering");

        }

    }
    

}
