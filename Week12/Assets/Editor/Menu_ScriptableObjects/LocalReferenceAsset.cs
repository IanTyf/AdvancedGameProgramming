using UnityEngine;
using UnityEditor;

public class LocalReferenceAsset
{
    [MenuItem("Assets/Create/ScriptableObjects/LocalReference")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<LocalReference>();
    }
}