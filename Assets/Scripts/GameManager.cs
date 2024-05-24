using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor;
using Unity.VisualScripting;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public UnityEvent onLevelChange; // Event appelé sur les changements de niveaux

    [SerializeField]
    List<ScriptableWorlds> levelManagement;

/*    [SerializeField]
    UnityEditor.SceneAsset lobbyScene;*/

    KeyValuePair<ScriptableWorlds, int> currentLevel;

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
        
        DontDestroyOnLoad(this);
    }

    public void ReturnToLobby()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        op.allowSceneActivation = true;
        op.completed += (op) =>
        {
            Debug.Log("scene completed");
        };
    }

    public List<ScriptableWorlds> GetAllWorlds()
    {
        return levelManagement;
    }

    public ScriptableWorlds GetWorldByID(int id)
    {
        return levelManagement.Where(x => x.id == id).FirstOrDefault();
    }

    public void LoadSceneByID(int id , ScriptableWorlds lvl)
    {
        // get lvl and id to load to be able to go to next level
        currentLevel = new KeyValuePair<ScriptableWorlds, int>(lvl, id);

        AsyncOperation op = SceneManager.LoadSceneAsync(lvl.GetSceneByID(id), LoadSceneMode.Single);
        op.allowSceneActivation = true;
        op.completed += (op) =>
        {
            Debug.Log("scene completed");
        };
    }

    public void GoNextLevel()
    {
        if (currentLevel.Key.levels.Count > currentLevel.Value+1) // get if nextLevel is reachable
        {
            LoadSceneByID(currentLevel.Value+1, currentLevel.Key);
        }
    }

    public void ReloadLevel()
    {
        LoadSceneByID(currentLevel.Value, currentLevel.Key);
    }

}
