using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquareButton : GridSquareFourWay
{
    [SerializeField] private Material buttonPressedMaterial;
    [SerializeField] private Material buttonUnpressedMaterial;
    [SerializeField] private Renderer buttonRend;

    public bool IsPressed { get; private set; }
    public override IReadOnlyList<Action> Actions =>
    new List<Action> { Action.Up, Action.Down,
            Action.Left, Action.Right, Action.Interact };

    private List<IButtonPressHandler> pressHandlers;

    protected override void Awake()
    {
        base.Awake();
        pressHandlers = new List<IButtonPressHandler>();
    }

    public void PressButton()
    {
        IsPressed = true;
        buttonRend.material = buttonPressedMaterial;
        pressHandlers.ForEach(h => h.OnButtonPressed());
    }

    public void ResetButton()
    {
        IsPressed = false;
        buttonRend.material = buttonUnpressedMaterial;
    }

    public override GridSquare GetNextSquare(Action action)
    {
        switch (action)
        {
            case Action.Up:
            case Action.Down:
            case Action.Left:
            case Action.Right:
                return base.GetNextSquare(action);
            case Action.Interact:
                return this;
            default:
                throw new System.ArgumentException("Unsupported Action");
        }
    }

    public override void ResetState()
    {
        base.ResetState();
        ResetButton();
    }

    public override void OnActionPerformed(Action action)
    {
        base.OnActionPerformed(action);
        if (action == Action.Interact)
        {
            if (!IsPressed)
            {
                PressButton();
            }
        }
    }

    public override Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> baseDict = base.ToDictionary();
        baseDict.Add("pressed", IsPressed.ToString());
        return baseDict;
    }

    public void AddHandler(IButtonPressHandler handler)
    {
        pressHandlers.Add(handler);
    }

    public void RemoveHandler(IButtonPressHandler handler)
    {
        pressHandlers.Remove(handler);
    }
}
