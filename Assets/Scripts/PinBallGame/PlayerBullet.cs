using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    // ������ �浹�� ��������
    bool enter;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    // �ݻ� ����
    Vector3 reflectVector;
    bool playerTurn = false;

   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enter = false;

        //if (playerBallShotter.PlayerTurn = true)
        //{
        //    Debug.Log("������");
        //    spriteRenderer.color = Color.red;

        //}
        //else
        //{
        //    Debug.Log("�����");
        //    spriteRenderer.color = Color.yellow;
        //}

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


        //rb.position += speed * Time.deltaTime * (Vector2)reflectVector.normalized;

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

            

            Destroy(gameObject);

           

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enter = false;
    }



}
