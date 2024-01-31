using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.VersionControl;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public UnityEvent onLevelChange; // Event appelé sur les changements de niveaux

    [SerializeField]
    List<ScriptableWorlds> levelManagement;

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
        
        DontDestroyOnLoad(this);
    }

    public List<ScriptableWorlds> GetAllWorlds()
    {
        return levelManagement;
    }

    public void LoadSceneByID(int id , ScriptableWorlds lvl)
    {
        SceneAsset asset = lvl.GetSceneByID(id);
        AsyncOperation op = SceneManager.LoadSceneAsync(asset.name, LoadSceneMode.Single);
        op.allowSceneActivation = true;
        op.completed += (op) =>
        {
            Debug.Log("scene completed");
        };
    }
}
