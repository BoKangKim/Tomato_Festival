using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class RectFloor : MonoBehaviourPun
{

    // 플레이어가 땅에 닿았는 지 판단하기 위한 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBattle")
        {
            // 벽 옆면에 닿았을 때 점프가 되는 것을 막기 위해 판단하는 if문
            // 플레이어는 점프를 해서 장애물 위에 올라감
            // -> 벽의 윗면 보다 플레이어의 아랫면이 위에 있으면 벽 위에 있는 것임
            // 오브젝트의 중앙 y 값 + Y축 스케일 /2 를 하면 윗면의 한 점이 나옴
            // 똑같이 플레이어도 적용하면 맨 아래에 있는 점이 나옴
            // 두 개의 점의 위치로 판단
            if (transform.position.y + transform.lossyScale.y / 2 <= collision.transform.position.y - 0.5f)
            {
                collision.gameObject.SendMessage("SetininJump", true, SendMessageOptions.DontRequireReceiver);
                // FixedUpdate가 Update보다 늦게 처리를 하여서 점프를 한 후 점프키를 다시 눌렀을 때
                // 뒤늦게 한 번더 점프를 하는 현상을 방지 하기위해 점프키가 눌리는 것을 초기화
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
