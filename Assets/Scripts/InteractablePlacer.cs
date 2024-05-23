using Oculus.Interaction;
using System;
using System.Collections;
using UnityEngine;

public class InteractablePlacer : MonoBehaviour
{
    [SerializeField]
    PlaceHolder[] placingHolder;

    [SerializeField]
    float placementDeltaRotation , placementDeltaPosition;

    Grabbable grab;

    PlaceHolder chooseHolder;

    [SerializeField]
    Renderer rend;

    Coroutine PlaceCo;

    Vector3 firstPos;

    PointerEventType type;

    [SerializeField]
    float maxpos;

    private void Start()
    {
        grab = GetComponent<Grabbable>();
        firstPos = transform.position;
        if (grab)
            grab.WhenPointerEventRaised += OnEvent;
    }

    private void OnEvent(PointerEvent e)
    {
        type = e.Type;
        switch (e.Type)
        {
            case PointerEventType.Hover:
                break;
            case PointerEventType.Unhover:
                break;
            case PointerEventType.Select:
                PlaceCo = StartCoroutine(PlaceObect());
                break;
            case PointerEventType.Unselect:
                if(PlaceCo != null)StopCoroutine(PlaceCo);
                if (chooseHolder)
                    chooseHolder.OnObjectPlace(this);
                break;
            case PointerEventType.Move:
                break;
            case PointerEventType.Cancel:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (HaveToReplace() && type != PointerEventType.Select)
        {
            transform.position = firstPos;
        }

        if (PlaceProcessing())
            Debug.Log("placed");
        else
            return;
    }

    IEnumerator PlaceObect()
    {
        if (chooseHolder)
        {
            chooseHolder.OnObjectRemove(this);
            chooseHolder = null;
        }
        
        while (true)
        {
            if (PlaceProcessing())
                 break;
            yield return null;
        }
    }

    bool PlaceProcessing()
    {
        //Debug.Log(Quaternion.Angle(placingHolder.rot, transform.rotation));
        foreach (var p in placingHolder)
        {
            if (p.isAlreadyTaken)
                continue;

            if ((p.pos - transform.position).magnitude < placementDeltaPosition)
            {
                chooseHolder = p;
                p.OnObjectPlace(this);
                return true;
            }
        }
        return false;
    }

    public void Hide()
    {
        rend.enabled = false;
    }

    public void Show()
    {
        rend.enabled = true;
    }

    bool HaveToReplace(){
        return Vector3.Distance(transform.position, firstPos) > maxpos;
    }
}
