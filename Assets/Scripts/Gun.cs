using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class Gun : MonoBehaviourPun
{
    ScriptableWeaponData playerWeapondata = null;
    Vector3 fireDir = Vector3.zero;
    PlayerBattle player = null;
    PlayerBattle myEnemy = null;
    PinBallGame_ItemCheck _itemCheck;

    float attRange = 2.5f;
    float distace;

    public float numberOfBullet { get; set; }
    bool canshoot = true;

    private void Awake()
    {
        _itemCheck = FindObjectOfType<PinBallGame_ItemCheck>();
    }
    public void SetGunData(ScriptableWeaponData data) //gundata 활성화될때마다 splitdata에서 데이터 받아서 호출.
    {
        if (photonView.IsMine == false)
            return;

        playerWeapondata = data;
        //numberOfBullet += data.NumberOfBullet;

    }

    private void OnEnable()
    {
        canshoot = true;
    }
    private void Start()
    {
        numberOfBullet = 0;
        _itemCheck.UpdateBulletNum(numberOfBullet);
    }
    // Update is called once per frame
    void Update()
    {

        if (photonView.IsMine == false)
            return;

        if (Input.GetMouseButtonDown(0) && canshoot == true) //shoot ���� ����
        {


            FindEnemy();


            if (numberOfBullet <= 0)
            {
                PhotonNetwork.Instantiate("HitPow", transform.position, Quaternion.identity);
                if (distace < attRange)
                {
                    Debug.Log("근접공격");
                    myEnemy.SendMessage("StartKnockBackCoroutine", transform.position, SendMessageOptions.DontRequireReceiver);
                    myEnemy.SendMessage("TransferDamage", 10f, SendMessageOptions.DontRequireReceiver);



                }
            }
            else
            {
                StartCoroutine("Shoot_" + playerWeapondata.GunName);
            }
        }

    }

    void FindEnemy()
    {
        if (myEnemy == null || player == null)
        {
            PlayerBattle[] players = FindObjectsOfType<PlayerBattle>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].photonView.IsMine == false)
                {
                    myEnemy = players[i];
                }
                if (players[i].photonView.IsMine == true)
                {
                    player = players[i];
                }
            }

            if (myEnemy == null)
                Debug.LogError("myEnemy is null Check Gun.cs");
        }

        if (myEnemy != null && player != null)
        {
            distace = Vector3.Distance(player.GetComponentInChildren<Gun>().transform.position, myEnemy.GetComponentInChildren<Gun>().transform.position);

        }
    }

    void InitializingBullet(bool isShotGun, int count)
    {
        canshoot = false;
        numberOfBullet -= 1f;

        TransferFireDir();

        if (isShotGun == true && count == -1)
        {
            float radian = Mathf.Atan2(fireDir.y, fireDir.x);
            radian += (3.14f / 12f);
            fireDir = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f);
        }
        else if (isShotGun == true && count == 1)
        {
            float radian = Mathf.Atan2(fireDir.y, fireDir.x);
            radian -= (3.14f / 12f);
            fireDir = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f);
        }

        GameObject objectInst = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);
        Bullet bulletInst = objectInst.GetComponent<Bullet>();
        _itemCheck.UpdateBulletNum(numberOfBullet);
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
        for (int i = 0; i < 3; i++)
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
        fireDir = (mousePos - transform.position).normalized; //�߻����
    }

    public Vector3 GetFireDir()
    {
        TransferFireDir();
        return fireDir;
    }
}
