using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PinBallGame_ItemCheck : MonoBehaviour
{
    List<string> ItemCheck = new List<string>();

    public void AddItemCheckList(string destroyBlockData)
    {
        ItemCheck.Add(destroyBlockData);
        for (int i = 0; i < ItemCheck.Count; i++)
        {
            Debug.Log(ItemCheck[i]);
        }
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
