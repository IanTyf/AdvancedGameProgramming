using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILifecycle
{
    private List<GameObject> AIs = new List<GameObject>();

    // creation
    public GameObject createAI(GameObject aiPref, Vector3 pos, int teamID, Material mat)
    {
        GameObject newAI = Object.Instantiate(aiPref);
        newAI.transform.position = pos;
        newAI.GetComponent<AIMovement>().teamID = teamID;
        newAI.GetComponent<MeshRenderer>().material = mat;
        AIs.Add(newAI);
        return newAI;
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
        Object.Destroy(ai);
    }
    public void deleteAllAIs()
    {
        foreach (GameObject ai in AIs)
        {
            Object.Destroy(ai);
        }
        AIs.Clear();
    }

    // tracking
    public List<GameObject> getCurrentAIs()
    {
        return AIs;
    }
}
