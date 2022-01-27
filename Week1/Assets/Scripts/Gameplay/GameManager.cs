using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject aiPrefab;
    public GameObject cubePrefab;

    [SerializeField]
    private float cooldown = 2f;
    private float timer;

    void Start()
    {
        Services.Init();

        createAIsAtRandomPos(2);
        createCollectableCubes(6);
    }

    private void Update()
    {
        // create an additional cube every once in a while
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            List<GameObject> newCubes = createCollectableCubes(1);
            Services.aiManager.CheckOutNewTargets(newCubes); // this lets the AIs compare their current target with the newly created cubes and see if any of the new ones is closer
            timer = 0;
            cooldown = Random.Range(1f, 2.5f);
        }
    }

    private void FixedUpdate()
    {
        Services.aiManager.moveAI();
    }

    // create a number of AIs at random positions on the plane and return them in a list
    private List<GameObject> createAIsAtRandomPos(int numOfAI)
    {
        List<GameObject> retAIs = new List<GameObject>();
        for (int i = 0; i < numOfAI; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-13f, 13f), 0.5f, Random.Range(-8f, 8f));
            GameObject newAI = Instantiate(aiPrefab, transform);
            Services.aiManager.createAI(newAI, randPos);
            retAIs.Add(newAI);
        }
        return retAIs;
    }

    // create a number of collectable cubes at random positions on the plane and return them in a list
    private List<GameObject> createCollectableCubes(int numOfCubes)
    {
        List<GameObject> retCubes = new List<GameObject>();
        for (int i = 0; i < numOfCubes; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-13f, 13f), 0.5f, Random.Range(-8f, 8f));
            GameObject newCube = Instantiate(cubePrefab, transform);
            Services.cubeManager.createCube(newCube, randPos);
            retCubes.Add(newCube);
        }
        return retCubes;
    }
}
