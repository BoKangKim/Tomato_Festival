using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

public class CreateInstance : MonoBehaviourPunCallbacks
{
    GameObject pool = null;

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

}
