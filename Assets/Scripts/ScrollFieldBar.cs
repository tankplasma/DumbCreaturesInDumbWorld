using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct ButtonContent
{
    public string text;
    public Sprite image;
    public int id;
}

public class ScrollFieldBar : MonoBehaviour
{

    public UnityEvent<int> onElementClicked;

    [SerializeField]
    Transform parentContent;

    [SerializeField]
    ButtonUI buttonUIPrefab;

    List<ButtonUI> buttonsUI = new List<ButtonUI>();

    public void ClearField()
    {
        // clear field
        /*        for (int i = 0; i < parentContent.childCount; i++)
                {
                    Destroy(parentContent.GetChild(i));
                }*/
        foreach (var item in buttonsUI)
        {
            Destroy(item.gameObject);
        }
    }

    public void FillContent(List<ButtonContent> bc)
    {
        ClearField();

        buttonsUI.Clear();

        // fill it
        foreach (var c in bc)
        {
            ButtonUI buttonUI = Instantiate(buttonUIPrefab, parentContent);
            buttonUI.id = c.id;
            buttonUI.SetText(c.text);
            buttonUI.SetImage(c.image);
            buttonUI.onClicked.AddListener(OnElementClicked);
            buttonsUI.Add(buttonUI);
        }
    }

    public void OnElementClicked(int id)
    {
        onElementClicked.Invoke(id);
    }

}
