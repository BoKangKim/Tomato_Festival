using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 moveDir = Vector3.zero;
    Player enemy;

    public Vector3 MoveDir 
    {
        set
        {
            moveDir = value;
        }
    }

    public Player Enemy
    {
        set
        {
            enemy = value;
        }
    }

    void FixedUpdate()
    {
        if(enemy.gameObject.transform.lossyScale.x / 2 + gameObject.transform.lossyScale.x /2 >=
            Vector3.Distance(enemy.gameObject.transform.position, gameObject.transform.position))
        {
            enemy.SendMessage("StartKnockBackCoroutine",transform.position,SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
        transform.Translate(moveDir * Time.deltaTime * 10f);
    }
    
}
