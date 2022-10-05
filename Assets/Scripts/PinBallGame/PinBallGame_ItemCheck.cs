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
        ItemCheck.Add("SniperRifle");
        ItemCheck.Add("Shotgun");
        ItemCheck.Add("Repeater");
        return ItemCheck;
    }

}
