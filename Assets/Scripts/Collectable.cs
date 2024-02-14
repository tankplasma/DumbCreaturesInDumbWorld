using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    piece,
    trophy
}

[RequireComponent(typeof(Collider))]
public class Collectable : MonoBehaviour
{
    [SerializeField]
    CollectableType type;

    private void OnTriggerEnter(Collider other)
    {
        print("enter");

        if (other.tag == "Dumb")
        {
            if (other.gameObject.TryGetComponent(out PNJMain main))
            {
                main.OnCollectableCollected(this);      
            }
        }
    }

    public void OnCollected()
    {
        GetComponent<Animator>().SetTrigger("Collected");
    }

    public void Disabled() 
    {
        gameObject.SetActive(false);
    }
}
