using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionChecker : MonoBehaviour
{
    [SerializeField]
    UnityEvent MissionComplete;

    [SerializeField]
    List<MissionHandler> missions;

    void CheckMissionComplete()
    {

    }
}
