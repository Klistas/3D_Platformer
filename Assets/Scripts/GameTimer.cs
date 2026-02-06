using UnityEngine;
using TMPro;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    [Header("UI Text")]
    public TextMeshProUGUI TimerText; // 현재 시각을 기록하는 타이머 텍스트
    public TextMeshProUGUI BestTimeText; // 지금까지 로컬에 저장된 기록중 가장 뛰어난 시간 출력
    public TextMeshProUGUI CountdownText; // 카운트 다운을 출력할 텍스트

    [Header("설정값")]
    public string BestTimeKey; // 가장 뛰어난 기록을 저장할 키값
    public float CountdownTime; // 카운트다운을 얼마나 진행할지
    public float CurrentTime; // 현재까지 측정한 값
    public bool IsRunning; // 현재 타이머가 동작하고 있는지
    public bool IsCountdown; // 카운트다운 하고있는지
    public PlayerMovement player; // 카운트다운 중에는 플레이어가 움직이지 않게 하기 위해가져옴.

    private float bestTime; // 이전에 불러온 최고 기록 값

    private void Start()
    {
        LoadBestTime();
    }

    public void StartCountdown()
    {
        StartCoroutine(StartCountdownCoroutine());
    }

    /// <summary>
    /// 카운트다운 로직을 담당하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartCountdownCoroutine()
    {
         // 카운트다운을 시작했으므로 true
        IsCountdown = true;
        player.enabled = false;

        // 우선 카운트다운 텍스트를 활성화
        CountdownText.gameObject.SetActive(true);

        //정해진 카운트다운 시간에 맞춰서 반복
        for(int i = (int)CountdownTime; i > 0; i--)
        {
            // 매초 텍스트 출력
            CountdownText.text = i.ToString();
            //1초대기
            yield return new WaitForSeconds(1f);
        }
        // 마지막 텍스트를 띄우고
        CountdownText.text = "Go!";

        // 1초 후에
        yield return new WaitForSeconds(1f);

        // 카운트 다운 텍스트를 비활성화
        CountdownText.gameObject.SetActive(false);

        // 다시 움직일 수 있도록 함.
        IsCountdown = false;
        player.enabled = true;

        // 타이머를 켜주는 함수
        StartTimer();
    }

    private void Update()
    {
        // IsRunning 이 true일때 타이머를 시작한다.
        if(IsRunning)
        {
            CurrentTime += Time.deltaTime;
            // UI에 업데이트
            UpdateUI();
        }
    }

    /// <summary>
    /// 타이머를 켜주는 함수
    /// </summary>
    public void StartTimer()
    {
        CurrentTime = 0f;
        IsRunning = true;
    }

    /// <summary>
    /// 타이머를 끄고 최고 기록을 기록하는 함수
    /// </summary>
    public void StopTimer()
    {
        IsRunning = false;
        // 최고 기록 기록
        CheckBestTime();
    }

    /// <summary>
    /// 타이머에 현재 시간을 기록해주는 함수.
    /// </summary>
    private void UpdateUI()
    {
        // string(문자열) 값을 반환하는 FormattingTime() 함수를 대입. 매개변수는 현재 계산되고 있는 CurrentTime
        TimerText.text = FormattingTime(CurrentTime);
    }

    /// <summary>
    /// 매개변수로 현재 시간을 받아서 포맷에 맞춰주는 함수.
    /// </summary>
    /// <param name="time">현재 기록된 시간</param>
    /// <returns></returns>
    private string FormattingTime(float time)
    {
        // 매개변수의 시간을 받아서 분 계산
        int minutes = (int)(time / 60f);

        // 매개변수의 시간을 받아서 초 계산
        int seconds = (int)(time % 60f);

        // 밀리초는 소수점 두자리까지만 출력
        int milliseconds = (int)((time * 100f) % 100f);

        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    /// <summary>
    /// 최고 기록을 저장하는 함수
    /// </summary>
    private void SaveBestTime()
    {
        // 만약 최고 기록을 갱신했으면, 현재 시간 = 최고 기록 시간, 아니면 그냥 그대로 bestTime을 저장할거다.
        PlayerPrefs.SetFloat(BestTimeKey, bestTime);
        // 세팅했으면 저장.
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 최고 기록을 불러오는 함수
    /// </summary>
    private void LoadBestTime()
    {
        // 이전 기록을 불러오고, 없으면 0
        bestTime = PlayerPrefs.GetFloat(BestTimeKey, 0f);
        // 이전 기록을 UI에 적용
        BestTimeText.text = "Best : " + FormattingTime(bestTime);
    }

    /// <summary>
    /// 최고 기록인지 판단하는 함수
    /// </summary>
    private void CheckBestTime()
    {
        // 최고 기록이 0이거나(아직 기록이 없음), 혹은 최고 기록을 경신했을때
        if(bestTime == 0f || CurrentTime < bestTime)
        {
            // 최고 기록은 현재 기록
            bestTime = CurrentTime;
            // 최고 기록 텍스트를 변경해주고
            BestTimeText.text = "Best : " + FormattingTime(bestTime);
            // 저장
            SaveBestTime();
        }
    }
}
