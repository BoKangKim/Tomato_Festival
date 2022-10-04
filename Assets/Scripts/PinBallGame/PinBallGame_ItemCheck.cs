using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class PinBallGame_ItemCheck : MonoBehaviourPun
{
    List<string> ItemCheck = new List<string>();

    [SerializeField] Sprite[] guns = new Sprite[4];
    Dictionary<string, Sprite> gunslist;

    [SerializeField] TextMeshProUGUI Item_Bullet = null;
    int BulletNum;
    [SerializeField] TextMeshProUGUI Item_Grenade = null;
    int GrenadeNum;
    [SerializeField] TextMeshProUGUI Item_Shield = null;
    int ShieldNum;
    [SerializeField] Image gunImg = null;

    void Start()
    {
        gunslist = new Dictionary<string, Sprite>();
        gunslist.Add("Handgun", guns[0]);
        gunslist.Add("Repeater", guns[1]);
        gunslist.Add("Shotgun", guns[2]);
        gunslist.Add("SniperRifle", guns[3]);
        gunImg.sprite = gunslist["Handgun"];

        BulletNum = 10;
        GrenadeNum = 0;
        ShieldNum = 0;

        Item_Bullet.text = $"x {BulletNum}";
        Item_Grenade.text = $"x {GrenadeNum}";
        Item_Shield.text = $"x {ShieldNum}";
    }
    public void ResetItem()
    {
        gunImg.sprite = gunslist["Handgun"];

        BulletNum = 10;
        GrenadeNum = 0;
        ShieldNum = 0;

        Item_Bullet.text = $"x {BulletNum}";
        Item_Grenade.text = $"x {GrenadeNum}";
        Item_Shield.text = $"x {ShieldNum}";
    }
    public void AddItemCheckList(string destroyBlockData, bool ismine)
    {

        if (PhotonNetwork.IsMasterClient == true && ismine == true)
        {
            AddMyList(destroyBlockData);
        }
        else if (PhotonNetwork.IsMasterClient == false && ismine == false)
        {
            AddMyList(destroyBlockData);
        }
    }

    void AddMyList(string destroyBlockData)
    {
        ItemCheck.Add(destroyBlockData);

        if (destroyBlockData.Equals("Handgun")
         || destroyBlockData.Equals("Repeater")
         || destroyBlockData.Equals("Shotgun")
         || destroyBlockData.Equals("SniperRifle"))
        {
            gunImg.sprite = gunslist[destroyBlockData];
        }
        else if (destroyBlockData.Equals("Bullet"))
        {
            Item_Bullet.text = $"x {BulletNum += 5}";
        }
        else if (destroyBlockData.Equals("Grenade"))
        {
            Item_Grenade.text = $"x {++GrenadeNum}";
        }
        else if (destroyBlockData.Equals("Shield"))
        {
            Item_Shield.text = $"x {++ShieldNum}";
        }
    }

    public void UpdateBulletNum()
    {
        Item_Bullet.text = $"x {--BulletNum}";
    }
    public void UpdateGrenadeNum()
    {
        Item_Grenade.text = $"x {--GrenadeNum}";
    }
    public void UpdateShieldNum()
    {
        Item_Shield.text = $"x {--ShieldNum}";
    }

    public List<string> GetMyItemDataList()
    {
        return ItemCheck;
    }

}
