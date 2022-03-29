using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

public enum GameResultType { Win, Lose, Draw };
public class MinerAgent : Agent
{
	GameGroup gameGroup;

	[HideInInspector]
	public Rigidbody rB;
	Vector3 originalPos;
	Quaternion originalRota;
	Color originalColor;
	[SerializeField]
	Color dashColor;
	[SerializeField]
	Renderer cubeRenderer;
	[SerializeField]
	float rotateSpeed = 3;
	[SerializeField]
	float speed = 50;
	[SerializeField]
	float maxSpeed = 3;
	[SerializeField]
	float maxDashSpeed = 6;

	bool dashing = false;
	float dashingTimer = 0;
	float dashingTimerTotal = .3f;

	bool dashCDReady = true;
	float dashCDTimer = 0;
	float dashCDTimerTotal = 2;

	int teamID = -1;
	int teamID_Opponent = -1;

	public override void Initialize()
	{
		rB = GetComponent<Rigidbody>();
		gameGroup = GetComponentInParent<GameGroup>();
		originalPos = transform.localPosition;
		originalRota = transform.localRotation;
		originalColor = cubeRenderer.material.GetColor("_Color");
		teamID = GetComponent<BehaviorParameters>().TeamId;

		if (teamID == 1)
		{
			teamID_Opponent = 0;
		}
		else
		{
			teamID_Opponent = 1;
		}
	}
	public override void OnEpisodeBegin()
	{
		transform.localPosition = originalPos;
		transform.localRotation = originalRota;
		cubeRenderer.material.SetColor("_Color", originalColor);
		rB.velocity = Vector3.zero;

	}
	public override void CollectObservations(VectorSensor sensor)
	{
		sensor.AddObservation(rB.velocity);

		sensor.AddObservation(dashing);
		sensor.AddObservation(dashingTimer / dashingTimerTotal);
		sensor.AddObservation(dashCDTimer / dashCDTimerTotal);
	}

	public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
	{
		actionMask.SetActionEnabled(2, 1, dashCDReady);
	}
	public override void OnActionReceived(ActionBuffers actions)
	{
		//Actions: 0=Do nothing 
		//branch 0: rotating, decision 1 = turn left, 2 = turn right
		//branch 1: moving, decision 1 = go forward, 2 = go backward
		//branch 2: decision 1 = use dash
		float rotateValue = 0;
		float forwardValue = 0;
		var discreteActions = actions.DiscreteActions;

		int rotateAction = discreteActions[0];
		int forwardAction = discreteActions[1];
		int dashAction = discreteActions[2];

		switch(rotateAction)
		{
			case 1:
				rotateValue = 1;
				break;
			case 2:
				rotateValue = -1;
				break;
		}

		switch (forwardAction)
		{
			case 1:
				forwardValue = 1;
				break;
			case 2:
				forwardValue = -1;
				break;
		}

		Vector3 force = transform.forward * forwardValue * (speed + dashAction * 2);

		if (dashAction == 1 && forwardAction == 1)
		{
			cubeRenderer.material.SetColor("_Color", dashColor);
			dashCDReady = false;
			dashing = true;
			dashingTimer = dashingTimerTotal;
			// penalty for overusing the dash ability
			AddReward(-.01f);
		}

		if (dashing)
		{
			force = force * 1.5f;
		}

		rB.AddForce(force, ForceMode.VelocityChange);
		transform.Rotate(transform.up * rotateValue, rotateSpeed);

	}

	private void FixedUpdate()
	{
		if (!dashing)
		{
			Vector2 velocityXZ = new Vector2(rB.velocity.x, rB.velocity.z);
			if (velocityXZ.magnitude > maxSpeed)
			{
				velocityXZ = velocityXZ.normalized * maxSpeed;
				rB.velocity = new Vector3(velocityXZ.x, rB.velocity.y, velocityXZ.y);
			}

			if (!dashCDReady)
			{
				if (dashCDTimer > 0)
				{
					dashCDTimer -= Time.fixedDeltaTime;
				}
				else
				{
					dashCDTimer = 0;
					dashCDReady = true;
				}
			}
		}
		else
		{
			Vector2 velocityXZ = new Vector2(rB.velocity.x, rB.velocity.z);
			if (velocityXZ.magnitude > maxDashSpeed)
			{
				velocityXZ = velocityXZ.normalized * maxSpeed;
				rB.velocity = new Vector3(velocityXZ.x, rB.velocity.y, velocityXZ.y);
			}

			if (dashingTimer > 0)
			{
				dashingTimer -= Time.fixedDeltaTime;
			}
			else
			{
				dashingTimer = 0;
				dashCDTimer = dashCDTimerTotal;
				dashing = false;
				cubeRenderer.material.SetColor("_Color", originalColor);
			}
		}
	}
	public override void Heuristic(in ActionBuffers actionsOut)
	{

		float rotateValue = Input.GetAxis("Horizontal");
		float forwardValue = Input.GetAxis("Vertical");

		int rotateAction = 0;
		int forwardAction = 0;

		if (rotateValue > 0.01f)
		{
			rotateAction = 1;
		}
		else if (rotateValue < -0.01f)
		{
			rotateAction = 2;
		}

		if (forwardValue > 0.01f)
		{
			forwardAction = 1;
		}
		else if (forwardValue < -0.01f)
		{
			forwardAction = 2;
		}

		var discreteActions = actionsOut.DiscreteActions;
		discreteActions[0] = rotateAction;
		discreteActions[1] = forwardAction;

		if (Input.GetKey(KeyCode.Space) && dashCDReady)
		{
			discreteActions[2] = 1;
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		//if (!other.tag.Contains("Gold") && !other.tag.Contains("Gap")) return;

		if (other.tag.Contains("Gold"))
		{
			AddReward(.1f);
			if (dashing)
			{
				AddReward(.02f);
			}
			gameGroup.event_GoalScored.Invoke(teamID);

			gameGroup.DestroyGold(other.gameObject.transform);
		}
		else if (other.tag.Contains("Gap"))
		{
			if (dashing)
			{
				AddReward(.05f);
			}
		}
		else if (other.tag.Contains("Reset"))
		{
			//AddReward(-0.01f);
			transform.localPosition = originalPos;
			transform.localRotation = originalRota;
			cubeRenderer.material.SetColor("_Color", originalColor);
			rB.velocity = Vector3.zero;
		}
	}
	private void OnCollisionEnter(Collision other)
	{
		//"GameAgent_0" or "GameAgent_1" means blue & red team
		if (!other.gameObject.tag.Contains("GameAgent_" + teamID_Opponent) && dashing)
		{
			AddReward(.02f);
		}
	}
}
