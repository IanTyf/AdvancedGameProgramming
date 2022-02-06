using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int teamID;

    [SerializeField]
    private float moveSpeed;
    private Rigidbody rb;

    private void Awake()
    {
        Services.player = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xMovement, 0f, zMovement) * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + movement);
    }
}
