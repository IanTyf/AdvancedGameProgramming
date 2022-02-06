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
            int teamID = other.GetComponent<AIMovement>().teamID;
            Services.eventManager.Fire(new Event_GoalScored(teamID));
            Destroy(this.gameObject);
        }
    }
}
