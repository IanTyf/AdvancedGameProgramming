using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public static List<GameObject> birds = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameObject getClosestBird(Vector3 pos)
    {
        GameObject closest = null;
        float dist = 10000f;
        foreach (GameObject bird in birds)
        {
            if (Vector3.Distance(pos, bird.transform.position) < dist)
            {
                dist = Vector3.Distance(pos, bird.transform.position);
                closest = bird;
            }
        }
        return closest;
    }
}
