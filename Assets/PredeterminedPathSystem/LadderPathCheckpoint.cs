using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPathCheckpoint : PathCheckpoint
{
    public bool showAnchors;

    public List<Vector3> anchors = new List<Vector3>();

    public Vector3 BottomLadder , endHandPosR , endHandPosL;

    [SerializeField]
    Transform topTPPoint;

    [SerializeField]
    [Range(0f, 0.1f)]
    public float rangeBetweenBars, spaceBetweenPointOfBar, heightOfFirstBar;

    [SerializeField]
    public int numberOfBars;

    public Vector3 GetTopPoint()
    {
        return topTPPoint.position;
    }

    public Vector3 GetCloserPointOfHeight(Vector3 point)
    {
        Vector3 closestPoint = Vector3.zero;

        foreach (var anchor in anchors)
        {
            Vector3 worldLeftAnchor = transform.TransformPoint(
                new Vector3(anchor.x , anchor.y , anchor.z - spaceBetweenPointOfBar/2));

            Vector3 worldRightAnchor = transform.TransformPoint(
                new Vector3(anchor.x, anchor.y, anchor.z + spaceBetweenPointOfBar / 2));

            if (closestPoint == Vector3.zero)
            {
                closestPoint = worldLeftAnchor;
            }
            if((worldLeftAnchor - point).magnitude < (closestPoint - point).magnitude)
            {
                closestPoint = worldLeftAnchor;
            }
            if ((worldRightAnchor - point).magnitude < (closestPoint - point).magnitude)
            {
                closestPoint = worldRightAnchor;
            }
        }

        return closestPoint;

    }
}
