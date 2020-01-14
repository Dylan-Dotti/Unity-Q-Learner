using System.Collections.Generic;
using UnityEngine;

public class GridState : State
{
    public IReadOnlyDictionary<Action, float> QValues { get => qValues; }
    public override IReadOnlyList<Action> Actions { get => agentSquare.Actions; }

    private Grid grid;
    private GridSquare agentSquare;
    private MDPSettings mdp;
    private Dictionary<Action, float> qValues;

    public GridState(Grid grid, GridSquare agentSquare) : base(grid.ToString()
        + new GridCoordinates(agentSquare.Row, agentSquare.Col).ToString())
    {
        this.grid = grid;
        this.agentSquare = agentSquare;
        mdp = MDPSettings.Instance;
        qValues = new Dictionary<Action, float>();
        foreach (Action a in agentSquare.Actions)
        {
            qValues.Add(a, 0f);
        }
    }

    public override void OnAgentEntered(QLearningAgent agent)
    {
        agent.ReceiveReward(agentSquare.EnterReward + mdp.LivingReward);
    }

    public override void OnAgentExited(QLearningAgent agent)
    {
        agent.ReceiveReward(agentSquare.ExitReward);
    }
}
