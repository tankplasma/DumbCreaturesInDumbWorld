using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonValidator : MonoBehaviour
{
    [SerializeField]
    List<SimpleButton> buttons = new List<SimpleButton>();

    Dictionary<int , bool> buttonsActivation = new Dictionary<int , bool>();

    [SerializeField]
    bool haveOrder;

    public UnityEvent AllButtonActivated;

    private void Awake()
    {
        int newID = 0;

        foreach (var button in buttons)
        {
            button.ID = 0;
            button.ButtonPressed.AddListener(ButtonPressed);
            newID++;
            buttonsActivation.Add(newID, false);
        }
    }

    void ButtonPressed(int id)
    {
        buttonsActivation[id] = true;
        VerifyIfAllPressed();
    }

    void VerifyIfAllPressed()
    {
        foreach (var button in buttonsActivation) 
        {
            if (button.Value != true)
                return;
        }

        ActivateCondition();
    }

    void ActivateCondition()
    {
        AllButtonActivated.Invoke();
    }
}
