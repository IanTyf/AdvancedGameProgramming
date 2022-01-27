using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager
{
    public List<GameObject> cubes = new List<GameObject>();

    public void createCube(GameObject cube, Vector3 pos)
    {
        cube.transform.position = pos;
        cubes.Add(cube);
    }
}
