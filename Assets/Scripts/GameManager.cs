using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameTimer timer;
    /// <summary>
    /// 싱글톤 패턴 적용
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // 초기화
        timer = GetComponent<GameTimer>();
        GameStart();
    }

    /// <summary>
    /// 게임 시작 - 카운트다운을 시작해줄것.
    /// </summary>
    public void GameStart()
    {
        timer.StartCountdown();
    }

    /// <summary>
    /// 게임 종료 - 타이머 종료.
    /// </summary>
    public void GameFinish()
    {
        // 타이머를 종료하고 기록을 남김.
        timer.StopTimer();
        Time.timeScale = 0;
    }
}
