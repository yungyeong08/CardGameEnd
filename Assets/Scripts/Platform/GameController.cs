using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject clear;    // 골인 지점에 들어갔을 때 뜰 UI
    public GameObject gameOver; // 시간 초과로 죽었을 때 뜰 UI (추시)

    public TextMeshProUGUI text; // 점수 표시용 텍스트
    public float Score = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Score = 0;
        UpdateUI();

        // 시작할 때 두 UI 패널 모두 확실하게 꺼두기
        if (clear != null) clear.SetActive(false);
        if (gameOver != null) gameOver.SetActive(false);
    }

    public void AddScore(float value)
    {
        Score += value;
        UpdateUI();
    }

    // [성공] 골인 지점에 안전하게 들어갔을 때 호출
    public void GameClear()
    {
        clear.SetActive(true); // 클리어 UI만 켜기
        Time.timeScale = 0f;
        Application.Quit();// 게임 정지
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // [실패] ─── 타이머가 끝나서 실패(죽음)했을 때 호출 ───
    public void GameOver()
    {
        gameOver.SetActive(true); // 게임 오버 UI만 켜기
        Time.timeScale = 0f;      // 게임 정지
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void UpdateUI()
    {
        text.text = Score.ToString();
    }
}