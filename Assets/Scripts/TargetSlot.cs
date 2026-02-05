using UnityEngine;

public class TargetSlot : MonoBehaviour
{
    // 박스가 슬롯에 부착 되었는지 여부(추후에 결승지점 활성화를 위해 사용.)
    public bool IsAttached;

    private void OnTriggerEnter(Collider other)
    {
        // 충돌체의 태그가 PushableBox일때, 슬롯에 부착되지 않았을 경우에만.
        if(other.CompareTag("PushableBox") && !IsAttached)
        {
            // 충돌체의 리지드바디를 가져와서.
            Rigidbody rigid = other.GetComponent<Rigidbody>();

            // 해당 물체의 트랜스폼을 이용해서 위치를 변경해주고,
            other.transform.position = transform.position + Vector3.up / 2;

            // 리지바디의 isKinematic을 켜준다.
            rigid.isKinematic = true;

            // 그리고 박스가 슬롯에 부착되었는지의 여부를 true로 만들어준다.
            IsAttached = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PushableBox"))
        {

        }
    }
}
