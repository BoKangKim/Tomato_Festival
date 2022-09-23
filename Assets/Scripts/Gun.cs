using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using System.Runtime.InteropServices;
public class Gun : MonoBehaviourPun
{
    [DllImport("user32.dll")]
    public static extern int SetCursorPos(int X, int Y);

    ScriptableWeaponData playerWeapondata = null;
    Vector3 fireDir = Vector3.zero; // 총알이 발사되는 방향 벡터
    Player player = null;
    Player myEnemy = null;
    public float numberOfBullet { get; set; } = 0;
    bool canshoot = true;
    bool isSniperMode = false;
    public void SetGunData(ScriptableWeaponData data)
    {
        if (photonView.IsMine == false)
            return;

        playerWeapondata = data;
        numberOfBullet += data.NumberOfBullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerWeapondata == null)
            return;

        if (photonView.IsMine == false || numberOfBullet == 0)
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

            StartCoroutine("Shoot_" + playerWeapondata.GunName);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            isSniperMode = true;
        }

        if (isSniperMode)
        {
            Vector3 target = Camera.main.WorldToScreenPoint(myEnemy.transform.position);
        }
    }

    void InitializingBullet(bool isShotGun,int count)
    {
        canshoot = false;
        numberOfBullet -= 1f;

        TransferFireDir();

        if(isShotGun == true && count == -1) 
        {
            float radian = Mathf.Atan2(fireDir.y,fireDir.x);
            radian += (3.14f / 12f);
            fireDir = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f);
        }
        else if(isShotGun == true && count == 1)
        {
            float radian = Mathf.Atan2(fireDir.y, fireDir.x);
            radian -= (3.14f / 12f);
            fireDir = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f);
        }

        GameObject objectInst = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);
        Bullet bulletInst = objectInst.GetComponent<Bullet>();
        if (bulletInst != null)
        {
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
        
    }

    // Handgun
    IEnumerator Shoot_Handgun()
    {
        InitializingBullet(false, 0);
        yield return new WaitForSeconds(playerWeapondata.AttackInterval);
        canshoot = true;
    }

    // Repeater
    IEnumerator Shoot_Repeater()
    {
        for(int i = 0; i < 3; i++)
        {
            InitializingBullet(false, 0);
            yield return new WaitForSeconds(playerWeapondata.AttackInterval);
        }
        
        yield return new WaitForSeconds(1f);
        canshoot = true;
    }
    
    // Shotgun
    IEnumerator Shoot_Shotgun()
    {
        InitializingBullet(true, -1);
        InitializingBullet(true, 0);
        InitializingBullet(true, 1);

        yield return new WaitForSeconds(playerWeapondata.AttackInterval);
        canshoot = true;
    }
    
    // SniperRifle
    IEnumerator Shoot_SniperRifle()
    {
        InitializingBullet(false, 0);
        yield return new WaitForSeconds(playerWeapondata.AttackInterval);
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
