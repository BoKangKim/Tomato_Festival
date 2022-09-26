using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBallShotter : MonoBehaviour
{
    public PlayerBullet bullet = null;
    public Transform BulletPos;
    public Transform Pos;
    public float cooltime = 0f;
    public Vector2 len;
    public bool PlayerTurn { get; set; } = true;

    [SerializeField] SpriteRenderer arrow;
    
    //public int Playcount = 3;
    //public int GetPlayercount { get { return Playcount; } }
    bool isGameOver = false;


    void Start()
    {
        arrow.enabled = false;
    }


    void Update()
    {

        if (PlayerTurn)
        {
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

                    }
                }
            }
            if (len.y > 1.5f)
            {
                arrow.enabled = true;
                //Debug.Log("플레이어 슈터");
                float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, z);
            }


            else if (arrow.enabled)
            {
                arrow.enabled = false;
            }

        }
    }
}
