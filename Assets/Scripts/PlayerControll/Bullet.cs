using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 TotalDistance = Vector3.zero;  //사거리체크
    Vector3 Distance = Vector3.zero;

    Vector3 moveDir = Vector3.zero;
    Player enemy;
    float attackRange; //사거리
    float attackSpeed; //속도
    float attackDamage; //공격력
    public Vector3 MoveDir { set { moveDir = value; } }
    public Player Enemy { set { enemy = value; } }
    public float AttackRange { set { attackRange = value; } }
    public float AttackSpeed { set { attackSpeed = value; } }
    public float AttackDamage { set { attackDamage = value; } }
    CamEffect camEffect = null;

    private void Awake()
    {
        //gameObject.transform.SetParent(BulletPool.Inst.gameObject.transform, false);
    }

    private void Start()
    {
        camEffect = Camera.main.GetComponent<CamEffect>();
    }

    void Update()
    {
        // 적이랑 맞았을 때 판단
        // 맞으면 넉백 코루틴 시작 메세지 보냄
        // 총알 릴리즈
        if(enemy.gameObject.transform.lossyScale.x / 2 + gameObject.transform.lossyScale.x /2 >=
            Vector3.Distance(enemy.gameObject.transform.position, gameObject.transform.position))
        {
            enemy.SendMessage("StartKnockBackCoroutine",transform.position,SendMessageOptions.DontRequireReceiver);
            enemy.SendMessage("TransferDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
            camEffect.StartCamEffectCoroutine();
            BulletPool.Inst.Release(this);
        }


        TotalDistance = Vector3.zero;

        Distance = moveDir * Time.deltaTime * attackSpeed;
        transform.Translate(Distance);

        TotalDistance += Distance;
        if ((TotalDistance).magnitude >= attackRange)
        {
            //BulletPool.Inst.Release(this);
        }

        // 나중에 총의 사거리의 따라 다르겠지만 일단 임시방편으로 일정 거리 이상 가면 릴리즈
        if (transform.position.x >= 36f
            || transform.position.y >= 26f
            || transform.position.x <= -36f
            || transform.position.y <=-26f)
        {
            BulletPool.Inst.Release(this);
        }

        

    }
    
}
