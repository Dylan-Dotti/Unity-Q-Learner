using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EpsilonDisplay : MonoBehaviour
{
    public static EpsilonDisplay Instance { get; private set; }

    public QLearningAgent trackedAgent { get; set; }

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI epsilonText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            slider.onValueChanged.AddListener(OnSliderChanged);
        }
    }

    private void LateUpdate()
    {
        float value = trackedAgent == null ?
            0 : trackedAgent.Epsilon;
        slider.value = value;
        epsilonText.text = value.ToString("F2");
    }

    public void OnSliderChanged(float newValue)
    {
        epsilonText.text = newValue.ToString("F2");
        if (trackedAgent != null)
        {
            trackedAgent.Epsilon = newValue;
        }
    }
}
