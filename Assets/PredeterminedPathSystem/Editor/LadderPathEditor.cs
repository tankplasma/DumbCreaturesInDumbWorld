using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        if (!checkpoint.showAnchors)
            return;

        Vector3 anchor = Handles.PositionHandle(checkpoint.transform.position + checkpoint.BottomLadder, Quaternion.identity);
        checkpoint.BottomLadder = checkpoint.transform.InverseTransformPoint(anchor);

        CalculatePoints(checkpoint);

        Vector3 endLHand = Handles.PositionHandle(checkpoint.transform.position + checkpoint.endHandPosL, Quaternion.identity);
        Vector3 endRHand = Handles.PositionHandle(checkpoint.transform.position + checkpoint.endHandPosR, Quaternion.identity);

        checkpoint.endHandPosL = checkpoint.transform.InverseTransformPoint(endLHand);
        checkpoint.endHandPosR = checkpoint.transform.InverseTransformPoint(endRHand);

    }

    void CalculatePoints(LadderPathCheckpoint ladder)
    {
        List<Vector3> points = Enumerable.Repeat(Vector3.zero, ladder.numberOfBars).ToList();

        for (int i = 0; i < points.Count; i++)
        {
            float height = ladder.heightOfFirstBar + ladder.rangeBetweenBars *i;

            Vector3 rightPoint = new Vector3(0 , height , 0) + ladder.BottomLadder;
            Vector3 leftPoint = rightPoint;

            points[i] = rightPoint;

            rightPoint.z = ladder.BottomLadder.z + ladder.spaceBetweenPointOfBar /2;
            leftPoint.z = ladder.BottomLadder.z - ladder.spaceBetweenPointOfBar / 2;

            Handles.DrawLine(ladder.transform.TransformPoint(rightPoint), ladder.transform.TransformPoint(leftPoint));
        }

        ladder.anchors = points;
    }
}
