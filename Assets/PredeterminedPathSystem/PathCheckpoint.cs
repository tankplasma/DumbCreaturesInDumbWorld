using System;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using PathSystem;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.Splines;


public interface IPath
{
    public bool IsEnd {  get; }

    public PathType Type { get; }

    public Vector2 NodePos { get; set; }

    public bool IsAvailable { get; set; }

    public int Importance {  get;}

    public List<MonoBehaviour> GetCheckpoints();

    public void SetNewCheckpoint(MonoBehaviour path);

    public void RemoveCheckpoint(MonoBehaviour path);

    public void ReplaceCheckpoints(List<MonoBehaviour> paths);

    public Vector3 GetNextPos(float currentProgress, out float percentOfProgress);

    public IPath GetNextPathCheckpoint();
}

public enum PathType
{
    Walk,
    Jump,
    Swim,
    Fall,
    Fly,
    Elevator,
    Ladder,
    DownJump,
    JumpClimb,
    None
}

[RequireComponent(typeof(SplineContainer))]
public class PathCheckpoint : MonoBehaviour , IPath
{
    [SerializeField]
    public PathType type;

    protected SplineContainer spline;

    [SerializeField]
    protected int numberOfPosInSpline = 30;

    [SerializeField]
    public int importanceOfPath = 0;

    [SerializeField]
    public List<MonoBehaviour> checkpointList = new List<MonoBehaviour>();

    List<IPath> paths = new List<IPath>();

    public bool canTake = true;

    public PathType Type => type;

    bool IPath.IsAvailable { get => canTake; set => canTake = value; }

    public int Importance => importanceOfPath;

    Vector2 nodePosition = Vector2.zero;

    public Vector2 NodePos { get => nodePosition; set => nodePosition = value; }

    [SerializeField]
    bool isEnd;

    public bool IsEnd => isEnd;

    private void Awake()
    {
        spline = GetComponent<SplineContainer>();

        paths = checkpointList.Cast<IPath>().ToList();
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

    public IPath GetNextPathCheckpoint()
    {
        IPath newCheckpoint = paths[0];

        foreach (var item in paths)
        {
            if (item.IsAvailable)
            {
                if (!newCheckpoint.IsAvailable)
                {
                    newCheckpoint = item;
                    continue;
                }

                if (item.Importance > newCheckpoint.Importance)
                    newCheckpoint = item;
            }
        }
        return newCheckpoint;
    }

    public void SetNewCheckpoint(MonoBehaviour path)
    {
        checkpointList.Add(path);
    }

    public void RemoveCheckpoint(MonoBehaviour path)
    {
        checkpointList.Remove(path);
    }

    public void ReplaceCheckpoints(List<MonoBehaviour> paths)
    {
        checkpointList = paths;
    }

    public void ToggleActivation()
    {
        canTake = !canTake;
    }

    public List<MonoBehaviour> GetCheckpoints()
    {
        return checkpointList; 
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
