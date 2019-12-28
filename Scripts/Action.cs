
public enum Action
{
    Up, Down, Left, Right, Exit, None
}

static class ActionMethods
{
    public static Action GetLeftPerpAction(this Action action)
    {
        switch (action)
        {
            case Action.Up:
                return Action.Left;
            case Action.Left:
                return Action.Down;
            case Action.Down:
                return Action.Right;
            case Action.Right:
                return Action.Up;
            default:
                throw new System.ArgumentException("Unsupported Action");
        }
    }

    public static Action GetRightPerpAction(this Action action)
    {
        switch (action)
        {
            case Action.Up:
                return Action.Right;
            case Action.Right:
                return Action.Down;
            case Action.Down:
                return Action.Left;
            case Action.Left:
                return Action.Up;
            default:
                throw new System.ArgumentException("Unsupported Action");
        }
    }
}