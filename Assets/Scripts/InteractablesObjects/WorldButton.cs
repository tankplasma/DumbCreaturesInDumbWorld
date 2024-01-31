using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{
    public int id;

    public UnityEvent<int> onClicked;

    public void ActiveButton()
    {
        onClicked.Invoke(id);
    }
}
