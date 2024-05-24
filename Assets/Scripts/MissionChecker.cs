using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class MissionChecker : MonoBehaviour
{
    [SerializeField]
    UnityEvent MissionComplete , MissionNotComplete;

    bool missionsWasComplete;

    [SerializeField]
    List<MissionHandler> missions;

    private void Awake()
    {
        foreach (var mission in missions)
        {
            mission.missionStatusChanged.AddListener(CheckMissionComplete);
        }
    }

    void CheckMissionComplete()
    {
        if (AreAllMissionsComplete())
        {
            Debug.Log("mission are completes");
            missionsWasComplete = true;
            MissionComplete.Invoke();
        }
        else
            if(missionsWasComplete)
                MissionNotComplete.Invoke();
    }

    bool AreAllMissionsComplete()
    {
        foreach (var mission in missions)
            if (!mission.IsMissionComplete)
                return false;
        return true;
    }
}
