using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathCheckpoint)), CanEditMultipleObjects]
public class PathCheckPointEditor : Editor
{
    private void OnSceneGUI()
    {
        PathCheckpoint pathCheckpoint = (PathCheckpoint)target;

        foreach (var item in pathCheckpoint.checkpointList)
        {
        }
    }
}
