 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquareAgentSpawn : GridSquareFourWay
{
    [SerializeField] private QLearningAgent agentPrefab;

    private void Start()
    {
        SpawnAgent();
    }

    public void SpawnAgent()
    {
        QLearningAgent agent = Instantiate(agentPrefab);
        agent.SpawnAtState(this);
    }
}
