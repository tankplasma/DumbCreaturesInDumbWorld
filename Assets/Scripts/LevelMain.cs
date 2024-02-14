using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMain : MonoBehaviour
{
    static LevelMain instance;

    public Transform playerPos;

    [SerializeField]
    ScriptableGameState gameState;

    [SerializeField]
    GameObject menu;

    bool alreadyStart = false;

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
        menu.SetActive(false);
    }

    public void OnLevelEnd()
    {
        if (PathManager.instance.haveFinishedSpawn)
        {
            Debug.Log("end");
            menu.SetActive(true);
            //GameManager.Instance.ReturnToLobby();
        }
    }

    public void CallReturnToMenu()
    {
        GameManager.Instance.ReturnToLobby();
    }

    [ContextMenu("start")]
    public void StartLevel()
    {
        if (alreadyStart)
            return;
        alreadyStart = true;
        PathManager.instance.StartSpawn();
    }
}
