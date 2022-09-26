using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PinBallGame_ItemCheck : MonoBehaviour
{

    //Scriptable_PinBallBlock scriptable_PinBallBlock;
    //List<PinBallGame_ItemCheck> _items;

    //Block block;
    //BlockSpawner spawner;

    List<string> ItemCheck;

    private void Awake()
    {
        ItemCheck = new List<string>();
    }

    public void AddItemCheckList(string destroyBlockData)
    {
        ItemCheck.Add(destroyBlockData);
        for(int i = 0; i < ItemCheck.Count; i++)
        {
            Debug.Log(ItemCheck[i]);
        }
    }
    

    public List<string> GetMyItemDataList()
    {
        return ItemCheck;
    }

}
