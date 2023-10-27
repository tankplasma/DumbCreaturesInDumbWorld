using System;
using PathCreation;
using PathSystem;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(PathCreator))]
public class PathCheckpoint : MonoBehaviour
{
    [Serializable]
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
    }
}
