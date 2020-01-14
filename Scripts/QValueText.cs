using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QValueText : MonoBehaviour
{
    [SerializeField] private Action syncedAction = Action.None;

    private GridSquare syncedSquare;
    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        syncedSquare = GetComponentInParent<GridSquare>();
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        //if (syncedSquare.QValues.ContainsKey(syncedAction))
        //{
        //    textComponent.text = syncedSquare.QValues[syncedAction].ToString("F2");
        //}
    }
}
