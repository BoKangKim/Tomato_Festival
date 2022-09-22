using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI blockHP;
    public ScriptableBlock blockData;
    BlockSpawner blockSpawner;
    SpriteRenderer BlockRender;
    BoxCollider2D BlockBC = null;

    int currentHP = 0;
    bool enter;

    private void OnEnable()
    {
        if(blockData != null)
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
    }


    private void Start()
    {
        currentHP = blockData.GetblockMaxHp;
        enter = false;
        BlockRender.color = blockData.GetColor;
        blockHP.text = "" + currentHP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enter) { return; }

        currentHP -= blockData.GetblockgetDamages;
        blockHP.text = "" + currentHP;

        if (currentHP <= 0)
        {
            blockSpawner.Destroyblock(this);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        enter = false;
    }
}
