using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 20f; // 플레이어 속도
    Vector3 mousePos = Vector3.zero; // 마우스 포지션
    Vector3 fireDir = Vector3.zero; // 총알이 발사되는 방향 벡터
    Vector3 knockBackDir = Vector3.zero; // 총알에 맞고 넉백 되는 방향 벡터
    public bool isJumpKeyInput { get; set; } = false; // W를 입력하였는지(점프키를 눌렀는 지)
    bool initJump = true; // 땅에 닿아서 점프를 할 수 있는 상황인지
    Camera cam; // 마우스 좌표를 월드 좌표로 변환하기 위한 카메라
    [SerializeField] Player enemy; // 누가 적인지 판단하기 위한 변수 -> 이거는 바꿔야 함(네트워크 붙이고 바꿀예정)
    Rigidbody2D myRigidbody;

    // 현재 아이템 정보
    Items myItems = null;
    public int MyItemIndex { get; set; } = 0;
    public bool IsShieldTime { get; set; } = false;
    public int bulletCount { get; set; } = 0;

    private void Awake()
    {
        myItems = GetComponent<Items>();
        myRigidbody = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>();
    }

    // 키 입력을 위해 돌린 업데이트 함수 
    // FixedUpdate에서 같이 받으면 늦게 입력 처리가 되어서 입력이 늦어짐
    private void Update()
    {
        if (gameObject.name == "Enemy")
            return;

        #region 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            TransferMousePositionToWorold();
            Bullet bulletInst = BulletPool.Inst.Get();
            bulletInst.transform.position = transform.position;
            bulletInst.MoveDir = fireDir;
            bulletInst.Enemy = this.enemy;
        }
        if (Input.GetMouseButtonDown(1))
        {
            myItems.StartItemCoroutine();
        }

        #endregion

        // 키입력을 받아서 점프를 해야하는데 AddForce는 FixedUpdate 에서 처리를 해야지
        // 다른 컴퓨터에서도 동일한 속도, 거리로 움직임
        // 키 입력이 되었다는 것을 알리기 위해 bool 값을 넣어서 true 이면 눌린거 false이면 안눌린 것을 판단한 후
        // FixedUpdate에서 점프를 함
        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumpKeyInput = true;
        }
    }

    // 리지드바디 물리적인 처리를 하기위한 FixedUpdate
    void FixedUpdate()
    {
        if (gameObject.name == "Enemy")
            return;
        

        #region 플레이어 움직임
        // 점프키가 눌렸는지, 땅에 닿아서 점프를 할 수 있는 상태인지 판단한 후 점프
        if (isJumpKeyInput == true && initJump == true)
        {
            myRigidbody.AddForce(Vector2.up * 1800f, ForceMode2D.Force);
            isJumpKeyInput = false;
            initJump = false;
        }

        float xAxis = Input.GetAxis("Horizontal");

        // transform.Translate로 움직이니 벽 통과 현상
        // 캐릭터 흔들림 현상이 발생 -> transform으로 이동하면 이동보다는 순간이동에 가깝기 때문에 발생하는 현상
        // 해결하기 위해 rigidbody의 position으로 이동 -> 충돌 먼저 판단 후 이동 -> 통과, 흔들림 현상 완화
        // rigidbody가 달려있을 때는 transform으로 움직이면 rigidbody의 위치도 계산을 다시 해야 하기 때문에 코스트가 많이 든다고 함
        // rigidbody가 있을 때는 rigidbody.MovePostion 또는 rigidbody.position으로 처리하는 것이 좋음
        myRigidbody.position += (xAxis * Vector2.right * moveSpeed * Time.deltaTime);

        #endregion
    }

    // 총알이 충돌 했을 때 불림
    // 넉백을 처리하는 코루틴 실행 
    public void StartKnockBackCoroutine(Vector3 bulletVec)
    {
        if (IsShieldTime == true)
            return;

        knockBackDir = (transform.position - bulletVec).normalized; // 적이 총알에 맞은 방향으로 넉백을 당하기 위해 구한 방향벡터
        StartCoroutine(KnockBack());
    }

    // 넉백 코루틴
    IEnumerator KnockBack()
    {
        float knockBackSpeed = 64f;

        // 시간이 지날수록 점점 덜 밀리게 하기 위해 속도를 계속 빼주어서
        // 거리가 줄어들 수 있도록 함
        while (knockBackSpeed >= 1f)
        {
            knockBackSpeed -= Time.deltaTime * 400f;
            transform.Translate(knockBackDir * Time.deltaTime * knockBackSpeed);
            yield return null;
        }
    }

    private void TransferMousePositionToWorold()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mousePos - new Vector3(0f, 0f, mousePos.z);
        fireDir = (mousePos - transform.position).normalized;
    }

    public Vector3 GetFireDir()
    {
        TransferMousePositionToWorold();
        return fireDir;
    }

    public Player GetTarget()
    {
        return enemy;
    }

    private void SetininJump(bool initJump)
    {
        this.initJump = initJump;
    }

    private void SetisJump(bool isJump)
    {
        this.isJumpKeyInput = isJump;
    }

}
