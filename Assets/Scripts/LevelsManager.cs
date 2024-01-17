using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SwitchType
{
    next,
    previous,
    free
}

public class LevelsManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    LevelMain[] levelsPoints;

    int currentLevel = 0;

    Transform currentPlayerPos;

    UnityEvent<LevelMain> EOnLevelFinished;

    private void Awake()
    {
        currentPlayerPos = Instantiate(playerPrefab , levelsPoints[currentLevel].playerPos.position , playerPrefab.transform.rotation).GetComponent<Transform>();
    }

    public void OnNewLevel(LevelMain level)
    {
        currentPlayerPos = level.playerPos;
    }

    public void StartLevel(LevelMain level)
    {
        level.StartLevel();
    }

    public void OnLevelFinished(LevelMain level)
    {
        EOnLevelFinished.Invoke(level);
    }

    public void SwitchLevel(SwitchType type , int index = 0)
    {
        switch (type)
        {
            case SwitchType.next:
                currentLevel++;
                break;
            case SwitchType.previous:
                currentLevel--;
                break;
            case SwitchType.free:
                currentLevel = index;
                break;
            default:
                break;
        }

        OnNewLevel(levelsPoints[currentLevel]);
    }
}
