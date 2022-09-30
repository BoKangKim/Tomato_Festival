using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPun
{
    Vector3 Distance = Vector3.zero;
    Vector3 startPos = Vector3.zero;
    Vector3 moveDir = Vector3.zero;
    float attackRange; //��Ÿ�
    float attackSpeed; //�ӵ�
    float attackDamage; //���ݷ�
    public Vector3 MoveDir { set { moveDir = value; } }
    public float AttackRange { set { attackRange = value; } }
    public float AttackSpeed { set { attackSpeed = value; } }
    public float AttackDamage { set { attackDamage = value; } }
    public PlayerBattle myEnemy { get; set; } = null;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (photonView.IsMine == false)
            return;
        // ���̶� �¾��� �� �Ǵ�
        // ������ �˹� �ڷ�ƾ ���� �޼��� ����
        // �Ѿ� ������
        // ��Ʈ��ũ ó��

        transform.Translate(moveDir * Time.deltaTime * attackSpeed);
        if (Vector3.Distance(startPos, transform.position) >= attackRange)
            PhotonNetwork.Destroy(this.gameObject);

        if (myEnemy == null)
            return;

        if (myEnemy.gameObject.transform.lossyScale.x / 2 + gameObject.transform.lossyScale.x / 2 >=
            Vector3.Distance(myEnemy.gameObject.transform.position, gameObject.transform.position))
        {
            myEnemy.SendMessage("StartKnockBackCoroutine", transform.position, SendMessageOptions.DontRequireReceiver);
            myEnemy.SendMessage("TransferDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
            PhotonNetwork.Destroy(this.gameObject);
        }
        

    }
    
}
