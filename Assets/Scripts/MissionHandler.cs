using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionHandler : MonoBehaviour
{
    public UnityEvent missionComplete;

    public bool IsMissionComplete;

    public void CompleteMission()
    {
        IsMissionComplete = true;
        missionComplete.Invoke();
    }

    public void UnCompleteMission()
    {
        IsMissionComplete= false;
    }
}
