using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class PinBallGame_ItemCheck : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Item_Bullet = null;
    [SerializeField] TextMeshProUGUI Item_Grenade = null;
    [SerializeField] TextMeshProUGUI Item_Shield = null;
    [SerializeField] TextMeshProUGUI Item_Handgun = null;
    [SerializeField] TextMeshProUGUI Item_Repeater = null;
    [SerializeField] TextMeshProUGUI Item_Shotgun = null;
    [SerializeField] TextMeshProUGUI Item_SniperRifle = null;
    [SerializeField] TextMeshProUGUI Item_Coin = null;

    List<string> ItemCheck = new List<string>();
    void Start()
    {
        Item_Bullet.text = "X " + 0;
        Item_Grenade.text = "X " + 0;
        Item_Shield.text = "X " + 0;
        Item_Handgun.text = "X " + 0;
        Item_Repeater.text = "X " + 0;
        Item_Shotgun.text = "X " + 0;
        Item_SniperRifle.text = "X " + 0;
        Item_Coin.text = "X " + 0;
    }

    public void AddItemCheckList(string destroyBlockData)
    {
        ItemCheck.Add(destroyBlockData);
    }


    public List<string> GetMyItemDataList()
    {
        ItemCheck.Add("Bullet");
        ItemCheck.Add("Grenade");
        ItemCheck.Add("Shield");
        ItemCheck.Add("Grenade");
        ItemCheck.Add("Shield");
        ItemCheck.Add("Grenade");
        ItemCheck.Add("Shield");
        ItemCheck.Add("Grenade");
        ItemCheck.Add("Shield");
        ItemCheck.Add("Repeater");
        ItemCheck.Add("Shotgun");
        ItemCheck.Add("SniperRifle");
        return ItemCheck;
    }

}
