using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : Singleton<GameManger>
{
    static int playerCount = 1;
    ScriptableWeaponData[] GunDatas = new ScriptableWeaponData[4];
    Gun playergun = null;
    List<string> player_Weapon = null;

    private void Awake()
    {
        // Handgun(±ÇÃÑ) - Repeater(¿¬¹ßÃÑ) - Shotgun(¿¬»çÃÑ) - SniperRifle(Àú°ÝÃÑ)
        GunDatas[0] = Resources.Load<ScriptableWeaponData>("Handgun");
        GunDatas[1] = Resources.Load<ScriptableWeaponData>("Repeater");
        GunDatas[2] = Resources.Load<ScriptableWeaponData>("Shotgun");
        GunDatas[3] = Resources.Load<ScriptableWeaponData>("SniperRifle");

        player_Weapon = new List<string>();
    }

    public void SetPlayerNum(Gun playergun)
    {
        playergun.myNum = playerCount;
        playerCount++;
    }

    public void SetGunData(Gun playergun, int playerNum)
    {
        List<string> weapon = null;

        weapon = player_Weapon;
        weapon.Add("SniperRifle");
        
        for (int i = 0; i < GunDatas.Length; i++)
        {
            for (int j = 0; j < weapon.Count; j++)
            {
                if (weapon[j] == GunDatas[i].GunName)
                {
                    playergun.SetGunData(GunDatas[i]);

                    break;
                }
            }

        }
    }
    
}
