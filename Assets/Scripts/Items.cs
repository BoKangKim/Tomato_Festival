using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class Items : MonoBehaviourPun
{
    [SerializeField] Sprite[] itemImgs;

    public Image myImg;
    List<string> items;
    Player player = null;
    Player myEnemy = null;
    Rigidbody2D myRigidbody;
    float shieldTime = 2f;
    public GameObject GrenadePrefab = null;
    GameObject MyShield = null;
    int MyItemIndex = 0;
    Vector3 fireDir = Vector3.zero;
    public bool ItemIsMine { get; set; } = false;

    private void Awake()
    {
        items = new List<string>();
        player = GetComponent<Player>();
        MyShield = gameObject.transform.Find("ShiledEffect").gameObject;
        myImg = FindObjectOfType<Image>();
        ItemIsMine = photonView.IsMine;
    }

    private void Start()
    {
        if (items != null)
            return;

        SplitData splitData = FindObjectOfType<SplitData>();

        if (splitData != null)
            splitData.GetAndSplitData();
    }

    private void Update()
    {
        if (items.Count == 0)
            return;

        if (photonView.IsMine == false)
            return;
        
        if (Input.GetMouseButtonDown(1))
        {
            if (items[MyItemIndex].Equals("Grenade"))
                StartItemCoroutine();
            else if (items[MyItemIndex].Equals("Shield"))
                photonView.RPC("RPC_StartItemCorountine",RpcTarget.All);

            items.RemoveAt(MyItemIndex);
            photonView.RPC("RPC_RemoveItemList",RpcTarget.Others);

            if (MyItemIndex == items.Count)
            {
                MyItemIndex = 0;
                photonView.RPC("RPC_AsyncMyItemIndex", RpcTarget.Others,MyItemIndex);
            }

            if (items.Count == 0)
            {
                myImg.sprite = null;
                myImg.gameObject.SetActive(false);
            }
            else
            {
                ChangeUIImg();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (MyItemIndex == 0)
            {
                MyItemIndex = items.Count - 1;
            }
            else
            {
                MyItemIndex--;
            }

            photonView.RPC("RPC_AsyncMyItemIndex", RpcTarget.Others, MyItemIndex);
            ChangeUIImg();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (MyItemIndex == items.Count - 1)
            {
                MyItemIndex = 0;
            }
            else
            {
                MyItemIndex++;
            }

            photonView.RPC("RPC_AsyncMyItemIndex", RpcTarget.Others, MyItemIndex);
            ChangeUIImg();
        }
    }

    public void ItemListSetting()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals("Bullet"))
            {
                player.bulletCount += 5;
                items.RemoveAt(i);
            }
        }
        photonView.RPC("RPC_AsyncItemList",RpcTarget.Others,this.items);

        Debug.Log(items.Count);
        ChangeUIImg();
    }

    void StartItemCoroutine()
    {
        StartCoroutine(items[MyItemIndex]);
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    Debug.Log("OnPhotonSerializeView0");
    //    if (stream.IsWriting)
    //    {
    //        Debug.Log("OnPhotonSerializeView1");
    //        stream.SendNext(MyItemIndex);
    //        stream.SendNext(MyShield.activeSelf);
    //        Debug.Log("OnPhotonSerializeView2");
    //    }
    //    else
    //    {
    //        Debug.Log("OnPhotonSerializeView  Receive");
    //        MyItemIndex = (int)stream.ReceiveNext();
    //        MyShield.SetActive((bool)stream.ReceiveNext());
    //    }
    //}

    [PunRPC]
    void RPC_AsyncMyItemIndex(int MyItemIndex)
    {
        this.MyItemIndex = MyItemIndex;
    }

    [PunRPC]
    void RPC_AsyncItemList(List<string> items)
    {
        for (int i = this.items.Count; i < items.Count; i++)
        {
            this.items.Add(items[i]);
        }
    }

    [PunRPC]
    void RPC_RemoveItemList()
    {
        items.RemoveAt(MyItemIndex);
    }

   [PunRPC]
    void RPC_StartItemCorountine()
    {
        StartCoroutine(items[MyItemIndex]);
    }

   

    IEnumerator Shield()
    {
        MyShield.SetActive(true);
        player.IsShieldTime = true;
        yield return new WaitForSeconds(shieldTime);
        MyShield.SetActive(false);
        player.IsShieldTime = false;
    }

    void TransferFireDir()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        fireDir = (mousePos - transform.position).normalized; //발사방향
    }

    IEnumerator Grenade()
    {
        TransferFireDir();

        GameObject grenade = PhotonNetwork.Instantiate("Grenade", (transform.position), Quaternion.identity);
        grenade.transform.position = player.transform.position + fireDir;


        myRigidbody = grenade.GetComponent<Rigidbody2D>();
        myRigidbody.AddForce(fireDir * 500f, ForceMode2D.Force);

        yield return new WaitForSeconds(3f);
        GrenadeExplosion(grenade);
        PhotonNetwork.Destroy(grenade.gameObject);
    }

    private void GrenadeExplosion(GameObject grenade)
    {
        if (photonView.IsMine == false)
            return;

        if (myEnemy == null)
        {
            Player[] players = FindObjectsOfType<Player>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].photonView.IsMine == false)
                {
                    myEnemy = players[i];
                    break;
                }
            }

            if (myEnemy == null)
                Debug.LogError("myEnemy is null Check Gun.cs");
        }

        if (Vector3.Distance(grenade.transform.position, myEnemy.transform.position) <= 5f)
        {
            myEnemy.SendMessage("StartKnockBackCoroutine", grenade.transform.position, SendMessageOptions.DontRequireReceiver);
        }

    }
    
    public void SetList(List<string> items)
    {
        for(int i = this.items.Count; i < items.Count; i++)
        {
            this.items.Add(items[i]);
        }
    }
    
    void ChangeUIImg()
    {
        if (items[MyItemIndex].Equals("Grenade"))
        {
            myImg.sprite = itemImgs[0];
        }
        else if (items[MyItemIndex].Equals("Shield"))
        {
            myImg.sprite = itemImgs[1];
        }
        
    }

    
}
