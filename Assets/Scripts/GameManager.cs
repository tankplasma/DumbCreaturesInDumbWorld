using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.VersionControl;

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

    public void LoadSceneByID(int id)
    {
        SceneAsset asset = levelManagement.GetSceneByID(id);
        AsyncOperation op = SceneManager.LoadSceneAsync(asset.name, LoadSceneMode.Single);
        op.allowSceneActivation = true;
        op.completed += (op) =>
        {
            Debug.Log("scene completed");
        };
    }
}
