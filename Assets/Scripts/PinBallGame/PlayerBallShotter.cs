using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerBallShotter : MonoBehaviourPunCallbacks, IPunObservable
{
    public PlayerBullet bullet = null;
    public Transform BulletPos;
    public Transform Pos;
    public float cooltime = 0f;
    public Vector2 len;
    public bool PlayerTurn = true;
    private PhotonView view;
    private PhotonTransformView transformview;
    [SerializeField] SpriteRenderer arrow;

    //public int Playcount = 3;
    //public int GetPlayercount { get { return Playcount; } }
    bool isGameOver = false;


    void Start()
    {
        view = GetComponent<PhotonView>();
        transformview = GetComponent<PhotonTransformView>();
        arrow.enabled = false;
        

    }


    void Update()
    {

        if (PhotonNetwork.IsMasterClient && PlayerTurn)
        {


            Debug.Log("¸¶½ºÅÍµé¾î¿È");
            Debug.Log(PlayerTurn);

            len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;


            if (Input.GetMouseButtonDown(0))
            {

                if (!GameObject.Find("PlayerBall"))
                {
                    if (len.y > 1.5f)
                    {
                        PlayerBullet instPlayerBullet = Instantiate(bullet, BulletPos.transform.position, Quaternion.identity);

                        instPlayerBullet.SetMoveDir(len.normalized);
                        PlayerTurn = false;
                        photonView.RPC("GameTurn", RpcTarget.All,false);
                    }
                }
            }
            if (len.y > 1.5f)
            {
                arrow.enabled = true;
                float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, z);
            }


            else if (arrow.enabled)
            {
                arrow.enabled = false;
            }
        }

        else if (!PhotonNetwork.IsMasterClient && !PlayerTurn)
        {
            Debug.Log("Àûµé¿È");
            len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            //if (Input.GetMouseButtonDown(0) && Playcount  > 0)
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("PlayerShotter" + PlayerTurn);
                if (!GameObject.Find("PlayerBall"))
                {
                    if (len.y > 1.5f)
                    {
                        PlayerBullet instPlayerBullet = Instantiate(bullet, BulletPos.transform.position, Quaternion.identity);
                        instPlayerBullet.SetMoveDir(len.normalized);
                        PlayerTurn = true;
                        photonView.RPC("GameTurn", RpcTarget.All, true);
                    }
                }
            }
            if (len.y > 1.5f)
            {
                arrow.enabled = true;
                float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, z);
            }


            else if (arrow.enabled)
            {
                arrow.enabled = false;
                
            }

        }

    }

    [PunRPC]
    void GameTurn(bool a)
    {

        PlayerTurn = a;

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
        stream.SendNext(transform.position);
        stream.SendNext(transform.rotation);
        }

        else
        {
        transform.position = (Vector3)stream.ReceiveNext();
        transform.rotation = (Quaternion)stream.ReceiveNext();
        }

    }
}
