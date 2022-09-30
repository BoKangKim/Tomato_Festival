using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class RectFloor : MonoBehaviourPun
{
    BoxCollider2D floorCollider = null;

    private void Awake()
    {
        floorCollider = GetComponent<BoxCollider2D>();
    }
    // �÷��̾ ���� ��Ҵ� �� �Ǵ��ϱ� ���� �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // �� ���鿡 ����� �� ������ �Ǵ� ���� ���� ���� �Ǵ��ϴ� if��
            // �÷��̾�� ������ �ؼ� ��ֹ� ���� �ö�
            // -> ���� ���� ���� �÷��̾��� �Ʒ����� ���� ������ �� ���� �ִ� ����
            // ������Ʈ�� �߾� y �� + Y�� ������ /2 �� �ϸ� ������ �� ���� ����
            // �Ȱ��� �÷��̾ �����ϸ� �� �Ʒ��� �ִ� ���� ����
            // �� ���� ���� ��ġ�� �Ǵ�

            CircleCollider2D playerCollider = collision.gameObject.GetComponent<CircleCollider2D>();
            if (transform.position.y + floorCollider.size.y / 2 <= collision.transform.position.y - playerCollider.radius)
            {
                collision.gameObject.SendMessage("SetininJump", true, SendMessageOptions.DontRequireReceiver);
                // FixedUpdate�� Update���� �ʰ� ó���� �Ͽ��� ������ �� �� ����Ű�� �ٽ� ������ ��
                // �ڴʰ� �� ���� ������ �ϴ� ������ ���� �ϱ����� ����Ű�� ������ ���� �ʱ�ȭ
                collision.gameObject.SendMessage("SetisJump", false, SendMessageOptions.DontRequireReceiver);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            PhotonNetwork.Destroy(collision.gameObject);
        }
    }

}
