using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PokeInteractable))]
public class SimpleButton : MonoBehaviour
{
    PokeInteractable interactable;

    public UnityEvent<int> ButtonPressed;

    bool isActivate;

    [HideInInspector]
    public int ID;

    private void Awake()
    {
        interactable = GetComponent<PokeInteractable>();
        isActivate = false;
        interactable.WhenStateChanged += OnButtonPressed;
    }

    private void OnButtonPressed(InteractableStateChangeArgs obj)
    {
        switch (obj.NewState)
        {
            case InteractableState.Normal:
                break;
            case InteractableState.Hover:
                break;
            case InteractableState.Select:
                isActivate = !isActivate;
                ButtonPressed.Invoke(ID);
                break;
            case InteractableState.Disabled:
                break;
        }
    }
}
