using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool moving;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hMovement = Input.GetAxis("Horizontal");
        transform.position = transform.position + Vector3.right * hMovement * speed * Time.deltaTime;

        if (hMovement == 0) moving = false;
        else moving = true;
    }

    private void FixedUpdate()
    {
        //float hMovement = Input.GetAxis("Horizontal");
        //rb.AddForce(Vector3.right * hMovement * speed);
    }
}
