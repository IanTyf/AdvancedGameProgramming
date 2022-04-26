using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisitorProperty_Showcase : MonoBehaviour
{
	[SerializeField]
	LocalReference reference;
	ImageExample imageExample;
	[SerializeField]
	TextMesh tX_Name;
	[SerializeField]
	string referenceCameraName = "Main Camera";
	[SerializeField]
	CharacterSetDB characterSetDB;

	[SerializeField]
	DressingUp_Visitor dressingUp_Visitor;
	[SerializeField]
	Transform mainBody;
	[SerializeField]
	Animator anim;

	Transform mainCamera;

	NavMeshAgent nav;

	string currentDirection = "Right";
	float timer_Direction = 0;
	float timer_DirectionTotal = 1;

	float strength = 1000;
	bool isExiting = false;

	[HideInInspector]
	public int dressCode = 0;

	float moveTimer = 0;
	float moveTimerTotal = 0;

	// Use this for initialization
	void Start ()
	{
		mainCamera = reference.GetReference<Transform>(referenceCameraName);
		imageExample = reference.GetReference<Transform>("ImageExample").GetComponent<ImageExample>();

		imageExample.onLoadFinished += CharacterSetDBRefreshed;

		//tX_Name.text = csvExample.GetRandomName();
		strength = Random.Range(500, 1000);
		nav = GetComponent<NavMeshAgent>();
		nav.enabled = true;

	}

	private void OnDestroy()
	{
		imageExample.onLoadFinished -= CharacterSetDBRefreshed;
	}

	void CharacterSetDBRefreshed()
	{
		dressCode = Random.Range(0, characterSetDB.cosplayerSet.Count);
		CharacterSpriteSet c = characterSetDB.cosplayerSet[dressCode];
		dressingUp_Visitor.ClothesSetUp(c.sBody,
										c.sHair,
										c.sHead,
										c.sFace,
										c.sLegsL,
										c.sLegsR,
										c.sHandL,
										c.sHandR);
	}

	// Update is called once per frame
	void Update () {
		if (!isExiting)
		{
			if (strength > 0)
			{
				strength -= 0.01f * Time.deltaTime;
				if(moveTimer > moveTimerTotal)
				{
					moveTimer = 0;
					moveTimerTotal = Random.Range(1.1f, 3.3f);

					nav.SetDestination(transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
				}
				else
				{
					moveTimer += Time.deltaTime;
				}
			}
			else
			{
				isExiting = true;
				//nav.SetDestination(visitorManager.exitPoints[Random.Range(0, visitorManager.exitPoints.Length)].transform.position);
			}
		}


		if (nav.velocity.magnitude < 0.1f)
		{
			anim.SetInteger("AnimationState", 0);
		}
		else
		{
			anim.SetInteger("AnimationState", 1);
			anim.SetFloat("Velocity", nav.velocity.magnitude);
		}

		//Direction Change Detection
		if (timer_Direction > timer_DirectionTotal)
		{
			timer_Direction = 0;
			timer_DirectionTotal = Random.Range(0.2f, 1.23f);
			float currentRelativeDirection = Vector3.Dot(nav.velocity, mainCamera.right);
			if (currentRelativeDirection > 0 && currentDirection == "Right")
			{
				currentDirection = "Left";
				var localS = mainBody.localScale;
				localS.x *= -1;
				mainBody.localScale = localS;
			}
			else if (currentRelativeDirection < 0 && currentDirection == "Left")
			{
				currentDirection = "Right";
				var localS = mainBody.localScale;
				localS.x *= -1;
				mainBody.localScale = localS;
			}
		}
		else
		{
			timer_Direction += Time.deltaTime;
		}
		//Actually
		mainBody.LookAt(mainCamera);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("ExitArea"))
		{

			//visitorManager.RemoveVisitor(this);
			Destroy(gameObject);
		}
	}
}
