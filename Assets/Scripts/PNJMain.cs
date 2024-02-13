using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class PNJMain : MonoBehaviour
{
    [SerializeField]
    ScriptableGameState gameState;

    private void Start()
    {
        InitPNJ();
    }

    void InitPNJ()
    {
        gameState.AddPNJ(this);
    }

    public void OnCollectableCollected(Collectable collectable)
    {
        collectable.OnCollected();
    }

    public void IAmDead()
    {
        gameState.ChangePNJState(this, PNJStatus.dead);
        gameObject.SetActive(false);
    }

    public void IFinished()
    {
        gameState.PNJHaveFinished(this);
        gameObject.SetActive(false);
    }
}
