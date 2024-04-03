using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceHolder : MonoBehaviour
{

    [SerializeField]
    UnityEvent OnPlacedComplete;

    public Vector3 pos { get => transform.position;}
    public Quaternion rot { get => transform.rotation; }

    [SerializeField]
    Renderer rend;

    [SerializeField]
    Material completeMaterial , placeHolderMaterial;

    public bool isAlreadyTaken { get; private set; }

    private void Start()
    {
        isAlreadyTaken = false;
        rend.material = placeHolderMaterial;
    }

    public void OnObjectPlace(InteractablePlacer placer)
    {
        rend.material = completeMaterial;
        isAlreadyTaken = true;
        OnPlacedComplete.Invoke();
        placer.Hide();
    }
}
