using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "BlockData", menuName = "SciptableObject_PinBall/BlockData", order = 1)]

public class ScriptableBlock : ScriptableObject
{
    //[SerializeField] string blockName;
    //public string GetblockName { get { return blockName; } }

    [Header("몇번 맞으면 깨지는지?")]
    [SerializeField] int blockMaxHP;
    public int GetblockMaxHp { get { return blockMaxHP; } }

    [Header("한번 맞을때 받은 데미지")]
    [SerializeField] int blockgetDamages;
    public int GetblockgetDamages { get { return blockgetDamages; } }

    //[Header("블럭 이미지")]
    //[SerializeField] Sprite sprite;
    //public Sprite Getsprite { get { return sprite; } }

    [Header("블럭 컬러")]
    [SerializeField] Color color;
    public Color GetColor { get { return color; } }


    // 나중에 시간 될시 시도 해볼 예정
    // 텍스트 메쉬 프로가 안 들어가짐 ㅠㅠ
    //[Header("블럭 HP 표시 텍스트")]
    //[SerializeField] TextMeshProUGUI blockHP;
    //public TextMeshProUGUI GetblockHP { get { return blockHP; } }



}
