using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 TotalDistance = Vector3.zero;  //��Ÿ�üũ
    Vector3 Distance = Vector3.zero;

    Vector3 moveDir = Vector3.zero;
    Player enemy;
    float attackRange; //��Ÿ�
    float attackSpeed; //�ӵ�
    float attackDamage; //���ݷ�
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
        // ���̶� �¾��� �� �Ǵ�
        // ������ �˹� �ڷ�ƾ ���� �޼��� ����
        // �Ѿ� ������
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

        // ���߿� ���� ��Ÿ��� ���� �ٸ������� �ϴ� �ӽù������� ���� �Ÿ� �̻� ���� ������
        if (transform.position.x >= 36f
            || transform.position.y >= 26f
            || transform.position.x <= -36f
            || transform.position.y <=-26f)
        {
            BulletPool.Inst.Release(this);
        }

        

    }
    
}
