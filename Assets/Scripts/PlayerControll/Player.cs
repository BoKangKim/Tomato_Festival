using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviourPun
{
    float playermaxHP;
    float playercurHP;
    float moveSpeed = 20f; // �÷��̾� �ӵ�
    Vector3 mousePos = Vector3.zero; // ���콺 ������
    Vector2 knockBackDir = Vector2.zero; // �Ѿ˿� �°� �˹� �Ǵ� ���� ����
    public bool isJumpKeyInput { get; set; } = false; // W�� �Է��Ͽ�����(����Ű�� ������ ��)
    bool initJump = true; // ���� ��Ƽ� ������ �� �� �ִ� ��Ȳ����
    Rigidbody2D myRigidbody;
    CamEffect camEffect = null;
    

    // ���� ������ ����
    Items myItems = null;
    public int MyItemIndex { get; set; } = 0;
    public bool IsShieldTime { get; set; } = false;
    public int bulletCount { get; set; } = 0;

    private void Awake()
    {
        myItems = GetComponent<Items>();

        if (photonView.IsMine == false)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }
        else
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }

        camEffect = FindObjectOfType<CamEffect>();
        playermaxHP = 100f;
        playercurHP = 100f;
    }

    // Ű �Է��� ���� ���� ������Ʈ �Լ� 
    // FixedUpdate���� ���� ������ �ʰ� �Է� ó���� �Ǿ �Է��� �ʾ���
    private void Update()
    {
        // ��Ʈ��ũ ó��
        if (photonView.IsMine == false)
            return;

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
        if (myRigidbody == null)
            return;
        // ��Ʈ��ũ ó��
        if (photonView.IsMine == false)
            return;

        #region �÷��̾� ������
        // ����Ű�� ���ȴ���, ���� ��Ƽ� ������ �� �� �ִ� �������� �Ǵ��� �� ����
        if (isJumpKeyInput == true && initJump == true)
        {
            Jump();
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
    

    private void Jump()
    {
        
        myRigidbody.AddForce(Vector2.up * 1800f, ForceMode2D.Force);
        isJumpKeyInput = false;
        initJump = false;
    }

    private void SetininJump(bool initJump)
    {
        this.initJump = initJump;
    }

    private void SetisJump(bool isJump)
    {
        this.isJumpKeyInput = isJump;
    }

    void TransferDamage(float attackDamage)
    {
       
        photonView.RPC("RPC_TransferDamage", RpcTarget.Others, attackDamage);
    }
    public void StartKnockBackCoroutine(Vector3 bulletVec)
    {
        photonView.RPC("RPC_StartKnockBackCoroutine", RpcTarget.Others, bulletVec);
    }

    [PunRPC]
    void RPC_StartKnockBackCoroutine(Vector3 bulletVec)
    {
        if (IsShieldTime == true)
            return;

        knockBackDir = (transform.position - bulletVec).normalized; // ���� �Ѿ˿� ���� �������� �˹��� ���ϱ� ���� ���� ���⺤��
        StartCoroutine(KnockBack());
    }
    [PunRPC]
    void RPC_TransferDamage(float attackDamage)
    {
        camEffect.StartCamEffectCoroutine();
        playercurHP -= attackDamage;
    }

    // �˹� �ڷ�ƾ
    IEnumerator KnockBack()
    {
        float knockBackSpeed = 64f;

        // �ð��� �������� ���� �� �и��� �ϱ� ���� �ӵ��� ��� ���־
        // �Ÿ��� �پ�� �� �ֵ��� ��
        if(myRigidbody != null)
        {
            while (knockBackSpeed >= 1f)
            {
                knockBackSpeed -= Time.deltaTime * 400f;
                myRigidbody.position += (knockBackDir * Time.deltaTime * knockBackSpeed);

                yield return null;
            }
        }
        
    }

}
