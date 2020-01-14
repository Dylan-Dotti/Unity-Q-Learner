using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquareFourWay : GridSquare
{
    public override IReadOnlyList<Action> Actions => new List<Action>
    {
        Action.Up, Action.Down, Action.Left, Action.Right
    };

    public override GridSquare GetNextSquare(Action action)
    {
        int newRow = Row, newCol = Col;
        switch (action)
        {
            case Action.Up:
                newRow -= 1;
                break;
            case Action.Down:
                newRow += 1;
                break;
            case Action.Left:
                newCol -= 1;
                break;
            case Action.Right:
                newCol += 1;
                break;
            default:
                throw new System.ArgumentException("Unsupported Action");
        }
        if (!ParentGrid.CoordsInRange(newRow, newCol))
        {
            return this;
        }
        GridSquare newSquare = ParentGrid.GetSquareAt(newRow, newCol);
        return newSquare == null ? this : newSquare.Walkable ? newSquare : this;
    }
}
