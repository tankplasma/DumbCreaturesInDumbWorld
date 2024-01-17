using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    ScriptableLevelsManagement levelManagement;

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    public static void CountPoints(ScriptableGameState state)
    {
           
    }
}
