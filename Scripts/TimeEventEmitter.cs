using UnityEngine;
using UnityEngine.Events;

public class TimeEventEmitter : MonoBehaviour
{
    public static TimeEventEmitter Instance { get; private set; }

    public UnityAction PeriodicTimeEvent;

    public float TimeBetweenEvents
    {
        get => timeBetweenEvents;
        set => timeBetweenEvents = value;
    }

    [SerializeField] private float timeBetweenEvents = 0.5f;
    private float timeSinceLastEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        timeSinceLastEvent += Time.deltaTime;
        if (timeSinceLastEvent >= TimeBetweenEvents)
        {
            PeriodicTimeEvent?.Invoke();
            timeSinceLastEvent = 0;
        }
    }

    public void ResetTimer()
    {
        timeSinceLastEvent = 0;
    }
}
