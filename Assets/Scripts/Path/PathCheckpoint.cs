using System;
using System.Collections.Generic;
using PathCreation;
using PathSystem;
using UnityEngine;
using UnityEngine.Splines;

public enum PathType
{
    Walk,
    Jump,
    Swim,
    Fall,
    Fly,
    Elevator,
    Ladder,
    None
}

[RequireComponent(typeof(SplineContainer))]
public class PathCheckpoint : MonoBehaviour
{
    [SerializeField]
    public PathType type;

    SplineContainer spline;

    [SerializeField]
    int numberOfPosInSpline = 30;

    public int importanceOfPath;

    [SerializeField]
    List<PathCheckpoint> checkpointList = new List<PathCheckpoint>();

    public bool canTake = true;

    private void Awake()
    {
        spline = GetComponent<SplineContainer>();
    }

    public SplineContainer GetSpline()
    {
        return spline;
    }

    public Vector3 GetFirstPos()
    {
        return spline.EvaluatePosition(0);
    }

    public Vector3 GetNextPos(float currentProgress , out float percentOfProgress)
    {
        float percentForOnePos = 1f/numberOfPosInSpline;
        percentOfProgress = Math.Clamp(currentProgress + percentForOnePos, 0, 1);
        return spline.EvaluatePosition(Math.Clamp(currentProgress, 0 , 1)); // clamp value because it' only between 0 and 1
    }

    public PathCheckpoint GetNextPathCheckpoint()
    {

        Debug.Log("get new checkpoint");
        PathCheckpoint newCheckpoint = null;

        foreach (var item in checkpointList)
        {
            if (!newCheckpoint)
                newCheckpoint = item;
            else
            {
                if(item.importanceOfPath > newCheckpoint.importanceOfPath && item.canTake)
                    newCheckpoint = item;
            }
        }
        return newCheckpoint;
    }

    /*[Serializable]
    public class NextPath
    {
        [HideInInspector] public string Name;
        public PathCheckpoint pathCheck;
    }

    public PathType pathType;
    public NextPath[] nextPath;
    [HideInInspector] public PathCreator path;
    [HideInInspector] public bool activated = false;

    void Awake()
    {
        path = GetComponent<PathCreator>();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            UpdateNamesInPathsList();
        }
    }

    /// <summary>
    /// Change le nom des intitulés des listes en fonction des enum choisie
    /// </summary>
    void UpdateNamesInPathsList()
    {
        if (nextPath != null)
        {
            for (int i = 0; i < nextPath.Length; i++)
            {
                nextPath[i].Name = nextPath[i].pathCheck.pathType.ToString();
            }
        }
    }

    public void SetNextPath(int index)
    {
        if (index + 1 <= nextPath.Length)
        {
            for (int i = 0; i < nextPath.Length; i++)
            {
                if (index == i)
                {
                    nextPath[i].pathCheck.activated = true;
                }
                else
                {
                    nextPath[i].pathCheck.activated = false;
                }
            }
        }
    }*/
}
