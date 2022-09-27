using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class RectFloor : MonoBehaviourPun
{

    // �÷��̾ ���� ��Ҵ� �� �Ǵ��ϱ� ���� �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBattle")
        {
            // �� ���鿡 ����� �� ������ �Ǵ� ���� ���� ���� �Ǵ��ϴ� if��
            // �÷��̾�� ������ �ؼ� ��ֹ� ���� �ö�
            // -> ���� ���� ���� �÷��̾��� �Ʒ����� ���� ������ �� ���� �ִ� ����
            // ������Ʈ�� �߾� y �� + Y�� ������ /2 �� �ϸ� ������ �� ���� ����
            // �Ȱ��� �÷��̾ �����ϸ� �� �Ʒ��� �ִ� ���� ����
            // �� ���� ���� ��ġ�� �Ǵ�
            if (transform.position.y + transform.lossyScale.y / 2 <= collision.transform.position.y - 0.5f)
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
