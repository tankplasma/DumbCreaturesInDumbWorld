using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionHandler : MonoBehaviour
{
    public UnityEvent missionStatusChanged;

    public bool IsMissionComplete;

    public void CompleteMission()
    {
        IsMissionComplete = true;
        missionStatusChanged.Invoke();
    }

    public void DiscompleteMission()
    {
        IsMissionComplete = false;
        missionStatusChanged.Invoke();
    }

    public void UnCompleteMission()
    {
        IsMissionComplete= false;
    }
}
