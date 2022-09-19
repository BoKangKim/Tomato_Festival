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
        // ���̶� �¾��� �� �Ǵ�
        // ������ �˹� �ڷ�ƾ ���� �޼��� ����
        // �Ѿ� ������
        if(enemy.gameObject.transform.lossyScale.x / 2 + gameObject.transform.lossyScale.x /2 >=
            Vector3.Distance(enemy.gameObject.transform.position, gameObject.transform.position))
        {
            enemy.SendMessage("StartKnockBackCoroutine",transform.position,SendMessageOptions.DontRequireReceiver);
            BulletPool.Inst.Release(this);
        }

        // ���߿� ���� ��Ÿ��� ���� �ٸ������� �ϴ� �ӽù������� ���� �Ÿ� �̻� ���� ������
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
