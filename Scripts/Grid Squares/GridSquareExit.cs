using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridSquareExit : GridSquare
{
    public override IReadOnlyList<Action> Actions => new List<Action>{ Action.Interact };

    public override float ExitReward
    {
        get => base.ExitReward;
        set
        {
            base.ExitReward = value;
            exitText.text = value == 0 ? "Exit" :
                value > 0 ? "Good Exit" : "Bad Exit";
        }
    }

    [SerializeField] private TextMeshProUGUI exitText;
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
        /*if (QValues[Action.Interact] == 0)
        {
            rend.material = defaultMaterial;
        }
        else
        {
            rend.material = QValues[Action.Interact] > 0 ?
                positiveRewardMaterial : negativeRewardMaterial;
        }*/
    }

    public override GridSquare GetNextSquare(Action action)
    {
        if (action == Action.Interact)
        {
            return null;
        }
        throw new System.ArgumentException("Unsupported Action");
    }
}
