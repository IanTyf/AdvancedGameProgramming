using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject aiPrefab;
    public GameObject cubePrefab;
    public GameObject playerPrefab;
    public FiniteStateMachine<GameManager> FSM;

    public GameObject TitleScreenUI;
    public GameObject MainGameUI;
    public GameObject EndGameUI;

    public Material[] teamMats;

    private int redScore = 0;
    private int blueScore = 0;

    [SerializeField]
    private float cooldown = 2f;
    private float timer;
    [SerializeField]
    private float gameTime = 30f;

    void Awake()
    {
        Services.gameManager = this;
        Services.Init();
        FSM = new FiniteStateMachine<GameManager>(this);
        FSM.TransitionTo<StateTitleScreen>();
        //createAIsAtRandomPos(2);
        //createCollectableCubes(6);
    }

    private void Update()
    {
        /*
        // create an additional cube every once in a while
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            List<GameObject> newCubes = createCollectableCubes(1);
            Services.aiManager.CheckOutNewTargets(newCubes); // this lets the AIs compare their current target with the newly created cubes and see if any of the new ones is closer
            timer = 0;
            cooldown = Random.Range(1f, 2.5f);
        }
        */
        FSM.Update();
    }

    private void FixedUpdate()
    {
        /*
        Services.aiManager.moveAI();
        */
        FSM.FixedUpdate();
    }

    private abstract class baseState : FiniteStateMachine<GameManager>.State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Context.TitleScreenUI.SetActive(false);
            Context.MainGameUI.SetActive(false);
            Context.EndGameUI.SetActive(false);
        }
    }

    private class StateTitleScreen : baseState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Context.TitleScreenUI.SetActive(true);
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TransitionTo<StateMainGame>();
            }
        }
    }

    private class StateMainGame : baseState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Context.MainGameUI.SetActive(true);

            Services.aiManager = new AILifecycle();
            Services.cubeManager = new CubeManager();

            Instantiate(Context.playerPrefab);
            Context.createAIsAtRandomPos(5);
            Context.createCollectableCubes(10);

            Services.eventManager.Register<Event_GoalScored>(onGoal);
            Services.eventManager.Register<Event_TimedOut>(timeOut);
        }

        public override void Update()
        {
            base.Update();
            Context.timer += Time.deltaTime;
            if (Context.timer > Context.cooldown)
            {
                List<GameObject> newCubes = Context.createCollectableCubes(4);
                Services.aiManager.CheckOutNewTargets(newCubes); // this lets the AIs compare their current target with the newly created cubes and see if any of the new ones is closer
                Context.timer = 0;
                Context.cooldown = Random.Range(1f, 2.5f);
            }

            Context.gameTime -= Time.deltaTime;
            if (Context.gameTime <= 0)
            {
                Services.eventManager.Fire(new Event_TimedOut(Context.blueScore, Context.redScore));
                TransitionTo<StateEndGame>();
            }
            Context.updateTimeLeftUI();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Services.aiManager.moveAI();
        }

        public override void OnExit()
        {
            base.OnExit();
            Services.eventManager.Unregister<Event_GoalScored>(onGoal);
            Services.eventManager.Unregister<Event_TimedOut>(timeOut);
        }

        private void onGoal(AGPEvent e)
        {
            var goalEvt = (Event_GoalScored)e;
            if (goalEvt.teamIDScored == 0) Context.redScore++;
            else if (goalEvt.teamIDScored == 1) Context.blueScore++;
            else Debug.Log("Error: no team " + goalEvt.teamIDScored + " found");

            Context.updateScoreUI();
        }

        private void timeOut(AGPEvent e)
        {
            //check the score and determine a winner

            TransitionTo<StateEndGame>();
        }
    }

    private class StateEndGame : baseState
    {
        
    }

    private void updateScoreUI()
    {
        MainGameUI.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Red: " + redScore;
        MainGameUI.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Blue: " + blueScore;
    }

    private void updateTimeLeftUI()
    {
        MainGameUI.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Time Left: " + (int)gameTime;
    }

    // create a number of AIs at random positions on the plane and return them in a list
    private List<GameObject> createAIsAtRandomPos(int numOfAI)
    {
        List<GameObject> retAIs = new List<GameObject>();
        for (int i = 0; i < numOfAI; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-13f, 13f), 0.5f, Random.Range(-8f, 8f));
            GameObject newAI = Instantiate(aiPrefab, transform);
            Services.aiManager.createAI(newAI, randPos, i % 2, teamMats[i%2]);
            retAIs.Add(newAI);
        }
        return retAIs;
    }

    // create a number of collectable cubes at random positions on the plane and return them in a list
    private List<GameObject> createCollectableCubes(int numOfCubes)
    {
        List<GameObject> retCubes = new List<GameObject>();
        for (int i = 0; i < numOfCubes; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-13f, 13f), 0.5f, Random.Range(-8f, 8f));
            GameObject newCube = Instantiate(cubePrefab, transform);
            Services.cubeManager.createCube(newCube, randPos);
            retCubes.Add(newCube);
        }
        return retCubes;
    }
}
