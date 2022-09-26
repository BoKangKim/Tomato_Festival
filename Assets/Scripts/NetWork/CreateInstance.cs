using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

public class CreateInstance : MonoBehaviourPunCallbacks
{
    GameObject pool = null;

    
    private void Awake()
    {
        StartCoroutine(CheckPlayerCount());
    }

    void Start()
    {
        if(PhotonNetwork.IsConnected == true)
        {
            pool = FindObjectOfType<Pool>().gameObject;
            pool.SetActive(false);
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate("Player",new Vector3(-9f,-6f,0f),Quaternion.identity);
            else
                PhotonNetwork.Instantiate("Player",new Vector3(9f,-6f,0f), Quaternion.identity);

            pool.gameObject.SetActive(true);
        }
    }

     IEnumerator CheckPlayerCount()
    {
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount != 2);

        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.LoadLevel("Loading");
    }
    //OnPlayerEnteredRoom(Player newPlayer)
}
