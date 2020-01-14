using System.Collections.Generic;
using UnityEngine;

public abstract class GridSquare : MonoBehaviour, IRewardGiver
{
    public Grid ParentGrid { get; private set; }
    public int Row { get; private set; }
    public int Col { get; private set; }
    public virtual bool Walkable { get => true; }

    public abstract IReadOnlyList<Action> Actions { get; }
    public virtual float EnterReward { get; set; } = 0;
    public virtual float ExitReward { get; set; } = 0;

    protected virtual void Awake()
    {

    }

    public void AddToGrid(Grid grid, int row, int col)
    {
        ParentGrid = grid;
        transform.parent = grid.transform;
        Row = row;
        Col = col;
    }

    public virtual void ResetState() { }

    public abstract GridSquare GetNextSquare(Action action);

    public Vector3 GetAgentPosition(float hoverHeight = 0.5f)
    {
        return transform.position + Vector3.back * hoverHeight;
    }

    public virtual Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { "row", Row.ToString() },
            { "col", Col.ToString() },
            { "walkable", Walkable.ToString() }
        };
    }

    public override string ToString()
    {
        Dictionary<string, string> stateDict = ToDictionary();
        string stateStr = "";
        foreach (KeyValuePair<string, string> kvPair in stateDict)
        {
            stateStr += string.Format("{0} : {1}{2}", 
                kvPair.Key, kvPair.Value, System.Environment.NewLine);
        }
        return stateStr;
    }

    public virtual void OnActionPerformed(Action action)
    {

    }
}
