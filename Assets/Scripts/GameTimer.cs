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

    private float bestTime; // 이전에 불러온 최고 기록 값

    private void Start()
    {
        StartCoroutine(StartCountdownCoroutine());
    }


    // 카운트다운을 해주는 함수가 필요.
    /// <summary>
    /// 카운트다운 로직을 담당하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartCountdownCoroutine()
    {
        // 카운트다운을 시작했으므로 true
        IsCountdown = true;

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


    }
    // 어떤 함수(기능)를 만들어야할까
    // 타이머의 시간을 계산하고, Text에 표기할 함수
    // 타이머를 시작해주는 함수
    // 타이머를 멈춰주는함수


    // 최고기록을 불러오고 비교하고 저장하는 함수.
}
