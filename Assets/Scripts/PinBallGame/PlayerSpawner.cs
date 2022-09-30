using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    GameObject arrow;

    // Start is called before the first frame update
    void Awake()
    {
        if (PhotonNetwork.IsConnected)
        {
             arrow =  PhotonNetwork.Instantiate("Player1_Shotter", new Vector3(0f, -3.5f, 0), Quaternion.identity);
             PlayerBallShotter ps = arrow.GetComponent<PlayerBallShotter>();

            if (PhotonNetwork.IsMasterClient)
            {
                ps.photonView.RPC("SetTurn", RpcTarget.All, true);
                ps.photonView.RPC("SetColor", RpcTarget.All, 1);
            } 
            else if(PhotonNetwork.IsMasterClient == false)
            {
                ps.photonView.RPC("SetTurn", RpcTarget.All, false);
                ps.photonView.RPC("SetColor", RpcTarget.All, 2);
            }

        }

    }
    
}
