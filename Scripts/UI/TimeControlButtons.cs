using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeControlButtons : MonoBehaviour
{
    private Button startButton;
    private Button resetButton;

    private void Awake()
    {
        startButton = transform.Find("Start Button").GetComponent<Button>();
        resetButton = transform.Find("Reset Button").GetComponent<Button>();
        startButton.onClick.AddListener(OnStartButtonPressed);
        resetButton.onClick.AddListener(OnResetButtonPressed);
    }

    public void OnStartButtonPressed()
    {
        startButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(true);
        Timer.Instance.enabled = true;
    }

    public void OnResetButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
