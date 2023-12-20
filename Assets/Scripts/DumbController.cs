using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbController : MonoBehaviour
{
    IPath path = null;

    bool useGravity;

    void UpdateByStatus()
    {

    }

    void DoPath()
    {

    }

    void GetNewPath()
    {
        path = path.GetNextPathCheckpoint();
    }

    void FinishPath() 
    {
        path = null;
    }

}

