using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractablePlacer : MonoBehaviour
{
    [SerializeField]
    PlaceHolder placingHolder;

    [SerializeField]
    float placementDeltaRotation , placementDeltaPosition;

    private void Update()
    {
        if (IsCloseToTheRightPlace())
            placingHolder.OnObjectPlace(this);
        else
            return;
    }

    bool IsCloseToTheRightPlace()
    {
        return Quaternion.Angle(placingHolder.rot, transform.rotation) < placementDeltaRotation && (placingHolder.pos - transform.position).magnitude < placementDeltaPosition;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
