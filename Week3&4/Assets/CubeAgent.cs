using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CubeAgent : Agent
{
    public GameObject ball;
    private Rigidbody ballRb;
    private Ball ballScript;
    public Vector3 startingPos;
    private Transform cube;
    private Rigidbody cubeRb;

    public override void Initialize()
    {
        ballRb = ball.GetComponent<Rigidbody>();
        cube = transform.GetChild(0);
        cubeRb = cube.GetComponent<Rigidbody>();
        ballScript = ball.GetComponent<Ball>();
        startingPos = cube.position;
        //SetResetParameters();
    }

    // each game cycle it executes once
    public override void OnEpisodeBegin()
    {
        // reset cube rotation w/ some random rotation
        cube.position = startingPos;
        cubeRb.velocity = Vector3.zero;
        cubeRb.angularVelocity = Vector3.zero;
        cube.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        gameObject.transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));
        

        // reset ball velocity and position
        ballRb.velocity = Vector3.zero;
        ball.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 4f, Random.Range(-1.5f, 1.5f)) + transform.position;
        SetResetParameters();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(cube.rotation.z);
        sensor.AddObservation(cube.rotation.x);
        sensor.AddObservation(ball.transform.position - cube.position);
        sensor.AddObservation(ballRb.velocity);
        sensor.AddObservation(cubeRb.velocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionZ = 2f * Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        var actionX = 2f * Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        var vertMovement = 8000f * Mathf.Clamp(actions.ContinuousActions[2], -1f, 1f);

        if ((cube.rotation.z < 0.25f && actionZ > 0f) || (cube.rotation.z > -0.25f && actionZ < 0f))
        {
            cube.Rotate(new Vector3(0, 0, 1), actionZ);
        }
        if ((cube.rotation.x < 0.25f && actionX > 0f) || (cube.rotation.x > -0.25f && actionX < 0f))
        {
            cube.Rotate(new Vector3(1, 0, 0), actionX);
        }
        if ((cube.position.y < startingPos.y + 0.5 && vertMovement > 0) || (cube.position.y > startingPos.y - 0.5 && vertMovement < 0))
        {
            cubeRb.AddForce(Vector3.up * vertMovement);
        }
        if ((cube.position.y >= startingPos.y + 0.5) || (cube.position.y <= startingPos.y - 0.5))
        {
            cubeRb.velocity = Vector3.zero;
        }

        if (ball.transform.position.y - cube.position.y < -3f 
            || Mathf.Abs(ball.transform.position.x - cube.position.x) > 5f
            || Mathf.Abs(ball.transform.position.z - cube.position.z) > 5f)
        {
            SetReward(-1f);
            EndEpisode();
        }
        else if (ballScript.hop)
        {
            SetReward(2f);
            ballScript.hop = false;
        }
        else if (ballScript.isTouching)
        {
            //SetReward(ballScript.timeTouching * 0.1f);
            SetReward(0.1f);
        }
        else if (!ballScript.isTouching)
        {
            SetReward(ballScript.timeInAir * ballScript.timeInAir * 0.1f); // use this one to train high bounces, starting from scratch
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = -Input.GetAxis("Horizontal");
        //continuousActionsOut[1] = -Input.GetAxis("Vertical");
        continuousActionsOut[2] = Input.GetAxis("Vertical");
    }

    private void SetResetParameters()
    {
        SetBall();
    }

    private void SetBall()
    {
        int scale = 1;
        ballRb.mass = scale;
        ball.transform.localScale = new Vector3(scale, scale, scale);

    }
}
