using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        MinigameTimer.TimerUpdated += UpdateTimerDisplay;
    }
    void OnDisable()
    {
        MinigameTimer.TimerUpdated -= UpdateTimerDisplay;
    }

    void UpdateTimerDisplay(float currentTime)
    {

        int remainingTime = Mathf.CeilToInt(currentTime);
        _timerText.text = remainingTime.ToString("#0");
    }
}
