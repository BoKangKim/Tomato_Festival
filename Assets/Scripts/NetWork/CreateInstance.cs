using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

public class CreateInstance : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject pool;

    void Start()
    {
        if(PhotonNetwork.IsConnected == true)
        {
            if(PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate("Player",new Vector3(-9f,-6f,0f),Quaternion.identity);
            else
                PhotonNetwork.Instantiate("Player",new Vector3(9f,-6f,0f), Quaternion.identity);

            pool.gameObject.SetActive(true);
        }
    }
}
