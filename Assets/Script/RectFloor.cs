using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectFloor : MonoBehaviour
{

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
            if (transform.position.y + transform.lossyScale.y / 2 <= collision.transform.position.y - collision.transform.lossyScale.y / 2)
            {
                collision.gameObject.SendMessage("SetininJump", true, SendMessageOptions.DontRequireReceiver);
                // FixedUpdate�� Update���� �ʰ� ó���� �Ͽ��� ������ �� �� ����Ű�� �ٽ� ������ ��
                // �ڴʰ� �� ���� ������ �ϴ� ������ ���� �ϱ����� ����Ű�� ������ ���� �ʱ�ȭ
                collision.gameObject.SendMessage("SetisJump", false, SendMessageOptions.DontRequireReceiver);
            }
           
        }
    }

}
