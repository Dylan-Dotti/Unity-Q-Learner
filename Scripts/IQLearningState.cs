using System.Collections.Generic;
using UnityEngine;

public interface IQLearningState
{
    QLearningAgent Occupant { get; set; }
    bool IsOccupied { get; }
    bool Walkable { get; }

    IReadOnlyList<Action> Actions { get; }
    IReadOnlyDictionary<Action, float> QValues { get; }
    float EnterReward { get; set; }
    float ExitReward { get; set; }

    void OnAgentEntered(QLearningAgent agent);
    void OnAgentExited(QLearningAgent agent);

    IQLearningState GetNextState(Action action);
    float GetMaxQValue();
    Action GetMaxValueAction();
    Action GetRandomAction();
    void UpdateQValue(Action action, float reward, float nextStateMaxQ);
    Vector3 GetAgentPosition(float hoverHeight = 0.5f);
}
