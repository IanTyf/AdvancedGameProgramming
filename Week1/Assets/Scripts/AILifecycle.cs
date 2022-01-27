using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILifecycle
{
    private List<GameObject> AIs = new List<GameObject>();

    // creation
    public void createAI(GameObject ai, Vector3 pos)
    {
        ai.transform.position = pos;
        AIs.Add(ai);
    }

    // updating
    public void moveAI()
    {
        foreach (GameObject ai in AIs)
        {
            ai.GetComponent<AIMovement>().MoveToTarget();
        }
    }
    public void CheckOutNewTargets(List<GameObject> newTargets)
    {
        foreach (GameObject ai in AIs)
        {
            foreach (GameObject newTarget in newTargets)
            {
                ai.GetComponent<AIMovement>().CheckOutNewTarget(newTarget);
            }
        }
    }

    // destruction
    public void deleteAI(GameObject ai)
    {
        AIs.Remove(ai);
    }

    // tracking
    public List<GameObject> getCurrentAIs()
    {
        return AIs;
    }
}
