using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private GridSquare[,] grid;
    private GridSquareFactory squareFactory;

    private void Awake()
    {
        squareFactory = GridSquareFactory.Instance;
    }

    private void Start()
    {
        InitDemoGrid();
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
        AddSquareAt(squareFactory.GetWallSquare(), 1, 1);
        AddSquareAt(squareFactory.GetExitSquare(1), 0, 3);
        AddSquareAt(squareFactory.GetExitSquare(-1), 1, 3);
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

    public bool AddSquareAt(GridSquare square, int row, int col)
    {
        if (!IsOccupied(row, col)) // if square not occupied
        {
            grid[row, col] = square;
            square.AddToGrid(this, row, col);
            square.transform.localPosition = new Vector3(0.5f + col, -0.5f - row);
            return true;
        }
        return false;

    }

    public GridSquare GetSquareAt(int row, int col)
    {
        return grid[row, col];
    }

    public bool IsOccupied(int row, int col)
    {
        return GetSquareAt(row, col) != null;
    }

    public bool CoordsInRange(int row, int col)
    {
        return row >= 0 && row < grid.GetLength(0) &&
            col >= 0 && col < grid.GetLength(1);
    }
}
