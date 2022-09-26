using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinBall_ItemMgr : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Item_Bullet = null;
    [SerializeField] TextMeshProUGUI Item_Grenade = null;
    [SerializeField] TextMeshProUGUI Item_Shield = null;
    [SerializeField] TextMeshProUGUI Item_Handgun = null;
    [SerializeField] TextMeshProUGUI Item_Repeater = null;
    [SerializeField] TextMeshProUGUI Item_Shotgun = null;
    [SerializeField] TextMeshProUGUI Item_SniperRifle = null;
    [SerializeField] TextMeshProUGUI Item_Coin = null;

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


    void Start()
    {
        Item_Bullet.text = "X" + getbullet;
        Item_Grenade.text = "X" + getgrenade;
        Item_Shield.text= "X" + getshield;
        Item_Handgun.text = "X" + gethandgun;
        Item_Repeater.text = "X" + getrepeater;
        Item_Shotgun.text = "X" + getShotgun;
        Item_SniperRifle.text = "X" + getSniperRifle;
        Item_Coin.text = "X" + getCoin;
    }


    private void PlusItemNum()
    {
 
        getbullet += pinballGetItem[0].GetItemNum;
        Item_Bullet.text = "X" + getbullet;

        getgrenade += pinballGetItem[1].GetItemNum;
        Item_Grenade.text = "X" + getgrenade;

        getshield += pinballGetItem[2].GetItemNum;
        Item_Shield.text = "X" + getshield;

        gethandgun += pinballGetItem[3].GetItemNum;
        Item_Handgun.text = "X" + gethandgun;

        getrepeater += pinballGetItem[4].GetItemNum;
        Item_Repeater.text = "X" + getrepeater;

        getShotgun += pinballGetItem[5].GetItemNum;
        Item_Shotgun.text = "X" + getShotgun;

        getSniperRifle += pinballGetItem[6].GetItemNum;
        Item_SniperRifle.text = "X" + getSniperRifle;

        getCoin += pinballGetItem[7].GetItemNum;
        Item_Coin.text = "X" + getCoin;

    }


    void Update()
    {

    }
}
