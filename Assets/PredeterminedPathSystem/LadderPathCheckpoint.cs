using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPathCheckpoint : PathCheckpoint
{
    public List<Vector3> anchors = new List<Vector3>();

    public Vector3 GetCloserPointOfHeight(float height)
    {
        Vector3 closestPoint = Vector3.zero;

        foreach (var anchor in anchors)
        {
            Vector3 worldAnchor = transform.TransformPoint(anchor);

            float worldanchorYPosDiff = Mathf.Abs(worldAnchor.y - height);
            float actualClosestpointYDiff = Mathf.Abs(closestPoint.y - height);

            if (worldanchorYPosDiff < actualClosestpointYDiff)
                closestPoint = worldAnchor;
        }

        return closestPoint;

    }
}
