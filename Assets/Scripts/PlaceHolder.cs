using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceHolder : MonoBehaviour
{

    [SerializeField]
    UnityEvent OnPlacedComplete , OnRemove;

    public Vector3 pos { get => placeHolderTransform.position;}
    public Quaternion rot { get => placeHolderTransform.rotation; }

    [SerializeField]
    Transform placeHolderTransform;

    [SerializeField]
    Renderer rend;

    [SerializeField]
    Material completeMaterial , placeHolderMaterial;

    public bool isAlreadyTaken { get; private set; }

    private void Start()
    {
        if(!placeHolderTransform)
            placeHolderTransform = transform;
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

    public void OnObjectRemove(InteractablePlacer placer)
    {
        rend.material = placeHolderMaterial;
        isAlreadyTaken = false;
        OnRemove.Invoke();
        placer.Show();
    }
}
