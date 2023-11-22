using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    PathCreator creator;

    private void Start()
    {
        Vector3[] v = creator.path.localPoints;

        for(int i =0;i < v.Length; i++)
        {
            print(v[i]);
        }
    }
}

