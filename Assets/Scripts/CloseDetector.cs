using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDetector : MonoBehaviour
{
    [SerializeField]
    Transform targetTransform;



    [SerializeField]
    float closeDeltaAngle , closeDeltaPosition;

    private void Start()
    {
        StartCoroutine(CloseDetections());
    }

    IEnumerator CloseDetections()
    {
        while (true)
        {
            if (IsClose())
            {
                transform.position = targetTransform.position;
                transform.rotation = targetTransform.rotation;
                targetTransform.gameObject.SetActive(false);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    bool IsClose()
    {
        return (Quaternion.Angle(targetTransform.rotation , transform.rotation) < closeDeltaAngle 
            && (transform.position - targetTransform.position).magnitude < closeDeltaPosition);
    }
}
