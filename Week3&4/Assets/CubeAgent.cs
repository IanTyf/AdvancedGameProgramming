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
    private Vector3 startingPos;

    public override void Initialize()
    {
        ballRb = ball.GetComponent<Rigidbody>();
        ballScript = ball.GetComponent<Ball>();
        startingPos = transform.position;
        //SetResetParameters();
    }

    // each game cycle it executes once
    public override void OnEpisodeBegin()
    {
        // reset cube rotation w/ some random rotation
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
        sensor.AddObservation(transform.rotation.z);
        sensor.AddObservation(transform.rotation.x);
        sensor.AddObservation(ball.transform.position - transform.position);
        sensor.AddObservation(ballRb.velocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionZ = 4f * Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        var actionX = 4f * Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        var vertMovement = 100f * Mathf.Clamp(actions.ContinuousActions[2], -1f, 1f);

        if ((transform.rotation.z < 0.25f && actionZ > 0f) || (transform.rotation.z > -0.25f && actionZ < 0f))
        {
            transform.Rotate(new Vector3(0, 0, 1), actionZ);
        }
        if ((transform.rotation.x < 0.25f && actionX > 0f) || (transform.rotation.x > -0.25f && actionX < 0f))
        {
            transform.Rotate(new Vector3(1, 0, 0), actionX);
        }
        if ((transform.position.y < startingPos.y + 0.5 && vertMovement > 0) || (transform.position.y > startingPos.y - 0.5 && vertMovement < 0))
        {
            transform.Translate(Vector3.up * vertMovement * Time.deltaTime);
        }

        if (ball.transform.position.y - transform.position.y < -3f 
            || Mathf.Abs(ball.transform.position.x - transform.position.x) > 5f
            || Mathf.Abs(ball.transform.position.z - transform.position.z) > 5f)
        {
            SetReward(-1f);
            EndEpisode();
        }
        
        if (ballScript.hop)
        {
            SetReward(2f);
            ballScript.hop = false;
        }
        else
        {
            SetReward(ballScript.timeInAir * 0.1f);
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
