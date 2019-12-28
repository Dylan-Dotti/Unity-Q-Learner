using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquareExit : GridSquare
{
    public override IReadOnlyList<Action> Actions => new List<Action>{ Action.Exit };

    [SerializeField] private Material positiveRewardMaterial;
    [SerializeField] private Material negativeRewardMaterial;

    private Renderer rend;
    private Material defaultMaterial;

    protected override void Awake()
    {
        base.Awake();
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
    }

    private void Update()
    {
        if (QValues[Action.Exit] == 0)
        {
            rend.material = defaultMaterial;
        }
        else
        {
            rend.material = QValues[Action.Exit] > 0 ?
                positiveRewardMaterial : negativeRewardMaterial;
        }
    }

    public override IQLearningState GetNextState(Action action)
    {
        if (action == Action.Exit)
        {
            return null;
        }
        throw new System.ArgumentException("Unsupported Action");
    }
}
