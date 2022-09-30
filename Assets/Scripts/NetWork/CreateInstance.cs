using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

public class CreateInstance : MonoBehaviourPunCallbacks
{
    GameObject pool = null;
    GameObject Player1 = null;
    GameObject Player2 = null;
    GameOver gameOver = null;

    private void Awake()
    {
        gameOver = FindObjectOfType<GameOver>();
        StartCoroutine("CheckPlayerCount");
    }
    void Start()
    {
        if(PhotonNetwork.IsConnected == true)
        {
            pool = FindObjectOfType<Pool>().gameObject;
            pool.SetActive(false);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("CreateInstance");
                Player1 = PhotonNetwork.Instantiate("PlayerBattle", new Vector3(-10f, -6.7f, 0f), Quaternion.identity);
            }
            else
            {
                Debug.Log("CreateInstance");
                Player2 = PhotonNetwork.Instantiate("PlayerBattle", new Vector3(10f, -6.7f, 0f), Quaternion.identity);
            }
            pool.gameObject.SetActive(true);
        }
    }

    IEnumerator CheckPlayerCount()
    {
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount != 2);
        if (gameOver.GameOvercheck)
        {
            yield break;
        }
        pool.gameObject.SetActive(false);
        if (Player1 != null)
            PhotonNetwork.Destroy(Player1.gameObject);
        if (Player2 != null)
            PhotonNetwork.Destroy(Player2.gameObject);

        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.LoadLevel("Loading");
    }

}
