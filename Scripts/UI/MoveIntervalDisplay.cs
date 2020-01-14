using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveIntervalDisplay : MonoBehaviour
{
    public static MoveIntervalDisplay Instance { get; private set; }

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI moveIntervalText;

    private Timer timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timer = Timer.Instance;
        slider.onValueChanged.AddListener(OnIntervalChanged);
        slider.value = Mathf.Clamp01(timer.TimeBetweenEvents);
    }

    public void OnIntervalChanged(float newValue)
    {
        timer.TimeBetweenEvents = newValue;
        moveIntervalText.text = newValue.ToString("F2");
    }
}
