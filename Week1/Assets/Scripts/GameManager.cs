using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject aiPrefab;
    public GameObject cubePrefab;

    List<GameObject> cubes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Services.Init();

        createAIsAtRandomPos(2);
        createCollectableCubes(6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createAIsAtRandomPos(int numOfAI)
    {
        for (int i = 0; i < numOfAI; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-13f, 13f), 0.5f, Random.Range(-8f, 8f));
            GameObject newAI = Instantiate(aiPrefab, transform);
            Services.aiManager.createAI(newAI, randPos);
        }
    }

    private void createCollectableCubes(int numOfCubes)
    {
        for (int i = 0; i < numOfCubes; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-13f, 13f), 0.5f, Random.Range(-8f, 8f));
            GameObject newCube = Instantiate(cubePrefab, transform);
            Services.cubeManager.createCube(newCube, randPos);
        }
    }
}
