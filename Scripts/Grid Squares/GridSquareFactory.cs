using UnityEngine;

public class GridSquareFactory : MonoBehaviour
{
    public static GridSquareFactory Instance { get; private set; }

    [SerializeField] private GridSquareFourWay fourWaySquarePrefab;
    [SerializeField] private GridSquareWall wallSquarePrefab;
    [SerializeField] private GridSquareAgentSpawn spawnSquarePrefab;
    [SerializeField] private GridSquareButton buttonSquarePrefab;
    [SerializeField] private GridSquareDoor doorSquarePrefab;
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

    public GridSquareButton GetButtonSquare()
    {
        return Instantiate(buttonSquarePrefab);
    }

    public GridSquareDoor GetDoorSquare(
        GridSquareDoor.Orientation orient, bool openByDefault=false)
    {
        GridSquareDoor door = Instantiate(doorSquarePrefab);
        door.Orient = orient;
        door.OpenByDefault = openByDefault;
        return door;
    }

    public GridSquareExit GetExitSquare(float exitReward)
    {
        GridSquareExit exitSquare = Instantiate(exitSquarePrefab);
        exitSquare.ExitReward = exitReward;
        return exitSquare;
    }
}
