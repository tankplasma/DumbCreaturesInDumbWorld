using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractablePlacer : MonoBehaviour
{
    Transform placingHolder;

    float placementDelta;

    bool IsCloseToTheRightPlace()
    {
        return Quaternion.Angle(placingHolder.rotation, transform.rotation) < placementDelta;
    }
}
