using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    


    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 50f, 0);


    }
}
