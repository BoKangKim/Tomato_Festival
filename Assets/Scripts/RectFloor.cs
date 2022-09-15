using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectFloor : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("SetIsJump", false, SendMessageOptions.DontRequireReceiver);

            if (transform.position.y + transform.lossyScale.y / 2 > collision.transform.position.y - collision.transform.lossyScale.y / 2)
            {
                collision.gameObject.SendMessage("SetIsFall", true, SendMessageOptions.DontRequireReceiver);
                collision.gameObject.SendMessage("StartFall", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                collision.gameObject.SendMessage("SetIsFall", false, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("SetIsFall", true, SendMessageOptions.DontRequireReceiver);
            collision.gameObject.SendMessage("StartFall", SendMessageOptions.DontRequireReceiver);
        }
    }

}
