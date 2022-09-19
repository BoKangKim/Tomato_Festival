using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Player enemy;

    ScriptableWeaponData playerWeapondata = null;
    Vector3 fireDir = Vector3.zero; // 총알이 발사되는 방향 벡터
    
    float numberOfBullet; //기본 제공 총알 개수
    float attackInterval; //연사속도
    bool canshoot;

    public void SetGunData(ScriptableWeaponData data) //일단 SniperRifle(저격총) 넘어올거야
    {
        playerWeapondata = data;
        Debug.Log($"{playerWeapondata.GetGunName()}");

        numberOfBullet = playerWeapondata.NumberOfBullet;
        Debug.Log($"{numberOfBullet}");

        attackInterval = playerWeapondata.AttackInterval;
        Debug.Log($"{attackInterval}");
        
        
        canshoot = true;
        Debug.Log($"{canshoot}");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canshoot == true) //shoot 가능 조건
        {
            StartCoroutine("Shoot");
        }
    }
    IEnumerator Shoot()
    {
        Debug.Log("Shoot코루틴 실행됨");

        canshoot = false;
        Debug.Log($"{canshoot}");
        numberOfBullet -= 1f;
        Debug.Log($"{numberOfBullet}");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        fireDir = (mousePos - transform.position).normalized; //발사방향

        Bullet bulletInst = BulletPool.Inst.Get();
        if(bulletInst != null)
        {
            Debug.Log("불렛 생성은 됨");
        }
        bulletInst.transform.position = transform.position;
        bulletInst.MoveDir = fireDir;
        bulletInst.Enemy = this.enemy;
        bulletInst.AttackRange = playerWeapondata.AttackRange;
        //Debug.Log($"{playerWeapondata.AttackRange}");
        bulletInst.AttackSpeed = playerWeapondata.AttackSpeed;
        Debug.Log($"{playerWeapondata.AttackSpeed}");

        bulletInst.AttackDamage = playerWeapondata.AttackDamage;
        


        yield return new WaitForSeconds(attackInterval);
        canshoot = true;
    }

    
}
