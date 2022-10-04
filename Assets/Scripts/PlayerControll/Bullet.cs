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
    float attackRange; //사거리
    float attackSpeed; //속도
    float attackDamage; //공격력
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
        // 적이랑 맞았을 때 판단
        // 맞으면 넉백 코루틴 시작 메세지 보냄
        // 총알 릴리즈
        // 네트워크 처리

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
