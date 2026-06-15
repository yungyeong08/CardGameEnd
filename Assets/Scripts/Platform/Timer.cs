using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeRemaining = 60f; // 1분
    private bool isTimerRunning = true;

    void Start()
    {
        timeRemaining = 60f;
        isTimerRunning = true;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isTimerRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerUI();
            isTimerRunning = false;

            // ─── [수정] 1분이 지나면 GameClear가 아니라 GameOver를 호출! ───
            GameController.Instance.GameOver();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}