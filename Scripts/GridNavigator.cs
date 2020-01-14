using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QLearningAgent))]
public class GridNavigator : MonoBehaviour
{
    public GridSquare OccupiedSquare { get; private set; }
    public GridSquare SpawnSquare { get; private set; }
    public Grid OccupiedGrid { get => OccupiedSquare.ParentGrid; }

    private QLearningAgent ai;

    private void Awake()
    {
        ai = GetComponent<QLearningAgent>();
        Timer.Instance.PeriodicTimeEvent += OnTimeEvent;
    }

    public void SpawnAtSquare(GridSquare spawnSquare)
    {
        SpawnSquare = spawnSquare;
        MoveToSquare(spawnSquare);
        OccupiedGrid.ResetState();
        ai.SpawnAtState(OccupiedGrid.ToGridState(OccupiedSquare));
    }

    public void Despawn()
    {
        RemoveFromOccupiedSquare();
        gameObject.SetActive(false);
    }

    public void MoveToSquare(GridSquare square)
    {
        AddToSquare(square);
        transform.position = square.GetAgentPosition(1);
    }

    private void AddToSquare(GridSquare square)
    {
        RemoveFromOccupiedSquare();
        OccupiedSquare = square;
    }

    private void RemoveFromOccupiedSquare()
    {
        if (OccupiedSquare != null)
        {
            OccupiedSquare = null;
        }
    }

    private void OnTimeEvent()
    {
        Action nextAction = ai.GetNextAction();
        GridSquare nextSquare = OccupiedSquare.GetNextSquare(nextAction);
        OccupiedSquare.OnActionPerformed(nextAction);
        if (nextSquare == null)
        {
            ai.UpdateState(null, nextAction);
            SpawnAtSquare(SpawnSquare);
        }
        else
        {
            MoveToSquare(nextSquare);
            ai.UpdateState(OccupiedGrid.ToGridState(OccupiedSquare), nextAction);
        }
    }
}
