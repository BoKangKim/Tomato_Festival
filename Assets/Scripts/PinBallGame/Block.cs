using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public delegate void AddList(string data);

public class Block : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI blockHP;
    [SerializeField] TextMeshProUGUI playCount;

    public Scriptable_PinBallBlock blockData;
    BlockSpawner blockSpawner;
    //PinBallGame_ItemCheck pinBallGame_ItemCheck;

    SpriteRenderer BlockRender;

    BoxCollider2D BlockBC = null;

    
    //List<string> ItemInfo;
    public AddList add;
    public int currentHP = 0; 

    public int Playcount = 3;



    //bool isGameChange = false;
    bool enter;

    private void OnEnable()
    {
        if (blockData != null)
        {
            currentHP = blockData.GetblockMaxHp;
            enter = false;
            BlockRender.color = blockData.GetColor;
            blockHP.text = "" + currentHP;
        }
    }

    private void Awake()
    {
        //  BlockSpawner spawner = new BlockSpawner();
        BlockRender = GetComponent<SpriteRenderer>();
        BlockBC = GetComponent<BoxCollider2D>();
        blockSpawner = FindObjectOfType<BlockSpawner>();
        PinBallGame_ItemCheck pinBallGame_ItemCheck = FindObjectOfType<PinBallGame_ItemCheck>();
        add = pinBallGame_ItemCheck.AddItemCheckList;
        //ItemInfo = new List<string>();
    }
    private void Start()
    {
        currentHP = blockData.GetblockMaxHp;
        enter = false;
        BlockRender.color = blockData.GetColor;
        blockHP.text = "" + currentHP;

        //playCount.text = "Play Count : " + Playcount;
    }

    public void MinusPlayCount()
    {
        Playcount--;
        playCount.text = "Play Count : " + Playcount;

        //if (Playcount == 0)
        //{
        //    isGameChange = true;
        //    return;
        //}
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (enter) { return; }

        currentHP -= blockData.GetblockgetDamages;


        blockHP.text = "" + currentHP;

        if (currentHP <= 0)
        {
            // 죽으면 자기가 가지고 있는 아이템이름과 갯수를 핀볼 아이템 매니저에게 알려준다.
            add(blockData.GetblockItemName);
            
            blockSpawner.Destroyblock(this);

            //pinBallGame_ItemCheck.AddItemCheckList(blockData.GetblockItemName);
            //Debug.Log("InstAdd : " );

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enter = false;
    }
}
