 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquareAgentSpawn : GridSquareFourWay
{
    [SerializeField] private GridNavigator navigatorPrefab;

    private void Start()
    {
        SpawnAgent();
    }

    public void SpawnAgent()
    {
        GridNavigator navAgent = Instantiate(navigatorPrefab);
        navAgent.SpawnAtSquare(this);
    }
}
