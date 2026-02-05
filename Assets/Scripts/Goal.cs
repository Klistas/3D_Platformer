using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("퍼즐")]
    public TargetSlot[] TargetSlots; // 박스가 장착되었는지 확인하기 위한 변수
    public Material PuzzleFinishMat; // 두 박스가 모두 장착되었으면 변경할 색.

    private MeshRenderer meshRenderer; // 매테리얼을 바꿔주기 위한 렌더러
    private bool isTriggered; // 이미 통과했는지
    private bool canFinished; // 퍼즐을 모두 완료해서 통과할 수 있는지.


    private void Start()
    {
        // 초기화
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // 배열 내부의 첫번째 타겟슬롯의 IsAttached가 true이고, 두번째도 true;
        if (TargetSlots[0].IsAttached && TargetSlots[1].IsAttached) 
        {
            CanGoal();
        }
    }

    // 퍼즐이 완료되었을 때 색을 바꾸고, 통과가 가능하게 만드는 기능.
    private void CanGoal()
    {
        // 이미 완료한 상황이라면 더이상 확인할 필요 없음
        if (canFinished) return;
        // 퍼즐 완료
        canFinished = true;
        // 색을 변경해줌
        meshRenderer.sharedMaterial = PuzzleFinishMat;

    }

    // 통과했을때, 승리 Ui가 출력되게끔 만드는 기능. 
    private void OnTriggerEnter(Collider other)
    {

        // 충돌체가 플레이어고, 아직 통과하기 전이어야 하며, 퍼즐은 모두 완료한 상태여야한다.
        if(other.CompareTag("Player") && !isTriggered && canFinished)
        {
            // 승리 UI 및 사운드 출력
            Debug.Log("승리~!!");
            AudioManager.Instance.PlaySFX("도착");
            isTriggered = true;
        }
    }
}
