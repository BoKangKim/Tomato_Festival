using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerBullet : MonoBehaviourPun, IPunObservable
{
    public float speed;
    // 여러번 충돌을 막기위해
    bool enter;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    // 반사 벡터
    Vector3 reflectVector;
    bool playerTurn = false;
    PlayerBallShotter playerBallShotter;
    Vector3 remotePos;
    Quaternion remoteRot;




    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enter = false;
        playerBallShotter = GetComponent<PlayerBallShotter>();


    }

    public void PlayerTurn(bool PlayerTurn)
    {
        this.playerTurn = PlayerTurn;
    }


    // 슈터에서 불릿을 생성할때 방향값을 최신화 해줌
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

        // Dot 다시한번 확인해봐야함 - 개인적인생각 vector값끼리 서로 못곱하기때문에 Dot의 값으로 어쩌구함 
        //  반사벡터가 다시 튕길때는 입사각이기때문에 반사벡터에 넣어준다.
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
}
