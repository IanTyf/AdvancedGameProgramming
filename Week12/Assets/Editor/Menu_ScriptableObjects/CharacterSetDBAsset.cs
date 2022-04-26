using UnityEngine;
using UnityEditor;

public class CharacterSetDBAsset
{
    [MenuItem("Assets/Create/ScriptableObjects/CharacterSetDB")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<CharacterSetDB>();
    }
}