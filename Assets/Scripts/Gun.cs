using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    ScriptableWeaponData playerWeapondata = null;
    Vector3 fireDir = Vector3.zero; // �Ѿ��� �߻�Ǵ� ���� ����
    
    float numberOfBullet; //�⺻ ���� �Ѿ� ����
    float attackInterval; //����ӵ�
    bool canshoot;

    public void SetGunData(ScriptableWeaponData data) //�ϴ� SniperRifle(������) �Ѿ�ðž�
    {
        playerWeapondata = data;

        numberOfBullet = playerWeapondata.NumberOfBullet;

        attackInterval = playerWeapondata.AttackInterval;
        
        
        canshoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canshoot == true) //shoot ���� ����
        {
            StartCoroutine("Shoot");
        }
    }
    IEnumerator Shoot()
    {
        Debug.Log("Shoot�ڷ�ƾ �����");

        canshoot = false;
        numberOfBullet -= 1f;

        TransferFireDir();

        Bullet bulletInst = BulletPool.Inst.Get();
        
        bulletInst.transform.position = transform.position;
        bulletInst.MoveDir = fireDir;
        //bulletInst.Enemy = this.enemy;
        bulletInst.AttackRange = playerWeapondata.AttackRange;
        bulletInst.AttackSpeed = playerWeapondata.AttackSpeed;

        bulletInst.AttackDamage = playerWeapondata.AttackDamage;
        


        yield return new WaitForSeconds(attackInterval);
        canshoot = true;
    }

    void TransferFireDir()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        fireDir = (mousePos - transform.position).normalized; //�߻����
    }

    public Vector3 GetFireDir()
    {
        TransferFireDir();
        return fireDir;
    }
}
