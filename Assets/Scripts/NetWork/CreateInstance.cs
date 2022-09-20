using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

public class CreateInstance : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if(PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Instantiate("Player",Vector3.zero,Quaternion.identity);
            if(PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate("BulletPool", Vector3.zero, Quaternion.identity);
        }
    }
}
