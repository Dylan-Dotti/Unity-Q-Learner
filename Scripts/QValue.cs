
public class QValue
{
    public Action Act { get; private set; }
    public float Value { get; private set; }

    public QValue(Action action, float value)
    {
        Act = action;
        Value = value;
    }
}
