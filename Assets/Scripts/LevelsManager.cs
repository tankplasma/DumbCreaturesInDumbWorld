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

    private void Awake()
    {
        Instantiate(playerPrefab , levelsPoints[currentLevel].playerPos.position , playerPrefab.transform.rotation);
    }

    public void StartLevel()
    {
        
    }

    public void OnLevelFinished()
    {

    }

    public void SwitchLevel()
    {

    }
}
