using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("1. 이동 관련 설정값")]
    public float moveSpeed; // 이동속도
    public float jumpPower; // 점프력
    public float rotateSpeed; // 회전속도

    //내부 변수
    private Rigidbody rb; // 물리엔진 이동용.
    private Vector2 moveInput;
    private Vector2 lookInput;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // 입력부
    /// <summary>
    /// 마우스 입력을 받는 함수
    /// </summary>
    /// <param name="value">마우스 입력값</param>
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    /// <summary>
    /// 키보드 입력을 받아 이동에 사용하는 함수
    /// </summary>
    /// <param name="value">키보드 입력</param>
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 이동함수
    /// </summary>
    private void Move()
    {
        // 이동 입력이 없을때.
        if(moveInput != Vector2.zero)
        {
            // 보고 있는 방향 확인용
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            // WASD 이동은 Y축 이동 없음.
            forward.y = 0f;
            right.y = 0f;

            //벡터 정규화
            forward.Normalize();
            right.Normalize();

            // 움직일 방향 벡터 = (앞쪽 방향 * W,S키 입력 + 옆쪽방향 * A,D키 입력).정규화;
            Vector3 moveDir = (forward * moveInput.y + right * moveInput.x).normalized;

            // 최종적으로 움직일 벡터 = 움직일 방향 벡터 * 이동속도;
            Vector3 finalVelocity = moveDir * moveSpeed;

            // 최종 속도 = 최종 벡터의 X값,Z값만 가지고 오면 됨.
            rb.linearVelocity = new Vector3(finalVelocity.x,rb.linearVelocity.y,finalVelocity.z);
        }
    }
}
