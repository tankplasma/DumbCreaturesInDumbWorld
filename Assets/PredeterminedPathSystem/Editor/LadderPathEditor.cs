using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.WSA;

[CustomEditor(typeof(LadderPathCheckpoint)) , CanEditMultipleObjects]
public class LadderPathEditor : Editor
{
    private void OnSceneGUI()
    {
        LadderPathCheckpoint checkpoint = (LadderPathCheckpoint)target;

        for (int i = 0;i < checkpoint.anchors.Count; i++)
        {
            Vector3 newAnchor = Handles.PositionHandle(checkpoint.transform.position + checkpoint.anchors[i], Quaternion.identity);
            checkpoint.anchors[i] = checkpoint.transform.InverseTransformPoint(newAnchor);
        }
    }
}
