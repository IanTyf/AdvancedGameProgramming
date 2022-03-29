using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Services.cubeManager.deleteCube(this.gameObject);
            int teamID = -1;
            if (other.TryGetComponent(out AIMovement aiMovement))
            {
                teamID = other.GetComponent<AIMovement>().teamID;
            }
            else
            {
                teamID = other.GetComponent<PlayerMovement>().teamID;
            }
            Services.eventManager.Fire(new Event_GoalScored(teamID));
        }
    }
}
