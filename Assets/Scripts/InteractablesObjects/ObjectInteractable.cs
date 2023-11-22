using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IMovable
{
    void ProcessMoving(Vector3 position);

    void Select(Vector3 pos);

    void unselect();
}

public class ObjectInteractable : MonoBehaviour, IPointableElement
{
    public event Action<PointerEvent> WhenPointerEventRaised;

    bool canMove;

    [SerializeField , Interface(typeof(IMovable))]
    List<UnityEngine.Object> movables;

    public void ProcessPointerEvent(PointerEvent evt)
    {
        Debug.Log("interact");
        switch (evt.Type)
        {
            case PointerEventType.Hover:
                break;
            case PointerEventType.Unhover:
                break;
            case PointerEventType.Select:
                canMove = true;
                break;
            case PointerEventType.Unselect:
                canMove = false;
                OnSelectExit();
                break;
            case PointerEventType.Move:
                if (canMove)
                {
                    ProcessMovement(evt.Pose.position); 
                    break;
                }
                break;
            case PointerEventType.Cancel:
                break;
        }

        WhenPointerEventRaised?.Invoke(evt);
    }

    void OnSelectEnter(Vector3 enterPos)
    {
        foreach (IMovable movable in movables)
        {
            movable.Select(enterPos);
        }
    }

    void OnSelectExit()
    {
        foreach (IMovable movable in movables)
        {
            movable.unselect();
        }
    }

    void ProcessMovement(Vector3 target)
    {
        foreach (IMovable item in movables)
        {
            item.ProcessMoving(target);
        }
    }
}
