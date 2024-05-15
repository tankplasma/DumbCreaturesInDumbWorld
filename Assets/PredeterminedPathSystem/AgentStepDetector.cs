using DG.Tweening;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AgentStepDetector : MonoBehaviour
{
    [SerializeField]
    float maxStepHeight;

#if UNITY_EDITOR
    [SerializeField]
    bool showStepDetection;
#endif

    [SerializeField]
    [Range(0f, 1f)]
    float rangeDetection = 0.1f;

    [SerializeField]
    [Range(0.00001f, 1f)]
    float spaceBetweenDetections = 0.01f;

    private void Update()
    {
        if (IsStepDetected(out RaycastHit hit))
        {
            transform.position = hit.point;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (showStepDetection)
        {
            Gizmos.color = Color.magenta;

            for (float i = 0; i < maxStepHeight ; i += spaceBetweenDetections)
            { 
                Gizmos.DrawRay(transform.position + Vector3.up * i, transform.forward * rangeDetection);
            }

            if (IsStepDetected(out RaycastHit hit))
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(hit.point, 0.01f);
            }
        }
    }
#endif
    bool IsStepDetected(out RaycastHit hit)
    {
        hit = new RaycastHit();

        // first , look if there is something toward

        for (float i = 0; i < maxStepHeight; i += spaceBetweenDetections)
        {
            Ray ray = new Ray(transform.position + Vector3.up * i, transform.forward);

            if (Physics.Raycast(ray , out RaycastHit h,rangeDetection))
            {
                //Gizmos.DrawWireSphere(h.point,0.01f);
                if (IsTheTopNotEat()) // if top not hit , then it's a step
                {
                    hit = GetTopOfStep(h.point /*+ transform.forward * 0.001f*/);
                    return true;
                }
            }
        }
        // second look if there is nomething 

        return false;
    }

    bool IsTheTopNotEat()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * maxStepHeight) + new Vector3(0, 0.00001f, 0), transform.forward);

        //Gizmos.color= Color.yellow;
        //Gizmos.DrawRay(ray);

        if (Physics.Raycast(ray , rangeDetection))
        {
            return false;
        }

        return true;
    }

    RaycastHit GetTopOfStep(Vector3 hittedPos)
    {
        Vector3 vector3 = new Vector3(hittedPos.x, transform.position.y, hittedPos.z) + (Vector3.up * maxStepHeight) + new Vector3(0, 0.1f, 0);

        Ray ray = new Ray(vector3, Vector3.down);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(hittedPos, 0.02f);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            //Gizmos.DrawWireSphere(hit.point, 0.01f);
            return hit;
        }

        return new RaycastHit();
    }
}
