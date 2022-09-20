using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Gun : MonoBehaviourPun
{
    ScriptableWeaponData playerWeapondata = null;
    Vector3 fireDir = Vector3.zero; // 총알이 발사되는 방향 벡터
    Player player = null;
    Player myEnemy = null;
    public int myNum { get; set; } = 0;
    float numberOfBullet; //기본 제공 총알 개수
    float attackInterval; //연사속도
    bool canshoot;
    

    private void Start()
    {
        if(photonView.IsMine == true)
        {
            player = GetComponentInParent<Player>();
            GameManger.Instance.SetPlayerNum(this);
            GameManger.Instance.SetGunData(this, myNum);
        }
    }

    public void SetGunData(ScriptableWeaponData data) //일단 SniperRifle(저격총) 넘어올거야
    {
        playerWeapondata = data;

        numberOfBullet = playerWeapondata.NumberOfBullet;

        attackInterval = playerWeapondata.AttackInterval;
        
        
        canshoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
            return;

        if (Input.GetMouseButtonDown(0) && canshoot == true) //shoot 가능 조건
        {
            if (myEnemy == null)
            {
                Player[] players = FindObjectsOfType<Player>();
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].photonView.IsMine == false)
                    {
                        myEnemy = players[i];
                        break;
                    }
                }

                if (myEnemy == null)
                    Debug.LogError("myEnemy is null Check Gun.cs");
            }
            StartCoroutine("Shoot");
        }
    }
    IEnumerator Shoot()
    {
        Debug.Log("Shoot코루틴 실행됨");

        canshoot = false;
        numberOfBullet -= 1f;

        TransferFireDir();

        GameObject objectInst = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);
        Bullet bulletInst = objectInst.GetComponent<Bullet>();
        if(bulletInst != null)
        {
            //bulletInst.transform.SetParent(BulletPool.Inst.gameObject.transform, false);
            bulletInst.myEnemy = this.myEnemy;
            bulletInst.MoveDir = fireDir;
            bulletInst.AttackRange = playerWeapondata.AttackRange;
            bulletInst.AttackSpeed = playerWeapondata.AttackSpeed;
            bulletInst.AttackDamage = playerWeapondata.AttackDamage;
        }
        else
        {
            Debug.LogError("Not Found Bullet Check Gun.cs");
        }


        yield return new WaitForSeconds(attackInterval);
        canshoot = true;
    }

    void TransferFireDir()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        fireDir = (mousePos - transform.position).normalized; //발사방향
    }

    public Vector3 GetFireDir()
    {
        TransferFireDir();
        return fireDir;
    }
}
