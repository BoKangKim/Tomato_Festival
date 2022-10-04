using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class Items : MonoBehaviourPun
{
    [SerializeField] Sprite[] itemImgs;

    Image myImg;
    List<string> items = null;
    PlayerBattle player = null;
    PlayerBattle myEnemy = null;
    Rigidbody2D myRigidbody;
    float shieldTime = 2f;
    public GameObject GrenadePrefab = null;
    GameObject MyShield = null;
    int MyItemIndex = 0;
    Vector3 fireDir = Vector3.zero;
    int preListCount = 0;
    GameObject grenade = null;
    PinBallGame_ItemCheck _itemCheck;

    private void Awake()
    {
        items = new List<string>();
        player = GetComponent<PlayerBattle>();
        MyShield = gameObject.transform.Find("ShiledEffect").gameObject;
        if(photonView.IsMine)
            myImg = GameObject.Find("ItemUIImg").GetComponent<Image>();
        _itemCheck = FindObjectOfType<PinBallGame_ItemCheck>();
    }
    
    private void OnEnable()
    {
        SplitData splitData = FindObjectOfType<SplitData>();

        if (splitData != null && photonView.IsMine)
        {
            preListCount = this.items.Count;
            items.AddRange(splitData.GetAndSplitData("Item"));
            ItemListSetting();
        }
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
            {
                _itemCheck.UpdateGrenadeNum();
                StartItemCoroutine();
            }
            else if (items[MyItemIndex].Equals("Shield"))
            {
                _itemCheck.UpdateShieldNum();
                photonView.RPC("RPC_StartItemCorountine", RpcTarget.All);
            }

            items.RemoveAt(MyItemIndex);
            photonView.RPC("RPC_RemoveItemList", RpcTarget.Others);

            if (items.Count == 0)
            {
                ChangeUIImg();
                return;
            }
            
            if (MyItemIndex == items.Count)
            {
                MyItemIndex = 0;
                photonView.RPC("RPC_AsyncMyItemIndex", RpcTarget.Others, MyItemIndex);
            }
            ChangeUIImg();

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

    #region Async Data

    public void ItemListSetting()
    {
        Gun gun = GetComponentInChildren<Gun>();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals("Bullet"))
            {
                gun.numberOfBullet += 5;
                items.RemoveAt(i);
            }
        }

        for(int i = preListCount; i < items.Count; i++)
        {
            photonView.RPC("RPC_AsyncItemList", RpcTarget.Others, items[i]);
        }

        ChangeUIImg();
    }

    void StartItemCoroutine()
    {
        StartCoroutine(items[MyItemIndex]);
    }

    [PunRPC]
    void RPC_AsyncMyItemIndex(int MyItemIndex)
    {
        this.MyItemIndex = MyItemIndex;
        for(int i = 0; i < this.items.Count; i++)
        {
            Debug.Log(items[i]);
        }
        Debug.Log(this.MyItemIndex);
    }

    [PunRPC]
    void RPC_AsyncItemList(string item)
    {
        items.Add(item);
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

    #endregion

    #region Item Logic
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

        GameObject grenade = PhotonNetwork.Instantiate("Grenade", transform.position + (fireDir * transform.lossyScale.y), Quaternion.identity);

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
            PlayerBattle[] players = FindObjectsOfType<PlayerBattle>();
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
            myEnemy.SendMessage("TransferDamage", 40f, SendMessageOptions.DontRequireReceiver);
            myEnemy.SendMessage("StartKnockBackCoroutine", grenade.transform.position, SendMessageOptions.DontRequireReceiver);
        }

    }
    
    void ChangeUIImg()
    {
        if (myImg == null)
            return;

        if (items.Count == 0)
        {
            if(myImg.gameObject.activeSelf == true)
            {
                Debug.Log("이상하다고 말해");
                myImg.sprite = null;
                myImg.gameObject.SetActive(false);
            }
            return;
        }
        else if(items.Count != 0 && myImg.gameObject.activeSelf == false)
        {
            myImg.gameObject.SetActive(true);
        }

        if (items[MyItemIndex].Equals("Grenade"))
        {
            myImg.sprite = itemImgs[0];
        }
        else if (items[MyItemIndex].Equals("Shield"))
        {
            myImg.sprite = itemImgs[1];
        }
        
    }
    

    #endregion
}
