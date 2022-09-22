using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "SciptableObject_PinBall/ItemData", order = 1)]

public class Scriptable_PinBallGetItem : ScriptableObject
{
    [Header("블럭 깨진 후 획득 아이템 이름")]
    [SerializeField] string blockItemName;
    public string GetblockItemName { get { return blockItemName; } }

    [Header("아이템 수량")]
    [SerializeField] int ItemNum;
    public int GetItemNum { get { return ItemNum; } }

}
