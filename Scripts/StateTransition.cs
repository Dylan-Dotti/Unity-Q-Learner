
public class StateTransition
{
    public State PrevState { get; private set; }
    public State NewState { get; private set; }
    public Action TransAction { get; private set; }

    public StateTransition(State prevState, Action action, State newState)
    {
        PrevState = prevState;
        TransAction = action;
        NewState = newState;
    }
}
