using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class PinBallGame_ItemCheck : MonoBehaviour
{
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
        ItemCheck.Add("Repeater");
        ItemCheck.Add("Shotgun");
        ItemCheck.Add("SniperRifle");
        return ItemCheck;
    }

}
