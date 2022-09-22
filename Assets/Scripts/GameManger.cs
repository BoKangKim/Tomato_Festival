using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : Singleton<GameManger>
{
    static int playerCount = 1;
    ScriptableWeaponData[] GunDatas = new ScriptableWeaponData[4];
    Gun playergun = null;
    List<string> player1_Weapon = null;
    List<string> player2_Weapon = null;

    private void Awake()
    {
        // Handgun(����) - Repeater(������) - Shotgun(������) - SniperRifle(������)
        GunDatas[0] = Resources.Load<ScriptableWeaponData>("Handgun");
        GunDatas[1] = Resources.Load<ScriptableWeaponData>("Repeater");
        GunDatas[2] = Resources.Load<ScriptableWeaponData>("Shotgun");
        GunDatas[3] = Resources.Load<ScriptableWeaponData>("SniperRifle");

        player1_Weapon = new List<string>();
        player2_Weapon = new List<string>();
    }

    public void SetPlayerNum(Gun playergun)
    {
        playergun.myNum = playerCount;
        playerCount++;
    }

    public void SetGunData(Gun playergun, int playerNum)
    {
        List<string> weapon = null;

        if(playerNum == 1)
        {
            weapon = player1_Weapon;
        }
        else if(playerNum == 2)
        {
            weapon = player2_Weapon;
        }
        else
        {
            return;
        }

        weapon.Add("SniperRifle");
        
        for (int i = 0; i < GunDatas.Length; i++)
        {
            for (int j = 0; j < weapon.Count; j++)
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
