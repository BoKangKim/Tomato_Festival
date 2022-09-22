using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    // 여러번 충돌을 막기위해
    bool enter;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    // 반사 벡터
    Vector3 reflectVector;
    bool playerTurn = false;

   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enter = false;

        //if (playerBallShotter.PlayerTurn = true)
        //{
        //    Debug.Log("빨간맛");
        //    spriteRenderer.color = Color.red;

        //}
        //else
        //{
        //    Debug.Log("노란맛");
        //    spriteRenderer.color = Color.yellow;
        //}

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

        // Dot 다시한번 확인해봐야함 - 개인적인생각 vector값끼리 서로 못곱하기때문에 Dot의 값으로 어쩌구함 
        //  반사벡터가 다시 튕길때는 입사각이기때문에 반사벡터에 넣어준다.
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
