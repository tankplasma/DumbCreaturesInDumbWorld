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
    public static LevelsManager instance;

    [SerializeField]ScriptableGameState gameState;

    private void Awake()
    {
        if(instance)
            Destroy(instance);
        else
            instance = this;
    }

    [SerializeField]
    float time;

    UnityEvent EOnLevelFinished , EOnLevelStarted;

    public void StartLevel()
    {
        EOnLevelStarted.Invoke();
        PathManager.instance.StartSpawn();
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        float currentTime = 0;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
