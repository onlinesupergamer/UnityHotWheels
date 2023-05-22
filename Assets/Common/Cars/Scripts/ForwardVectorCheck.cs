using UnityEngine;

public class ForwardVectorCheck : MonoBehaviour
{        
      void Update()
    {
        
        Debug.DrawRay(transform.position, transform.forward * 5f);



    }
}
