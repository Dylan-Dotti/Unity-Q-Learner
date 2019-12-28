using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QLearningAgent : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float epsilon;

    public IQLearningState OccupiedState { get; private set; }
    public IQLearningState SpawnState { get; private set; }
    public ICollection<float> RewardBuffer { get; private set; }

    private void Awake()
    {
        RewardBuffer = new List<float>();
    }

    private void Start()
    {
        TimeEventEmitter.Instance.PeriodicTimeEvent += 
            TransitionToNextState;
    }

    public void SpawnAtState(IQLearningState spawnState)
    {
        SpawnState = spawnState;
        MoveToState(spawnState);
    }

    public void Despawn()
    {
        RemoveFromOccupiedState();
        gameObject.SetActive(false);
    }

    public void TransitionToNextState()
    {
        IQLearningState prevState = OccupiedState;
        Action selectedAction = Random.Range(0f, 1f) > epsilon ?
            OccupiedState.GetMaxValueAction() : OccupiedState.GetRandomAction();
        IQLearningState nextState = OccupiedState.GetNextState(selectedAction);
        MoveToState(nextState ?? SpawnState);
        float totalRewards = RewardBuffer.Sum();
        prevState.UpdateQValue(selectedAction, totalRewards, nextState == null ?
            0 : nextState.GetMaxQValue());
        RewardBuffer.Clear();

    }

    public void MoveToState(IQLearningState state)
    {
        AddToState(state);
        transform.position = state.GetAgentPosition(1);
    }

    private void AddToState(IQLearningState state)
    {
        RemoveFromOccupiedState();
        OccupiedState = state;
        state.Occupant = this;
        state.OnAgentEntered(this);
    }

    private void RemoveFromOccupiedState()
    {
        if (OccupiedState != null)
        {
            IQLearningState prevState = OccupiedState;
            OccupiedState.Occupant = null;
            OccupiedState = null;
            prevState.OnAgentExited(this);
        }
    }
}
