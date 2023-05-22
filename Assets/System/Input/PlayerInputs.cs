using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public float throttleInput;
    public float steeringInput;


    private void Update()
    {
        throttleInput = Input.GetAxisRaw("Vertical");
        steeringInput = Input.GetAxisRaw("Horizontal");


    }
    

}
