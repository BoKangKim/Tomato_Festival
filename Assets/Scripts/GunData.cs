using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class GunData : MonoBehaviourPun
{
    string DefaultGunName = "Handgun";
    SpriteRenderer gunSprite;
    ScriptableWeaponData[] GunDatas;
    string playerWeapon = null;

    private void Awake()
    {
        GunDatas = new ScriptableWeaponData[4];
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < sprites.Length; i++)
        {
            if(sprites[i].gameObject.name == "Gun")
            {
                gunSprite = sprites[i];
            }
        }
        GunDatas[0] = Resources.Load<ScriptableWeaponData>("Handgun");
        GunDatas[1] = Resources.Load<ScriptableWeaponData>("Repeater");
        GunDatas[2] = Resources.Load<ScriptableWeaponData>("Shotgun");
        GunDatas[3] = Resources.Load<ScriptableWeaponData>("SniperRifle");
    }

    private void OnEnable()
    {
        List<string> guns = new List<string>();
        SplitData splitData = FindObjectOfType<SplitData>();

        if (splitData != null && photonView.IsMine)
            guns = splitData.GetAndSplitData("Gun");

        if(guns.Count == 0)
        {
            playerWeapon = DefaultGunName;
        }
        else
        {
            playerWeapon = guns[guns.Count - 1];
        }

        for (int i = 0; i < GunDatas.Length; i++)
        {
            if (GunDatas[i].GunName.Equals(playerWeapon))
            {
                gunSprite.sprite = GunDatas[i].GunImg;
                photonView.RPC("RPC_SetGunSprite",RpcTarget.Others, playerWeapon);
                break;
            }
        }
    }

    [PunRPC]
    void RPC_SetGunSprite(string gun)
    {
        for (int i = 0; i < GunDatas.Length; i++)
        {
            if (GunDatas[i].GunName.Equals(gun))
            {
                gunSprite.sprite = GunDatas[i].GunImg;
                break;
            }
        }
    }
    
}
