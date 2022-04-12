using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterDB : ScriptableObject
{
    [Header("***Character List***")]
    [SerializeField]
    public Character[] characters;
}
