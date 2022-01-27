using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILifecycle
{
    public List<GameObject> AIs = new List<GameObject>();

    public void createAI(GameObject ai, Vector3 pos)
    {
        ai.transform.position = pos;
        AIs.Add(ai);
    }

    public void updateAI()
    {
        foreach (GameObject ai in AIs)
        {

        }
    }
}
