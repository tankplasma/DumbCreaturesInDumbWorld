using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchableObject : MonoBehaviour
{
    [SerializeField]
    GrabInteractable interactable;

    [SerializeField]
    float pinchForce;

    private void Start()
    {
        interactable.WhenPointerEventRaised += OnHandImpact;
    }

    private void OnHandImpact(PointerEvent obj)
    {
        if (obj.Type == PointerEventType.Hover)
        {

        }
    }

    void ApplyPinch()
    {

    }
}
