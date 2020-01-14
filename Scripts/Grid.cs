using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int NumRows { get => grid.GetLength(0); }
    public int NumCols { get => grid.GetLength(1); }

    private GridSquare[,] grid;
    private GridSquareFactory squareFactory;

    private void Awake()
    {
        squareFactory = GridSquareFactory.Instance;
    }

    private void Start()
    {
        InitDemoDoorGrid();
    }

    public void InitEmptyGrid(int numRows, int numCols)
    {
        transform.position += new Vector3(-numCols / 2f, numRows / 2f);
        grid = new GridSquare[numRows, numCols];
    }

    public void InitDemoGrid()
    {
        int numRows = 3;
        int numCols = 4;
        InitEmptyGrid(numRows, numCols);
        AddSquareAt(squareFactory.GetSpawnSquare(), 2, 0);
        AddSquareAt(squareFactory.GetExitSquare(1), 0, 3);
        AddSquareAt(squareFactory.GetExitSquare(-1), 1, 3);
        AddSquareAt(squareFactory.GetWallSquare(), 1, 1);

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                if (!IsOccupied(row, col))
                {
                    AddSquareAt(squareFactory.GetFourWaySquare(), row, col);
                }
            }
        }
    }

    public void InitDemoDoorGrid()
    {
        int numRows = 5;
        int numCols = 5;
        InitEmptyGrid(numRows, numCols);

        GridSquareDoor buttonDoor = squareFactory.GetDoorSquare(
            GridSquareDoor.Orientation.TopHinge);
        GridSquareDoor exitDoor = squareFactory.GetDoorSquare(
            GridSquareDoor.Orientation.LeftHinge);
        GridSquareButton button1 = squareFactory.GetButtonSquare();
        GridSquareButton button2 = squareFactory.GetButtonSquare();
        button1.AddHandler(buttonDoor);
        button2.AddHandler(exitDoor);

        for (int col = 1; col < 3; col++)
        {
            AddSquareAt(squareFactory.GetFourWaySquare(), 2, col);
        }
        AddSquareAt(squareFactory.GetFourWaySquare(), 3, 0);
        AddSquareAt(squareFactory.GetFourWaySquare(), 4, 0);
        AddSquareAt(squareFactory.GetExitSquare(-1), 3, 2);
        AddSquareAt(squareFactory.GetFourWaySquare(), 4, 1);
        AddSquareAt(squareFactory.GetFourWaySquare(), 4, 2);
        AddSquareAt(squareFactory.GetExitSquare(1), 0, 1);
        AddSquareAt(button2, 2, 4);
        AddSquareAt(squareFactory.GetWallSquare(), 1, 0);
        AddSquareAt(buttonDoor, 2, 3);
        AddSquareAt(squareFactory.GetWallSquare(), 1, 2);
        AddSquareAt(squareFactory.GetWallSquare(), 1, 3);
        AddSquareAt(exitDoor, 1, 1);
        AddSquareAt(squareFactory.GetWallSquare(), 3, 1);
        AddSquareAt(squareFactory.GetWallSquare(), 3, 3);
        AddSquareAt(squareFactory.GetSpawnSquare(), 2, 0);
        AddSquareAt(button1, 4, 3);
    }

    public bool AddSquareAt(GridSquare square, int row, int col)
    {
        if (!IsOccupied(row, col))
        {
            grid[row, col] = square;
            square.AddToGrid(this, row, col);
            square.transform.localPosition = new Vector3(0.5f + col, -0.5f - row);
            return true;
        }
        return false;
    }

    public bool RemoveSquareAt(int row, int col)
    {
        if (IsOccupied(row, col))
        {
            Destroy(grid[row, col]);
            return true;
        }
        return false;
    }

    public void ReplaceSquareAt(GridSquare newSquare, int row, int col)
    {
        RemoveSquareAt(row, col);
        AddSquareAt(newSquare, row, col);
    }

    public GridSquare GetSquareAt(int row, int col)
    {
        return grid[row, col];
    }

    public GridSquare GetAdjacentSquare(GridSquare square, Action dirAction)
    {
        int tarRow = square.Row;
        int tarCol = square.Col;
        switch (dirAction)
        {
            case Action.Up:
                tarRow--;
                break;
            case Action.Down:
                tarRow++;
                break;
            case Action.Left:
                tarCol--;
                break;
            case Action.Right:
                tarCol++;
                break;
            default:
                throw new System.ArgumentException("Unsupported Action");
        }
        if (CoordsInRange(tarRow, tarCol) && grid[tarRow, tarCol].Walkable)
        {
            return grid[tarRow, tarCol];
        }
        return square;
    }

    public bool IsOccupied(int row, int col)
    {
        return GetSquareAt(row, col) != null;
    }

    public bool CoordsInRange(int row, int col)
    {
        return row >= 0 && row < NumRows &&
            col >= 0 && col < NumCols;
    }

    public void ResetState()
    {
        foreach (GridSquare square in grid)
        {
            if (square != null)
            {
                square.ResetState();
            }
        }
    }

    public GridState ToGridState(GridSquare agentSquare)
    {
        return new GridState(this, agentSquare);
    }

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> gridDictionary =
            new Dictionary<string, string>();
        for (int row = 0; row < NumRows; row++)
        {
            for (int col = 0; col < NumCols; col++)
            {
                gridDictionary.Add(new GridCoordinates(row, col).ToString(), 
                    GetSquareAt(row, col) == null ?
                    null : GetSquareAt(row, col).ToString());
            }
        }
        return gridDictionary;
    }

    public override string ToString()
    {
        Dictionary<string, string> stateDict = ToDictionary();
        string stateStr = "";
        foreach (KeyValuePair<string, string> kvPair in stateDict)
        {
            stateStr += string.Format("{0} :{1}{2}{3}",
                kvPair.Key, System.Environment.NewLine, 
                kvPair.Value, System.Environment.NewLine);
        }
        return stateStr;
    }

    /*public bool Equals(Grid otherGrid)
    {
        if (NumRows != otherGrid.NumRows || 
            NumCols != otherGrid.NumCols)
        {
            return false;
        }
        for (int row = 0; row < NumRows; row++)
        {
            for (int col = 0; col < NumCols; col++)
            {
                if (grid[row, col] == null && otherGrid.grid[row, col] != null)
                {
                    return false;
                }
                else if (!grid[row, col].Equals(otherGrid.grid[row, col]))
                {
                    return false;
                }
            }
        }
        return true;
    }*/
}
