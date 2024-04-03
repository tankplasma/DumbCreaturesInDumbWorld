using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class MissionChecker : MonoBehaviour
{
    [SerializeField]
    UnityEvent MissionComplete;

    [SerializeField]
    List<MissionHandler> missions;

    private void Awake()
    {
        foreach (var mission in missions)
        {
            mission.missionComplete.AddListener(CheckMissionComplete);
        }
    }

    void CheckMissionComplete()
    {
        if(AreAllMissionsComplete())
            MissionComplete.Invoke();
    }

    bool AreAllMissionsComplete()
    {
        foreach (var mission in missions)
            if (!mission.IsMissionComplete)
                return false;
        return true;
    }
}
