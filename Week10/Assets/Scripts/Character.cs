using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public float speed;
    public float size;
    public int predLevel;
    public Material material;

    public Character(string name, float speed, float size, int predLevel, Material mat)
    {
        this.name = name;
        this.speed = speed;
        this.size = size;
        this.predLevel = predLevel;
        this.material = mat;
    }
}
