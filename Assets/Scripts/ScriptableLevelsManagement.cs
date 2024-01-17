using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public struct LevelReference
{
    public SceneAsset asset;

    public int id;
}

[CreateAssetMenu(menuName = "Scriptables/LevelManagement")]
public class ScriptableLevelsManagement : ScriptableObject
{
    public List<LevelReference> levels;

    public SceneAsset GetSceneByID(int id)
    {
        foreach (LevelReference level in levels) {
            if (level.id == id) 
                return level.asset; 
        }

        return null;
    }
}
