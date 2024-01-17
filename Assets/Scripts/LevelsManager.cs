using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    LevelMain[] levelsPoints;

    int currentLevel = 0;

    Transform currentPlayerPos;

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

    public void OnLevelFinished()
    {

    }

    public void SwitchLevel()
    {

    }
}
