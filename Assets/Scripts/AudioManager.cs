using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 싱글톤패턴 == GameManager, 씬 안에서 유일하게 존재하는 객체. 전역적(어디에서나 가져올 수 있음)인 접근점이 제공된다. 씬이동간에 사라지지않는다.
    public static AudioManager Instance;

    // 오디오소스
    [Header("Audio Source")]
    public AudioSource BGMSource; // 배경음악
    public AudioSource SFXSource; // 효과음

    // 상황에 맞는 소리 파일(오디오 클립)
    [Header("소리 파일")]
    public AudioClip JumpSound; // 점프 사운드
    public AudioClip JumpPadSound;// 점프패드 사운드
    public AudioClip BombSound;// 폭탄 사운드
    public AudioClip PuzzleSound;// 퍼즐 사운드
    public AudioClip GoalSound;// 결승점 사운드
    public AudioClip BGMSound;// 배경음악

    // 볼륨
    [Header("볼륨 조절")]
    [Range(0f, 1f)] public float BGMVolume;
    [Range(0f, 1f)] public float SFXVolume;

    // 각각 상황에 맞는 소리를 가지고 올 수 있게 하는 인덱스
    private Dictionary<string, AudioClip> sfxDic;

    /// <summary>
    /// 싱글톤 패턴 적용
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
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
        // 배경음악 CD 변경 후 플레이
        BGMSource.clip = BGMSound;
        BGMSource.loop = true;
        BGMSource.Play();

        // 인덱스 생성
        sfxDic = new Dictionary<string, AudioClip>
        {
            { "점프",JumpSound},
            { "점프패드",JumpPadSound },
            { "폭탄",BombSound },
            { "퍼즐",PuzzleSound},
            { "도착",GoalSound},
        };
    }
    private void Update()
    {
        BGMSource.volume = BGMVolume;
    }

    /// <summary>
    /// 바로 가져와서 사운드를 플레이함.
    /// </summary>
    /// <param name="name">플레이 할 사운드의 이름</param>
    public void PlaySFX(string name)
    {
        if(sfxDic.ContainsKey(name))
        {
             SFXSource.PlayOneShot(sfxDic[name],SFXVolume);
        }
        else
        {
            Debug.LogError("해당 사운드 없음");
        }
    }
}
