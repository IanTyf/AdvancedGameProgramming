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
            Destroy(this.gameObject);
        }
    }
}
