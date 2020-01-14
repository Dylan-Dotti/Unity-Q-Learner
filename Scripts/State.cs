using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract IReadOnlyList<Action> Actions { get; }

    private string stateString;

    public State(string stateString)
    {
        this.stateString = stateString;
    }

    public Action GetRandomAction()
    {
        return Actions[Random.Range(0, Actions.Count)];
    }

    public bool Equals(State otherState)
    {
        return stateString.Equals(otherState.stateString);
    }

    public override string ToString()
    {
        return stateString;
    }

    public abstract void OnAgentEntered(QLearningAgent agent);

    public abstract void OnAgentExited(QLearningAgent agent);
}
