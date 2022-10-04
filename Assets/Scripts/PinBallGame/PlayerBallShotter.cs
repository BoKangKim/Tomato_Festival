using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerBallShotter : MonoBehaviourPun, IPunObservable
{
    public PlayerBullet bullet = null;
    public Transform BulletPos;
    public Transform Pos;
    public float cooltime = 0f;
    public Vector2 len;
    public bool PlayerTurn = true;
    private PhotonView view;
    private PhotonTransformView transformview;
    GameController gamecontroller = null;
    PlayerBallShotter OtherPlayer = null;
    [SerializeField] SpriteRenderer arrow;
    public int PlayerCount = 3;
    GameObject PinballObject = null;
    bool isGameOver = false;

    private void Awake()
    {
        gamecontroller = FindObjectOfType<GameController>();
    }

    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(PlayerCount);
        }
        else
        {
            PlayerCount = (int)stream.ReceiveNext();

        }
    }


    void Start()
    {
        view = GetComponent<PhotonView>();
        PinballObject = GameObject.FindWithTag("PinballGame");
        gameObject.transform.SetParent(PinballObject.transform);

    }

    private void OnEnable()
    {
        if (PlayerCount == 0)
        {
            PlayerCount = 3;
        }

        if (PhotonNetwork.IsMasterClient && photonView.IsMine == true)
        {
            PlayerTurn = true;
            photonView.RPC("Setarrow", RpcTarget.Others, true);
        }
        else if(PhotonNetwork.IsMasterClient == false && photonView.IsMine == true)
        {
            PlayerTurn = false;
            photonView.RPC("Setarrow", RpcTarget.Others, false);
        }
    }


    void Update()
    {

        if (OtherPlayer == null)
        {
            PlayerBallShotter[] pb = FindObjectsOfType<PlayerBallShotter>();
            for (int i = 0; i < pb.Length; i++)
            {
                if (pb[i].photonView.IsMine == false)
                {
                    OtherPlayer = pb[i];

                    break;
                }
            }
        }
        if (PlayerCount == 0)
        {
            arrow.enabled = false;
        }


        if (photonView.IsMine && PlayerCount == 0 && OtherPlayer.PlayerCount == 0)
        {
            if (GameObject.FindWithTag("Ball") == null)
            {
                gamecontroller.PinballOver();
            }
        }


        if (!photonView.IsMine || PlayerCount == 0)
        {
            return;
        }




        #region 
        if (PlayerTurn)
        {
            arrow.enabled = true;
            photonView.RPC("Setarrow", RpcTarget.Others, true);
            len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                if (GameObject.FindWithTag("Ball") == null)
                {
                    if (len.y > 1.5f)
                    {
                        --PlayerCount;
                        GameObject instPlayerBullet = PhotonNetwork.Instantiate("Player1_Ball", BulletPos.transform.position, Quaternion.identity);

                        instPlayerBullet.GetComponent<PlayerBullet>().SetMoveDir(len.normalized);

                        PlayerTurn = false;

                        PlayerBallShotter[] bs = FindObjectsOfType<PlayerBallShotter>();
                        for (int i = 0; i < bs.Length; i++)
                        {
                            bs[i].photonView.RPC("SetTurn", RpcTarget.Others, !PlayerTurn);
                        }
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

        else if (!PlayerTurn)
        {
            arrow.enabled = false;
            photonView.RPC("Setarrow", RpcTarget.Others, false);
        }
    }
    #endregion

    [PunRPC]
    void SetTurn(bool a)
    {
        PlayerTurn = a;
    }
    [PunRPC]
    void Setarrow(bool a)
    {
        arrow.enabled = a;
    }
    [PunRPC]
    void SetColor(int a)
    {
        switch (a)
        {
            case 1:
                {
                    arrow.color = Color.red;
                }
                break;
            case 2:
                {
                    arrow.color = Color.green;
                }
                break;
        }

    }


}



