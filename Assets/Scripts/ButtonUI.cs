using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    public int id;

    public UnityEvent<int> onClicked;

    [SerializeField]
    TMP_Text text;

    [SerializeField]
    Image image;

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void SetImage(Sprite sprite)
    {
        if(image)image.sprite = sprite;
    }

    public void OnClicked()
    {
        onClicked.Invoke(id);
    }
}
