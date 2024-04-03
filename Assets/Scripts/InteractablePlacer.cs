using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractablePlacer : MonoBehaviour
{
    [SerializeField]
    PlaceHolder[] placingHolder;

    [SerializeField]
    float placementDeltaRotation , placementDeltaPosition;

    private void Update()
    {
        if (IsCloseToTheRightPlace())
            Debug.Log("placed");
        else
            return;
    }

    bool IsCloseToTheRightPlace()
    {
        //Debug.Log(Quaternion.Angle(placingHolder.rot, transform.rotation));
        foreach (var p in placingHolder)
        {
            if(p.isAlreadyTaken)
                continue;

            if (/*Quaternion.Angle(p.rot, transform.rotation) < placementDeltaRotation && */(p.pos - transform.position).magnitude < placementDeltaPosition)
                p.OnObjectPlace(this);
                return true;
        }
        return false;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
