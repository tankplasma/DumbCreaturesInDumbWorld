using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public struct LevelReference
{
    public int id;
    public string asset;
    public Sprite look;
    public string name;
}

[CreateAssetMenu(menuName = "Scriptables/Worlds")]
public class ScriptableWorlds : ScriptableObject
{
    public int id;

    public GameObject TinyLevelVisual;

    public List<LevelReference> levels;

    public string GetSceneByID(int id)
    {
        foreach (LevelReference level in levels) {
            if (level.id == id) 
                return level.asset; 
        }

        return null;
    }
}
