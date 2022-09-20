using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Gun : MonoBehaviourPun
{
    ScriptableWeaponData playerWeapondata = null;
    Vector3 fireDir = Vector3.zero; // �Ѿ��� �߻�Ǵ� ���� ����
    Player player = null;
    Player myEnemy = null;
    public int myNum { get; set; } = 0;
    float numberOfBullet; //�⺻ ���� �Ѿ� ����
    float attackInterval; //����ӵ�
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
        if (photonView.IsMine == false)
            return;

        if (Input.GetMouseButtonDown(0) && canshoot == true) //shoot ���� ����
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
        Debug.Log("Shoot�ڷ�ƾ �����");

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
        fireDir = (mousePos - transform.position).normalized; //�߻����
    }

    public Vector3 GetFireDir()
    {
        TransferFireDir();
        return fireDir;
    }
}
