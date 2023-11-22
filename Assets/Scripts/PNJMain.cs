using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class PNJMain : MonoBehaviour
{
    [SerializeField]
    ScriptableGameState gameState;

    public void IAmDead()
    {
        gameState.ChangePNJState(this, PNJStatus.dead);
        gameObject.SetActive(false);
    }
}
