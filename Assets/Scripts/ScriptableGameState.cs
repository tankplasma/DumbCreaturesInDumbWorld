using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PNJStatus
{
    alive,
    dead
}

[CreateAssetMenu(menuName = "Scriptables/GameState")]
public class ScriptableGameState : ScriptableObject
{
    public Action<PNJMain> OnPNJDead , OnPNJAdd;
    
    Dictionary<PNJMain, PNJStatus> PNJsState = new Dictionary<PNJMain, PNJStatus>();

    List<PNJMain> PNJFinished = new List<PNJMain>();

    UnityEvent ENewPNJFinished;

    public void AddPNJ(PNJMain pnj)
    {
        PNJsState.Add(pnj , PNJStatus.alive);
    }

    public void RemovePNJ(PNJMain pnj)
    {
        if (!IsPNJExistInDictionary(pnj))
            return;

        PNJsState.Remove(pnj);
    }

    public void ChangePNJState(PNJMain pnj , PNJStatus status)
    {
        if (!IsPNJExistInDictionary(pnj))
            return;

        PNJsState[pnj] = status; 
    }

    bool IsPNJExistInDictionary(PNJMain pnj)
    {
        if(PNJsState.ContainsKey(pnj))
            return true;
        else
            return false;
    }

    public Dictionary<PNJMain, PNJStatus> GetPNJS()
    {
        return PNJsState;
    }

    public void PNJHaveFinished(PNJMain PNJ)
    {
        PNJFinished.Add(PNJ);
        ENewPNJFinished.Invoke();
    }
}
