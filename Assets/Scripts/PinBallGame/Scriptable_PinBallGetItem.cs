using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "SciptableObject_PinBall/ItemData", order = 1)]

public class Scriptable_PinBallGetItem : ScriptableObject
{
    [Header("�� ���� �� ȹ�� ������ �̸�")]
    [SerializeField] string blockItemName;
    public string GetblockItemName { get { return blockItemName; } }

    [Header("������ ����")]
    [SerializeField] int ItemNum;
    public int GetItemNum { get { return ItemNum; } }

}
