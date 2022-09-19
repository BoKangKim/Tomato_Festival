using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 20f; // �÷��̾� �ӵ�
    Vector3 mousePos = Vector3.zero; // ���콺 ������
    Vector3 fireDir = Vector3.zero; // �Ѿ��� �߻�Ǵ� ���� ����
    Vector3 knockBackDir = Vector3.zero; // �Ѿ˿� �°� �˹� �Ǵ� ���� ����
    public bool isJumpKeyInput { get; set; } = false; // W�� �Է��Ͽ�����(����Ű�� ������ ��)
    bool initJump = true; // ���� ��Ƽ� ������ �� �� �ִ� ��Ȳ����
    Camera cam; // ���콺 ��ǥ�� ���� ��ǥ�� ��ȯ�ϱ� ���� ī�޶�
    [SerializeField] Player enemy; // ���� ������ �Ǵ��ϱ� ���� ���� -> �̰Ŵ� �ٲ�� ��(��Ʈ��ũ ���̰� �ٲܿ���)
    Rigidbody2D myRigidbody;

    // ���� ������ ����
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

    // Ű �Է��� ���� ���� ������Ʈ �Լ� 
    // FixedUpdate���� ���� ������ �ʰ� �Է� ó���� �Ǿ �Է��� �ʾ���
    private void Update()
    {
        if (gameObject.name == "Enemy")
            return;

        #region �Ѿ� �߻�
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

        // Ű�Է��� �޾Ƽ� ������ �ؾ��ϴµ� AddForce�� FixedUpdate ���� ó���� �ؾ���
        // �ٸ� ��ǻ�Ϳ����� ������ �ӵ�, �Ÿ��� ������
        // Ű �Է��� �Ǿ��ٴ� ���� �˸��� ���� bool ���� �־ true �̸� ������ false�̸� �ȴ��� ���� �Ǵ��� ��
        // FixedUpdate���� ������ ��
        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumpKeyInput = true;
        }
    }

    // ������ٵ� �������� ó���� �ϱ����� FixedUpdate
    void FixedUpdate()
    {
        if (gameObject.name == "Enemy")
            return;
        

        #region �÷��̾� ������
        // ����Ű�� ���ȴ���, ���� ��Ƽ� ������ �� �� �ִ� �������� �Ǵ��� �� ����
        if (isJumpKeyInput == true && initJump == true)
        {
            myRigidbody.AddForce(Vector2.up * 1800f, ForceMode2D.Force);
            isJumpKeyInput = false;
            initJump = false;
        }

        float xAxis = Input.GetAxis("Horizontal");

        // transform.Translate�� �����̴� �� ��� ����
        // ĳ���� ��鸲 ������ �߻� -> transform���� �̵��ϸ� �̵����ٴ� �����̵��� ������ ������ �߻��ϴ� ����
        // �ذ��ϱ� ���� rigidbody�� position���� �̵� -> �浹 ���� �Ǵ� �� �̵� -> ���, ��鸲 ���� ��ȭ
        // rigidbody�� �޷����� ���� transform���� �����̸� rigidbody�� ��ġ�� ����� �ٽ� �ؾ� �ϱ� ������ �ڽ�Ʈ�� ���� ��ٰ� ��
        // rigidbody�� ���� ���� rigidbody.MovePostion �Ǵ� rigidbody.position���� ó���ϴ� ���� ����
        myRigidbody.position += (xAxis * Vector2.right * moveSpeed * Time.deltaTime);

        #endregion
    }

    // �Ѿ��� �浹 ���� �� �Ҹ�
    // �˹��� ó���ϴ� �ڷ�ƾ ���� 
    public void StartKnockBackCoroutine(Vector3 bulletVec)
    {
        if (IsShieldTime == true)
            return;

        knockBackDir = (transform.position - bulletVec).normalized; // ���� �Ѿ˿� ���� �������� �˹��� ���ϱ� ���� ���� ���⺤��
        StartCoroutine(KnockBack());
    }

    // �˹� �ڷ�ƾ
    IEnumerator KnockBack()
    {
        float knockBackSpeed = 64f;

        // �ð��� �������� ���� �� �и��� �ϱ� ���� �ӵ��� ��� ���־
        // �Ÿ��� �پ�� �� �ֵ��� ��
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
