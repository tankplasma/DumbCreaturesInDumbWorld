using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMain : MonoBehaviour
{
    static LevelMain instance;

    public Transform playerPos;

    [SerializeField]
    ScriptableGameState gameState;

    [SerializeField]
    public int NumberOfDumbs;

    private void Awake()
    {
        if (instance)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        gameState.ELevelFinished.AddListener(OnLevelEnd);    
    }

    public void OnLevelEnd()
    {
        if (PathManager.instance.haveFinishedSpawn)
        {
            Debug.Log("end");
            GameManager.Instance.ReturnToLobby();
        }
    }

    public void StartLevel()
    {

    }
}
