using System.Collections.Generic;

public class GridSquareWall : GridSquare
{
    public override IReadOnlyList<Action> Actions => new List<Action>();
    public override bool Walkable => false;

    public override IQLearningState GetNextState(Action action)
    {
        throw new System.NotImplementedException();
    }
}
