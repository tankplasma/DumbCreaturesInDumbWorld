using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PokeInteractable))]
public class SwitchButton : MonoBehaviour
{
    PokeInteractable interactable;

    [SerializeField]
    float switchAmountThreashold;

    [SerializeField]
    Transform visual;

    float originalThreashod;

    public UnityEvent<bool> OnSwitchUpdate;

    private void Start()
    {
        interactable = GetComponent<PokeInteractable>();

        originalThreashod = interactable.EnterHoverNormal;
        
        interactable.WhenStateChanged += OnButtonStateChange;
    }

    private void OnButtonStateChange(InteractableStateChangeArgs obj)
    {
        Debug.Log(obj.NewState);

        switch (obj.NewState)
        {
            case InteractableState.Normal:
                break;
            case InteractableState.Hover:
                break;
            case InteractableState.Select:
                if (obj.PreviousState != InteractableState.Select)
                {
                    ProcessSwitch();
                }
                break;
            case InteractableState.Disabled:
                break;
            default:
                break;
        }
    }

    void ProcessSwitch()
    {
        // switch on
        if (interactable.EnterHoverNormal == originalThreashod)
        {
            interactable.EnterHoverNormal = switchAmountThreashold;
            visual.position = new Vector3(visual.transform.position.x, visual.transform.position.y - switchAmountThreashold, visual.transform.position.z);
            OnSwitchUpdate.Invoke(true);
        }
        else //switch off
        {
            interactable.EnterHoverNormal = originalThreashod;
            visual.position = new Vector3(visual.transform.position.x, visual.transform.position.y + switchAmountThreashold, visual.transform.position.z);
            OnSwitchUpdate.Invoke(false);
        }
    }
}
