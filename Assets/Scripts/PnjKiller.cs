using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dumb")
        { 
            if(other.gameObject.TryGetComponent<PNJMain>(out PNJMain main))
            {
                main.IAmDead();
            }
        }

    }   
}
