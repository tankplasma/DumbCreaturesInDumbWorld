using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Digicode : MonoBehaviour
{
    [Range(2,14)]
    public int digicodeSize = 3;

    [SerializeField]
    List<SimpleButton> buttons;

    List<int> buttonsOrder = new List<int>();
    List<int> currentButtonsOrder = new List<int>();

    public UnityEvent ResetSelection , CodeSuccessfull , SuccefullTouch;

    private void Awake()
    {
        InitButtons();
    }

    void InitButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].ID = i;
            buttons[i].ButtonPressed.AddListener(OnButtonClicked);
        }
    }

    public void SetNewOrder(List<int> newOrder)
    {
        buttonsOrder = newOrder;
    }

    void OnButtonClicked(int id)
    {
        UpdateButtonsClicked(id);
    }

    void UpdateButtonsClicked(int id)
    {
        currentButtonsOrder.Add(id);
        if(isOrderCorrect())
        {
            SuccefullTouch.Invoke();
            if (IsOrderFinished())
            {
                CodeSuccessfull.Invoke();
            }
        }
        else
        {
            ResetOrder();
        }

    }

    void ResetOrder()
    {
        currentButtonsOrder.Clear();
        ResetSelection.Invoke();
    }

    bool isOrderCorrect()
    {
        for(int i = 0;i < currentButtonsOrder.Count;i++)
        {
            if (currentButtonsOrder[i] != buttonsOrder[i])
            {
                return false;
            }
        }

        return true;
    }

    bool IsOrderFinished()
    {
        return currentButtonsOrder.Count == buttonsOrder.Count? true: false;
    }
}
