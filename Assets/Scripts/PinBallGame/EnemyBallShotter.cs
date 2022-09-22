using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallShotter : MonoBehaviour
{
    public PlayerBullet bullet = null;
    public Transform BulletPos;
    public Transform Pos;
    public float cooltime = 0f;
    public Vector2 len;
    private PlayerBallShotter playerBallShotter = null;
    
    [SerializeField] SpriteRenderer arrow;

    void Start()
    {
        playerBallShotter = FindObjectOfType<PlayerBallShotter>();

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
                         PlayerBullet instEnemyBullet = Instantiate(bullet, BulletPos.transform.position, Quaternion.identity);

                        instEnemyBullet.SetMoveDir(len.normalized);

                        
                    }

                }
            }
        
            if (len.y > 1.5f)
            {
                arrow.enabled = true;
                Debug.Log("���ʹ� ����");
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
