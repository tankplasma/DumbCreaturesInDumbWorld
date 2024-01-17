using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public UnityEvent onLevelChange; // Event appelé sur les changements de niveaux

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
