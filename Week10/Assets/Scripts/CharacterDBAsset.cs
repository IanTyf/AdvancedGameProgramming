using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterDBAsset : MonoBehaviour
{
    [MenuItem("Assets/Create/ScriptableObjects/CharacterDB")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<CharacterDB>();
    }
}
