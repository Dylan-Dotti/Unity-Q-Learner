using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QLearningAgent : MonoBehaviour
{
    public float Epsilon { get => epsilon; set => epsilon = value; }

    [SerializeField][Range(0, 1)] private float epsilon;

    private State currentState;
    private ICollection<float> rewardBuffer;
    private QValueManager qManager;

    private void Awake()
    {
        rewardBuffer = new List<float>();
        qManager = new QValueManager();
    }

    private void Start()
    {
        EpsilonDisplay.Instance.trackedAgent = this;
    }

    public void ResetAgent()
    {
        currentState = null;
        rewardBuffer.Clear();
        qManager.ClearStates();
    }

    public void SpawnAtState(State spawnState)
    {
        EnterState(spawnState, true);
        rewardBuffer.Clear();
    }

    public void UpdateState(State newState, Action lastAction)
    {
        State prevState = currentState;
        TransitionToState(newState);
        float totalReward = rewardBuffer.Sum();
        rewardBuffer.Clear();
        StateTransition transition = new StateTransition(
            prevState, lastAction, currentState);
        qManager.UpdateQValue(transition, totalReward);
    }

    public Action GetNextAction()
    {
        if (Random.Range(0f, 1f) < epsilon)
        {
            return currentState.GetRandomAction();
        }
        return qManager.GetMaxValueAction(currentState);
    }

    public void ReceiveReward(float reward)
    {
        rewardBuffer.Add(reward);
    }

    private void TransitionToState(State newState)
    {
        ExitCurrentState();
        EnterState(newState);
    }

    private void EnterState(State newState, bool isSpawning=false)
    {
        currentState = newState;
        if (currentState != null)
        {
            qManager.AttemptAddState(currentState);
            if (!isSpawning)
            {
                currentState.OnAgentEntered(this);
            }
            /*string qvStr = "";
            foreach (Action a in currentState.Actions)
            {
                qvStr += string.Format("{0} : {1}{2}",
                    a, qManager.GetQValue(currentState, a), System.Environment.NewLine);
            }
            Debug.Log("State Q Values: " + System.Environment.NewLine + qvStr);*/
        }
    }

    private void ExitCurrentState()
    {
        if (currentState != null)
        {
            State temp = currentState;
            currentState = null;
            temp.OnAgentExited(this);
        }
    }
}
