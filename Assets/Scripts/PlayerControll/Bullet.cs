using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 moveDir = Vector3.zero;
    Player enemy;

    public Vector3 MoveDir 
    {
        set
        {
            moveDir = value;
        }
    }

    public Player Enemy
    {
        set
        {
            enemy = value;
        }
    }

    private void Awake()
    {
        gameObject.transform.SetParent(BulletPool.Inst.gameObject.transform,false);
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
            BulletPool.Inst.Release(this);
        }

        // 나중에 총의 사거리의 따라 다르겠지만 일단 임시방편으로 일정 거리 이상 가면 릴리즈
        if(transform.position.x >= 36f
            || transform.position.y >= 26f
            || transform.position.x <= -36f
            || transform.position.y <=-26f)
        {
            BulletPool.Inst.Release(this);
        }
        transform.Translate(moveDir * Time.deltaTime * 30f);
    }
    
}
