using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerBullet : MonoBehaviourPun, IPunObservable
{
    public float speed;
    // ������ �浹�� ��������
    bool enter;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    // �ݻ� ����
    Vector3 reflectVector;
    bool playerTurn = false;
    PlayerBallShotter playerBallShotter;
    Vector3 remotePos;
    Quaternion remoteRot;

    [Header("[����Ʈ]")]
    [SerializeField] GameObject effObject = null;
    [SerializeField] Transform effPos = null;


    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enter = false;
        playerBallShotter = GetComponent<PlayerBallShotter>();

        PlayerBallEffect();
    }

    public void PlayerTurn(bool PlayerTurn)
    {
        this.playerTurn = PlayerTurn;
    }


    // ���Ϳ��� �Ҹ��� �����Ҷ� ���Ⱚ�� �ֽ�ȭ ����
    public void SetMoveDir(Vector3 v)
    {
        reflectVector = v;
    }

    void Update()
    {
        if (false == photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, remotePos, 10 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, remoteRot, 10 * Time.deltaTime);
            return;
        }

        //rb.position += speed * Time.deltaTime * (Vector2)reflectVector.normalized;
        if (photonView.IsMine)
            rb.velocity = (reflectVector * speed);

        
        //rb.AddForce((Vector2)reflectVector.normalized);

        //transform.position += speed * Time.deltaTime * reflectVector;
        //rb.velocity = (reflectVector * speed);
        //rb.MovePosition(Time.deltaTime * speed * reflectVector);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enter) { return; }



        // Dot �ٽ��ѹ� Ȯ���غ����� - �������λ��� vector������ ���� �����ϱ⶧���� Dot�� ������ ��¼���� 
        //  �ݻ纤�Ͱ� �ٽ� ƨ�涧�� �Ի簢�̱⶧���� �ݻ纤�Ϳ� �־��ش�.
        // R = P+2N(-P*N)
        reflectVector = reflectVector + 2 * (Vector3)collision.contacts[0].normal * (-Vector3.Dot(reflectVector, collision.contacts[0].normal));
        enter = true;

        if (collision.gameObject.tag == "StopWall")
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enter = false;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            remotePos = (Vector3)stream.ReceiveNext();
            remoteRot = (Quaternion)stream.ReceiveNext();
        }

    }

    void PlayerBallEffect()
    {
        GameObject instObj = Instantiate(effObject, effPos.position, Quaternion.identity);
        Destroy(instObj, 3f);
    }



}
        else
        {
            remotePos = (Vector3)stream.ReceiveNext();
            remoteRot = (Quaternion)stream.ReceiveNext();
        }
    }
<<<<<<< HEAD
=======

    void PlayerBallEffect()
    {
        GameObject instObj = Instantiate(effObject, effPos.position, Quaternion.identity);
        Destroy(instObj, 3f);
    }



>>>>>>> origin/HyeWon
}




