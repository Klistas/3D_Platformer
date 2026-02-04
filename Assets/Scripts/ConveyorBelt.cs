using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("설정값")]
    public float VelocityPower; // 얼마나 큰 힘으로 밀어줄지
    public Vector3 VelocityDirection; // 힘이 가해지는 방향

    private void OnTriggerStay(Collider other)
    {
        // 대상이 플레이어 일때
        if (other.CompareTag("Player"))
        {
            // 플레이어 가져와서
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            // 가져오기 성공했다면
            if (player != null)
            {
                // 플레이어가 가지고 있는 힘을 더해주는 함수에 해당 오브젝트의 회전값을 반영한 실제 월드 방향을 가져와서 정규화. 여기에 힘을 곱해줌.
                player.AddExternalVelocity(transform.TransformDirection(VelocityDirection.normalized) * VelocityPower);
            }
        }
    }
}
