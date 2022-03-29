using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using System.Linq;
using TMPro;

public class GameGroup : MonoBehaviour
{
    // replace our maxstep on single agent
    [Tooltip("Max Environment Steps")] public int MaxEnvironmentSteps = 1500;
    //current environment in this episode
    private int currentEnvironmentStep;

    [SerializeField]
    Transform prefab_Gold;

    // list of gold in the scene
    [HideInInspector]
    public List<Transform> currentGolds = new List<Transform>();
    // gold that will be spawned
    [SerializeField]
    List<Transform> spawnerList_Gold = new List<Transform>();
    float goldTimer = 0;
    float goldTimerTotal = 3;

    // AI Teams
    private SimpleMultiAgentGroup m_BlueAgentGroup;
    private SimpleMultiAgentGroup m_RedAgentGroup;

    // A list of Agents
    [SerializeField]
    List<MinerAgent> gameAgents;
    //List<MinerAgent> gameAgents_TeamBlue = new List<MinerAgent>();
    //List<MinerAgent> gameAgents_TeamRed = new List<MinerAgent>();

    int redScore = 0;
    int blueScore = 0;

    // score display
    [SerializeField]
    TextMeshPro tX_RedScore;
    [SerializeField]
    TextMeshPro tX_BlueScore;

    public delegate void Event_GoalScored(int teamIDScored);
    public Event_GoalScored event_GoalScored;

    // Start is called before the first frame update
    void Start()
    {
        m_BlueAgentGroup = new SimpleMultiAgentGroup();
        m_RedAgentGroup = new SimpleMultiAgentGroup();

        // add listener
        event_GoalScored += OnGoalScored;

        foreach (MinerAgent agent in gameAgents)
        {
            if (agent.GetComponent<BehaviorParameters>().TeamId == 0)
            {
                m_BlueAgentGroup.RegisterAgent(agent);
                //gameAgents_TeamBlue.Add(agent);
            }
            else
            {
                m_RedAgentGroup.RegisterAgent(agent);
                //gameAgents_TeamRed.Add(agent);
            }
        }

        ResetScene();
    }

    private void OnDestroy()
    {
        event_GoalScored -= OnGoalScored;
    }

    void FixedUpdate()
    {
        currentEnvironmentStep += 1;
        if (currentEnvironmentStep >= MaxEnvironmentSteps && MaxEnvironmentSteps > 0)
        {

            if (blueScore > redScore)
            {
                m_BlueAgentGroup.AddGroupReward(1f);
                m_RedAgentGroup.SetGroupReward(-1f);
                m_BlueAgentGroup.EndGroupEpisode();
                m_RedAgentGroup.EndGroupEpisode();
            }
            else if (blueScore < redScore)
            {
                m_RedAgentGroup.AddGroupReward(1f);
                m_BlueAgentGroup.SetGroupReward(-1f);
                m_BlueAgentGroup.EndGroupEpisode();
                m_RedAgentGroup.EndGroupEpisode();
            }
            else
            {
                m_RedAgentGroup.SetGroupReward(0);
                m_BlueAgentGroup.SetGroupReward(0);
                m_BlueAgentGroup.EndGroupEpisode();
                m_RedAgentGroup.EndGroupEpisode();
            }


            ResetScene();
        }
		else
        {
            m_BlueAgentGroup.AddGroupReward(-0.0002f / (blueScore + 1));
            m_RedAgentGroup.AddGroupReward(-0.0002f / (redScore + 1));
        }
    }


	void ResetScene()
    {
        currentEnvironmentStep = 0;
        blueScore = 0;
        redScore = 0;
        RefreshText();
        CleanupAllGold();
    }

    void OnGoalScored(int teamIDScored)
    {
        if (teamIDScored == 0)
        {
            blueScore++;
            m_BlueAgentGroup.AddGroupReward(0.1f);
        }
        else
        {
            redScore++;
            m_RedAgentGroup.AddGroupReward(0.1f);
        }
        RefreshText();
    }

    void RefreshText()
	{
        tX_BlueScore.text = "" + blueScore;
        tX_RedScore.text = "" + redScore;
    }

    //**Gold Spawning
    private void Update()
    {
        if (goldTimer > goldTimerTotal)
        {
            goldTimer = 0;
            GoldShower();

        }
        else
		{
            goldTimer += Time.deltaTime;

        }
    }

    void GoldShower()
    {
        for(int i = 0; i < 2; i++)
        {
            Transform spawner = spawnerList_Gold[Random.Range(0, spawnerList_Gold.Count)];
            Transform gold = Instantiate(prefab_Gold, transform);
            gold.transform.position = spawner.position;
            currentGolds.Add(gold);
        }
    }

    // happens on reset, remove all golds
    void CleanupAllGold()
	{
        foreach(Transform gold in currentGolds)
		{
            Destroy(gold.gameObject);
		}

        currentGolds.Clear();
    }

    public void DestroyGold(Transform goldTransform)
    {
        currentGolds.Remove(goldTransform);

        Destroy(goldTransform.gameObject);
    }
}
