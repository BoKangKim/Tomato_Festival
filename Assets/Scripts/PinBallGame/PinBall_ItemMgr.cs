using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinBall_ItemMgr : MonoBehaviour
{

    Scriptable_PinBallBlock blockData;
    [SerializeField] Scriptable_PinBallGetItem[] pinballGetItem;


    bool GetItem = false;

    //[SerializeField] Block blockPrefab = null;

    private int getbullet = 0;
    public int GetBullet { get { return getbullet; } set { getbullet = value; } }

    private int getgrenade = 0;
    public int Grenade { get { return getgrenade; } set { getgrenade = value; }  }

    private int getshield = 0;
    public int Shield { get { return getshield; } set { getshield = value; } }

    private int gethandgun = 0;
    public int Handgun { get { return gethandgun; } set { gethandgun = value; } }

    private int getrepeater = 0;
    public int Repeater { get { return getrepeater; } set { getrepeater = value; } }

    public int getShotgun = 0;
    public int Shotgun { get { return getShotgun; } set { getShotgun = value; } }

    public int getSniperRifle = 0;
    public int SniperRifle { get { return getSniperRifle; } set { getSniperRifle = value; } }

    private int getCoin = 0;
    public int Coin { get { return getCoin; } set { getCoin = value; } }

    [SerializeField] BlockSpawner blockSpawnerPrefab;
    Block block;

    private void Awake()
    {
        //blockData = GetComponent<Scriptable_PinBallBlock>();

        blockSpawnerPrefab = GetComponent<BlockSpawner>();
    }


    


    private void PlusItemNum()
    {

    }

}
