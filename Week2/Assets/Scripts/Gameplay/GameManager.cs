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

    public int GameTime;
    public float CubeSpawnCooldown;


    private int redScore = 0;
    private int blueScore = 0;
    
    private float cooldown = 2f;
    private float timer;
    private float gameTime;

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

            Services.eventManager.Register<Event_GameStarted>(onStartGame);
            Services.eventManager.Register<Event_GoalScored>(onGoal);
            Services.eventManager.Register<Event_TimedOut>(timeOut);

            // this seems redundant for now but it's the logical way to do it
            Services.eventManager.Fire(new Event_GameStarted());
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
            Services.eventManager.Unregister<Event_GameStarted>(onStartGame); // just in case for some reason game never actually started
            Services.eventManager.Unregister<Event_GoalScored>(onGoal);
            Services.eventManager.Unregister<Event_TimedOut>(timeOut);
        }

        private void onStartGame(AGPEvent e)
        {
            Debug.Log("Game Started");
            Context.redScore = 0;
            Context.blueScore = 0;
            Context.gameTime = Context.GameTime;
            Context.timer = 0;
            Context.cooldown = Context.CubeSpawnCooldown;
            Context.updateScoreUI();
            
            // because we only want start game event happen once
            Services.eventManager.Unregister<Event_GameStarted>(onStartGame);
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
            Context.updateWinnerUI();
            Services.aiManager.deleteAllAIs();
            Services.cubeManager.deleteAllCubes();
            Destroy(Services.player.gameObject);
            TransitionTo<StateEndGame>();
        }
    }

    private class StateEndGame : baseState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Context.EndGameUI.SetActive(true);
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TransitionTo<StateTitleScreen>();
            }
        }
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

    private void updateWinnerUI()
    {
        string winnerText = "";
        if (redScore > blueScore) winnerText = "RED TEAM WINS!!! " + redScore + " : " + blueScore;
        else if (redScore < blueScore) winnerText = "BLUE TEAM WINS!!! " + blueScore + " : " + redScore;
        else winnerText = "ITS A TIE?!?!!! DAMN GOOD GAME " + redScore + " : " + blueScore;
        EndGameUI.transform.GetChild(0).gameObject.GetComponent<Text>().text = winnerText;
    }

    // create a number of AIs at random positions on the plane and return them in a list
    private List<GameObject> createAIsAtRandomPos(int numOfAI)
    {
        List<GameObject> retAIs = new List<GameObject>();
        for (int i = 0; i < numOfAI; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-13f, 13f), 0.5f, Random.Range(-8f, 8f));
            GameObject newAI = Services.aiManager.createAI(aiPrefab, randPos, i % 2, teamMats[i%2]);
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
            GameObject newCube = Services.cubeManager.createCube(cubePrefab, randPos);
            retCubes.Add(newCube);
        }
        return retCubes;
    }
}
