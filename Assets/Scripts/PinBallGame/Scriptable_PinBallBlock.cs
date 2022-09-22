using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "BlockData", menuName = "SciptableObject_PinBall/BlockData", order = 1)]

public class Scriptable_PinBallBlock : ScriptableObject
{
    //[SerializeField] string blockName;
    //public string GetblockName { get { return blockName; } }

    [Header("��� ������ ��������?")]
    [SerializeField] int blockMaxHP;
    public int GetblockMaxHp { get { return blockMaxHP; } }

    [Header("�ѹ� ������ ���� ������")]
    [SerializeField] int blockgetDamages;
    public int GetblockgetDamages { get { return blockgetDamages; } }

    //[Header("�� �̹���")]
    //[SerializeField] Sprite sprite;
    //public Sprite Getsprite { get { return sprite; } }

    [Header("�� �÷�")]
    [SerializeField] Color color;
    public Color GetColor { get { return color; } }

    [Header("�� ���� �� ȹ�� ������ �̸�(Ȯ�� ��)")]
    [SerializeField] string blockItemName;
    public string GetblockItemName { get { return blockItemName; } }

    [Header("������ ����")]
    [SerializeField] int ItemNum;
    public int GetItemNum { get { return ItemNum; } }


    // ���߿� �ð� �ɽ� �õ� �غ� ����
    // �ؽ�Ʈ �޽� ���ΰ� �� ���� �Ф�
    //[Header("�� HP ǥ�� �ؽ�Ʈ")]
    //[SerializeField] TextMeshProUGUI blockHP;
    //public TextMeshProUGUI GetblockHP { get { return blockHP; } }



}
