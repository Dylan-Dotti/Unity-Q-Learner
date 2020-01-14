using System.Collections.Generic;

public class GridSquareWall : GridSquare
{
    public override IReadOnlyList<Action> Actions => new List<Action>();
    public override bool Walkable => false;

    public override GridSquare GetNextSquare(Action action)
    {
        return null;
    }
}
