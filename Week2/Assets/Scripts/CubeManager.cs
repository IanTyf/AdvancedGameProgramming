using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager
{
    public List<GameObject> cubes = new List<GameObject>();

    // creation
    public GameObject createCube(GameObject cubePref, Vector3 pos)
    {
        GameObject newCube = Object.Instantiate(cubePref);
        newCube.transform.position = pos;
        cubes.Add(newCube);
        return newCube;
    }

    // destruction
    public void deleteCube(GameObject cube)
    {
        cubes.Remove(cube);
        Object.Destroy(cube);
    }
    public void deleteAllCubes()
    {
        foreach (GameObject cube in cubes)
        {
            Object.Destroy(cube);
        }
        cubes.Clear();
    }
}
