using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCharacSelec : MonoBehaviour
{
    [SerializeField] GameObject[] charac;

    void OnEnable()
    {
        for (int i = 0; i < charac.Length - 1; i++) 
        {
            charac[i].SetActive(false);
        }       
        charac[Random.Range(0,charac.Length - 1)].SetActive(true);
    }
}
