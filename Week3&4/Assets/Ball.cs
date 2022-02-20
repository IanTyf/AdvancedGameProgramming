using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float timeTouching;
    public float timeInAir;
    public bool isTouching;
    public bool hop;

    private void Start()
    {
        isTouching = false;
        timeTouching = 0;
        timeInAir = 0;
        hop = false;
    }

    private void Update()
    {
        if (isTouching)
        {
            timeTouching += Time.deltaTime;
        }
        else
        {
            timeInAir += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isTouching = true;
        timeInAir = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        isTouching = false;
        timeTouching = 0;
        hop = true;
    }
}
