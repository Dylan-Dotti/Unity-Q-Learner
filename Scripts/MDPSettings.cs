using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDPSettings : MonoBehaviour
{
    public static MDPSettings Instance { get; private set; }

    [SerializeField] [Range(0, 1)] private float discountFactor = 0.9f;
    [SerializeField] [Range(0, 1)] private float learningRate = 0.9f;

    public float DiscountFactor
    {
        get => discountFactor;
        set => discountFactor = Mathf.Clamp01(value);
    }

    public float LearningRate
    {
        get => learningRate;
        set => learningRate = Mathf.Clamp01(value);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
