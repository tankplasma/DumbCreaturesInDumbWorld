using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public struct LevelReference
{
    public AssetReference asset;

    public int id;
}

[CreateAssetMenu(menuName = "Scriptables/LevelManagement")]
public class ScriptableLevelsManagement : ScriptableObject
{
    public List<LevelReference> levels;
}
