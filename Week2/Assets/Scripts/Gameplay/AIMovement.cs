using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;
    private GameObject target;
    private Rigidbody rb;

    public int teamID;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // should be called in fixed updates since it involves physics
    public void MoveToTarget()
    {
        // if it has a current target, move to it
        if (target != null)
        {
            Vector3 movement = (target.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        // if there is no current target, look for one
        else
        {
            FindNewTarget();
        }
    }

    // look at all the cubes and set target to the closest one
    public void FindNewTarget()
    {
        float currentDist = 1000f;
        foreach (GameObject cube in Services.cubeManager.cubes)
        {
            float newDist = Vector3.Distance(transform.position, cube.transform.position);
            if (newDist < currentDist)
            {
                currentDist = newDist;
                target = cube;
            }
        }
    }

    // compare current target to a new target and see if it's a better option
    public void CheckOutNewTarget(GameObject newTarget)
    {
        if (target == null) target = newTarget;
        else
        {
            float newDist = Vector3.Distance(transform.position, newTarget.transform.position);
            float currentDist = Vector3.Distance(transform.position, target.transform.position);
            if (newDist < currentDist) target = newTarget;
        }
    }
}
