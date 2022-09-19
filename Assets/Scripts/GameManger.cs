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
        // Handgun(±ÇÃÑ) - Repeater(¿¬¹ßÃÑ) - Shotgun(¿¬»çÃÑ) - SniperRifle(Àú°ÝÃÑ)
        GunDatas[0] = Resources.Load<ScriptableWeaponData>("Handgun");
        GunDatas[1] = Resources.Load<ScriptableWeaponData>("Repeater");
        GunDatas[2] = Resources.Load<ScriptableWeaponData>("Shotgun");
        GunDatas[3] = Resources.Load<ScriptableWeaponData>("SniperRifle");

        playergun = FindObjectOfType<Gun>();
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

                    break;
                }
            }
            
        }
    }

    
}
