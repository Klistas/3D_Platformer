using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("설정값")]
    public float FallingDelay; // 밟은 후 몇초후에 떨어질지
    public float RespawnDelay; // 떨어지고 몇초뒤에 다시 생길지
    public float ShakeIntencity; // 흔들림강도.

    // 내부에서 사용하는 변수들
    private Vector3 originalPosition; // 리스폰할 원래 위치
    private Quaternion originalRotation; // 리스폰할 원래 각도
    private bool isFalling; // 지금 떨어지고 있는지.
    private bool isShaking; // 지금 떨리고있는지.
    private Rigidbody rb; // 물리연산을 위한 리지드바디.

    private void Start()
    {
        //초기화 작업 리지드바디 가져오기
        rb = GetComponent<Rigidbody>();

        // 원래 위치와 회전값 저장
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    /// <summary>
    /// 발판을 밟아야하므로 Trigger 사용 불가.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // 만약, 충돌한 물체의 태그가 Player
        if(collision.gameObject.CompareTag("Player") && !isFalling)
        {
            // 충돌했을때 떨리고 떨어지고 다시 리스폰하는 로직
            StartCoroutine(FallingSequence());
        }
    }

    private void Update()
    {
        if(isShaking)
        {
            // 떨리는 작동을 하도록 로직 작성
            transform.position = originalPosition + Random.insideUnitSphere * ShakeIntencity;
        }
    }

    /// <summary>
    /// 발판이 떨리고, 떨어지고, 다시 생기는 일련의 과정을 구현.
    /// </summary>
    /// <returns></returns>
    IEnumerator FallingSequence()
    {
        // 떨어짐, 떨림 로직 시작
        isFalling = true;
        isShaking = true;

        // 충분히 떨림이 보이도록 시간 딜레이.
        yield return new WaitForSeconds(FallingDelay);

        // 떨림은 멈추고,
        isShaking = false;
        //리지드바디의 isKinematic을 꺼서 중력 동작하게끔.
        rb.isKinematic = false;

        // 리스폰 할 수 있는 시간을 딜레이 해준다.
        yield return new WaitForSeconds(RespawnDelay);

        // 이동, 회전에 대한 속력을 초기화 한다.
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 중력을 받지 않게 다시 isKinematic 체크
        rb.isKinematic = true;

        // 원래 위치로 복귀
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // 다시 로직이 돌아갈 수 있도록 false
        isFalling = false;

    }


}
