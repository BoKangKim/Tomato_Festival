using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    ScriptableWeaponData[] GunDatas = new ScriptableWeaponData[4];
    Gun playergun = null;
    List<string> weapon = null;
    

    private void Awake()
    {
        // Handgun(권총) - Repeater(연발총) - Shotgun(연사총) - SniperRifle(저격총)
        GunDatas[0] = Resources.Load<ScriptableWeaponData>("Handgun");
        Debug.Log($"{GunDatas[0].GetGunName()}");
        GunDatas[1] = Resources.Load<ScriptableWeaponData>("Repeater");
        Debug.Log($"{GunDatas[1].GetGunName()}");
        GunDatas[2] = Resources.Load<ScriptableWeaponData>("Shotgun");
        Debug.Log($"{GunDatas[2].GetGunName()}");
        GunDatas[3] = Resources.Load<ScriptableWeaponData>("SniperRifle");
        Debug.Log($"{GunDatas[3].GetGunName()}");

        playergun = FindObjectOfType<Gun>();
        if(playergun != null)
        {
            Debug.Log("Gun 받았음");
        }
        weapon = new List<string>();
    }

    void Start()
    {
        weapon.Add("SniperRifle");

        for(int i = 0; i < GunDatas.Length; i++)
        {
            for(int j = 0; j < weapon.Count; j++)
            {
                if (weapon[j] == GunDatas[i].GetGunName())
                {
                    playergun.SetGunData(GunDatas[i]);
                    Debug.Log($"플레이어 건은 {GunDatas[i].GetGunName()} 임");

                    break;
                }
            }
            
        }
    }

    
}
