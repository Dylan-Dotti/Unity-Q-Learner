using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquareDoor : GridSquareFourWay, IButtonPressHandler
{
    [SerializeField] private GameObject doorOpened;
    [SerializeField] private GameObject doorClosed;

    public enum Orientation { TopHinge, BottomHinge, LeftHinge, RightHinge }

    public bool IsOpen { get; private set; }
    public bool OpenByDefault { get; set; }
    public Orientation Orient
    {
        get => orient;
        set { orient = value; OnOrientationChanged(); }
    }

    public override bool Walkable => IsOpen;
    public override IReadOnlyList<Action> Actions => 
        IsOpen ? base.Actions : new List<Action>();

    private Orientation orient = Orientation.TopHinge;

    public void OpenDoor()
    {
        IsOpen = true;
        doorClosed.SetActive(false);
        doorOpened.SetActive(true);
    }

    public void CloseDoor()
    {
        IsOpen = false;
        doorOpened.SetActive(false);
        doorClosed.SetActive(true);
    }

    public override void ResetState()
    {
        base.ResetState();
        if (OpenByDefault)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void OnButtonPressed()
    {
        OpenDoor();
    }

    public override Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> baseDict = base.ToDictionary();
        baseDict.Add("open", IsOpen.ToString());
        return baseDict;
    }

    private void OnOrientationChanged()
    {
        switch (Orient)
        {
            case Orientation.TopHinge:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Orientation.BottomHinge:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Orientation.LeftHinge:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Orientation.RightHinge:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            default:
                throw new System.ArgumentException("Unsupported orientation");
        }
    }
}
