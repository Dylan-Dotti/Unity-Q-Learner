using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquareFactory : MonoBehaviour
{
    public static GridSquareFactory Instance { get; private set; }

    [SerializeField] private GridSquareFourWay fourWaySquarePrefab;
    [SerializeField] private GridSquareWall wallSquarePrefab;
    [SerializeField] private GridSquareAgentSpawn spawnSquarePrefab;
    [SerializeField] private GridSquareExit exitSquarePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GridSquareFourWay GetFourWaySquare()
    {
        return Instantiate(fourWaySquarePrefab);
    }

    public GridSquareWall GetWallSquare()
    {
        return Instantiate(wallSquarePrefab);
    }

    public GridSquareAgentSpawn GetSpawnSquare()
    {
        return Instantiate(spawnSquarePrefab);
    }

    public GridSquareExit GetExitSquare(float exitReward)
    {
        GridSquareExit exitSquare = Instantiate(exitSquarePrefab);
        exitSquare.ExitReward = exitReward;
        return exitSquare;
    }
}
