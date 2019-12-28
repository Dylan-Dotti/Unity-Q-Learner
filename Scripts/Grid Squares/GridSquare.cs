using System.Collections.Generic;
using UnityEngine;

public abstract class GridSquare : MonoBehaviour, IQLearningState
{
    public Grid ParentGrid { get; private set; }
    public int Row { get; private set; }
    public int Col { get; private set; }
    public QLearningAgent Occupant { get; set; }
    public bool IsOccupied { get => Occupant != null; }
    public virtual bool Walkable { get => true; }

    public abstract IReadOnlyList<Action> Actions { get; }
    public IReadOnlyDictionary<Action, float> QValues { get => qValues; }
    public float EnterReward { get; set; } = 0;
    public float ExitReward { get; set; } = 0;

    private Dictionary<Action, float> qValues = 
        new Dictionary<Action, float>();

    private MDPSettings mdp;

    protected virtual void Awake()
    {
        foreach (Action a in Actions)
        {
            qValues.Add(a, 0f);
            mdp = MDPSettings.Instance;
        }
    }

    public void AddToGrid(Grid grid, int row, int col)
    {
        ParentGrid = grid;
        transform.parent = grid.transform;
        Row = row;
        Col = col;
    }

    public virtual void OnAgentEntered(QLearningAgent agent)
    {
        agent.RewardBuffer.Add(EnterReward);
    }

    public virtual void OnAgentExited(QLearningAgent agent)
    {
        agent.RewardBuffer.Add(ExitReward);
    }

    public abstract IQLearningState GetNextState(Action action);

    public Action GetRandomAction()
    {
        return Actions[Random.Range(0, Actions.Count)];
    }

    public float GetMaxQValue()
    {
        return QValues[GetMaxValueAction()];
    }

    public Action GetMaxValueAction()
    {
        KeyValuePair<Action, float> bestAVPair =
            new KeyValuePair<Action, float>(Action.None, float.MinValue);
        foreach (KeyValuePair<Action, float> avPair in QValues)
        {
            if (avPair.Value > bestAVPair.Value)
            {
                bestAVPair = avPair;
            }
        }
        List<Action> bestActions = new List<Action>();
        foreach (Action a in Actions)
        {
            if (QValues[a] == bestAVPair.Value)
            {
                bestActions.Add(a);
            }
        }
        return bestActions[Random.Range(0, bestActions.Count)];

    }

    public void UpdateQValue(Action action, float reward, float nextStateMaxQ)
    {
        qValues[action] = qValues[action] + mdp.LearningRate *
            (reward + mdp.DiscountFactor * nextStateMaxQ - qValues[action]);
    }

    public Vector3 GetAgentPosition(float hoverHeight = 0.5f)
    {
        return transform.position + Vector3.back * hoverHeight;
    }
}
