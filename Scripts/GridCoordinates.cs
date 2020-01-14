using UnityEngine;

[System.Serializable]
public class GridCoordinates
{
    public int Row { get => row; }
    public int Col { get => col; }

    [SerializeField] private int row;
    [SerializeField] private int col;

    public GridCoordinates(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public bool Equals(GridCoordinates otherCoords)
    {
        return Row == otherCoords.Row && Col == otherCoords.Col;
    }

    public override string ToString()
    {
        return string.Format("GridCoordinates({0}, {1})", Row, Col);
    }
}
