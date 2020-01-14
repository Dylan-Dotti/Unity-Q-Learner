using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QValueManager
{
    private Dictionary<State, Dictionary<Action, float>> stateQValues;

    public IReadOnlyList<State> States { get => stateQValues.Keys.ToList(); }

    public QValueManager()
    {
        stateQValues = new Dictionary<State, Dictionary<Action, float>>();
    }

    public float GetQValue(State state, Action action)
    {
        return stateQValues[GetStateActual(state)][action];
    }

    public void SetQValue(State state, Action action, float newVal)
    {
        stateQValues[GetStateActual(state)][action] = newVal;
    }

    public bool HasState(State state)
    {
        return stateQValues.Keys.Any(k => k.Equals(state));
    }

    public bool AttemptAddState(State state)
    {
        state = GetStateActual(state);
        if (HasState(state))
        {
            return false;
        }
        Dictionary<Action, float> qValues = new Dictionary<Action, float>();
        foreach (Action a in state.Actions)
        {
            qValues.Add(a, 0f);
        }
        stateQValues.Add(state, qValues);
        //Debug.Log("New State: " + state.ToString());
        //Debug.Log("States: " + States.Count);
        return true;
    }

    public void ClearStates()
    {
        stateQValues.Clear();
    }

    public float GetMaxQValue(State state)
    {
        return stateQValues[GetStateActual(state)].Max(qv => qv.Value);
    }

    public Action GetMaxValueAction(State state)
    {
        state = GetStateActual(state);
        float maxQV = GetMaxQValue(state);
        List<Action> bestActions = new List<Action>();
        foreach (KeyValuePair<Action, float> qvPair in stateQValues[state])
        {
            if (qvPair.Value == maxQV)
            {
                bestActions.Add(qvPair.Key);
            }
        }
        return bestActions[Random.Range(0, bestActions.Count)];
    }

    public void UpdateQValue(StateTransition transition, float reward)
    {
        MDPSettings mdp = MDPSettings.Instance;
        State prevState = GetStateActual(transition.PrevState);
        State newState = GetStateActual(transition.NewState);
        Action action = transition.TransAction;
        float nextQMax = newState == null ? 0 : GetMaxQValue(newState);
        // temporal difference equation
        stateQValues[prevState][action] = 
            stateQValues[prevState][action] + mdp.LearningRate *
            (reward + mdp.DiscountFactor * nextQMax - stateQValues[prevState][action]);
    }

    private State GetStateActual(State state)
    {
        if (state != null)
        {
            foreach (State s in States)
            {
                if (s.Equals(state))
                {
                    return s;
                }
            }
        }
        return state;
    }
}
