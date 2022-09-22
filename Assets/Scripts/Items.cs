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
    SpriteRenderer spriteRender = null;
    public int MyItemIndex { get; set; } = 0;
    Vector3 fireDir = Vector3.zero;
    

    private void Awake()
    {
        player = GetComponent<Player>();
        spriteRender = player.GetComponent<SpriteRenderer>();
        if(photonView.IsMine == true)
        {
            myImg = FindObjectOfType<Image>();
        }
    }

    private void Start()
    {
        SplitData splitData = FindObjectOfType<SplitData>();

        if (splitData != null && photonView.IsMine == true)
            splitData.GetAndSplitData();
    }

    private void Update()
    {
        if (photonView.IsMine == false)
            return;

        if (items.Count == 0)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            StartItemCoroutine();
            items.RemoveAt(MyItemIndex);
            
            if (MyItemIndex == items.Count)
            {
                MyItemIndex = 0;
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
            if(MyItemIndex == 0)
            {
                MyItemIndex = items.Count - 1;
            }
            else
            {
                MyItemIndex--;
            }
            ChangeUIImg();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if(MyItemIndex == items.Count - 1)
            {
                MyItemIndex = 0;
            }
            else
            {
                MyItemIndex++;
            }
            ChangeUIImg();
        }
    }

    public void ItemListSetting()
    {
        Debug.Log("ItemListSetting");
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals("Bullet"))
            {
                player.bulletCount += 5;
                items.RemoveAt(i);
            }
        }

        ChangeUIImg();
    }

    public void StartItemCoroutine()
    {
        StartCoroutine(items[MyItemIndex]);
    }

    IEnumerator Shield()
    {
        spriteRender.color = Color.blue;
        player.IsShieldTime = true;
        yield return new WaitForSeconds(shieldTime);
        spriteRender.color = Color.green;
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

        GameObject grenade = PhotonNetwork.Instantiate("Grenade",(transform.position),Quaternion.identity);
        grenade.transform.position = player.transform.position + fireDir;


        myRigidbody = grenade.GetComponent<Rigidbody2D>();
        myRigidbody.AddForce(fireDir * 500f,ForceMode2D.Force);

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
        Debug.Log("SetList");
        this.items = items;
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
