using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPlayer_Shotter : MonoBehaviour
{
    public AnotherPlayer_Bullet bullet = null;
    public Transform BulletPos;
    public Transform Pos;
    public float cooltime = 0f;
    public Vector2 len;
    private AnotherPlayer_Shotter playerBallShotter = null;
    public bool PlayerTurn { get; set; } = true;

    [SerializeField] SpriteRenderer arrow;

    void Start()
    {
        playerBallShotter = FindObjectOfType<AnotherPlayer_Shotter>();

        arrow.enabled = false;
    }

    void Update()
    {

        if(!playerBallShotter.PlayerTurn)
        {
            len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                
                if (!GameObject.Find("PlayerBall"))
                {
                    Debug.Log("EnemyShotter" + playerBallShotter.PlayerTurn);
                    if (len.y > 1.5f)
                    {
                         AnotherPlayer_Bullet instEnemyBullet = Instantiate(bullet, BulletPos.transform.position, Quaternion.identity);

                        instEnemyBullet.SetMoveDir(len.normalized);

                        
                    }

                }
            }
        
            if (len.y > 1.5f)
            {
                arrow.enabled = true;
                Debug.Log("ø°≥ πÃ Ω¥≈Õ");
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
