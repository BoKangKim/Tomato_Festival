using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Photon.Pun;

public delegate void AddList(string data, bool ismine);
public delegate void BlockEvent(string itemname, Vector3 Pos);

public class Block : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI blockHP;
    [SerializeField] TextMeshProUGUI playCount;
    ImgUIManager imgUIManager;
    GameObject PinballObject;
    public Scriptable_PinBallBlock blockData;
    BlockSpawner blockSpawner;
    //PinBallGame_ItemCheck pinBallGame_ItemCheck;

    public SpriteRenderer BlockRender;
    BoxCollider2D BlockBC = null;
    
    //List<string> ItemInfo;
    public AddList add;
    BlockEvent blockEvent;
    public int currentHP = 0;
    //public int Playcount = 3;
    int blockCount = 0;
    //bool isGameChange = false;
    bool enter;

    [SerializeField] GameObject blockeff = null;

    private void Awake()
    {
        //  BlockSpawner spawner = new BlockSpawner();
        BlockRender = GetComponent<SpriteRenderer>();
        BlockBC = GetComponent<BoxCollider2D>();
        blockSpawner = FindObjectOfType<BlockSpawner>();
        imgUIManager = FindObjectOfType<ImgUIManager>();
        blockEvent = imgUIManager.RenderItem;
        PinBallGame_ItemCheck pinBallGame_ItemCheck = FindObjectOfType<PinBallGame_ItemCheck>();
        add = pinBallGame_ItemCheck.AddItemCheckList;
        

        //ItemInfo = new List<string>();
    }



    private void Start()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            currentHP = blockData.GetblockMaxHp;
            enter = false;
            BlockRender.color = blockData.GetColor;
            blockHP.text = "" + currentHP;
        }

        if (PhotonNetwork.IsMasterClient == false)
        {
            PinballObject = GameObject.FindWithTag("PinballGame");
            gameObject.transform.SetParent(PinballObject.transform);
        }
        
        //playCount.text = "Play Count : " + Playcount;
    }
    
    public void RPC_BlockDatas(int scriptableIdx)
    {
        currentHP = blockData.GetblockMaxHp;
        enter = false;
        BlockRender.color = blockData.GetColor;
        blockHP.text = "" + currentHP;
        photonView.RPC("SetBlockData", RpcTarget.Others, blockData.GetblockMaxHp, blockData.GetColor.r, blockData.GetColor.g, blockData.GetColor.b, scriptableIdx);
    }

    [PunRPC]
    public void SetBlockData(int currentBlockHp, float r, float g, float b, int scriptableIdx)
    {
        this.currentHP = currentBlockHp;
        blockHP.text = "" + currentHP;
        BlockRender.color = new Color(r, g, b);
        blockData = blockSpawner.GetBlockData(scriptableIdx);
    }

    [PunRPC]
    public void BlockTransFormChange(Vector3 pos)
    {
        this.gameObject.transform.position = pos;
    }

    [PunRPC]
    public void blockDamages(int Damage, bool isMine)
    {
        //if (currentHP > 0)
        currentHP -= Damage;

        if (currentHP > 0)
        {
            //Debug.Log("## blockDamages " + Damage);
            blockHP.text = "" + currentHP;
        }
        else if (currentHP <= 0)
        {
            BlockEffect();
            //Debug.Log("BlockEffect : ������? " + blockeff);
            //Debug.Log("currentHP <= 0 : ");
            //Debug.Log("currentHP2 : " + currentHP);
            if (isMine) //���̸� ������
            {
                add(blockData.GetblockItemName, isMine);
            }
            else if(isMine == false) //other
            {
                add(blockData.GetblockItemName, isMine);
            }
            
            
            blockEvent(blockData.GetblockItemName, this.transform.position);
            
            this.transform.SetParent(GameObject.Find("BlockRemove").transform);

            if (photonView.IsMine)
                PhotonNetwork.Destroy(this.gameObject);


            //pinBallGame_ItemCheck.AddItemCheckList(blockData.GetblockItemName);
            //Debug.Log("InstAdd : ");
        }

    }

    void BlockEffect()
    {
        GameObject instObj = Instantiate(blockeff, this.transform.position, Quaternion.identity);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("## OnCollisionEnter - " + collision.gameObject.name);
        #region �����ڵ�
        //if (photonView.IsMine == true)
        //{
        //    //Damages = blockData.GetblockgetDamages;
        //    //currentHP -= Damages;

        //    photonView.RPC("blockDamages", RpcTarget.All, blockData.GetblockgetDamages);
        //}
        //if(PhotonNetwork.IsMasterClient)
        //Damages = blockData.GetblockgetDamages;
        //currentHP -= Damages;
        #endregion
        //if (currentHP > 0)
        ////if (currentHP > 0 && photonView.IsMine == true)
        //{
        //Debug.Log("## ��?����?��?����?������?");
        PlayerBullet pb = collision.gameObject.GetComponent<PlayerBullet>();

        if (pb.photonView.IsMine == false)
            return;

        if (enter) { return; }

        photonView.RPC("blockDamages", RpcTarget.All, blockData.GetblockgetDamages, photonView.IsMine);
        //Debug.Log("blockDamages : " );
        //Debug.Log("currentHP1 : " + currentHP);

        //if (currentHP <= 0 && photonView.IsMine == true)
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enter = false;
    }


}
