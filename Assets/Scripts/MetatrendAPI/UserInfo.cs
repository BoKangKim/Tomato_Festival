using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class UserInfo : MonoBehaviourPunCallbacks
{
    LoadingManager loadMng = null;

    private void Awake()
    {
        loadMng = FindObjectOfType<LoadingManager>();
        if (photonView.IsMine == true)
        {
            photonView.RPC("SendUserInfos", RpcTarget.MasterClient, APIHandler.Inst.GetMyProfile().userProfile.username, APIHandler.Inst.GetMySessionID().sessionId);
        }
    }

    void Start()
    {

    }

    [PunRPC]
    void SendUserInfos(string userName, string sessionID)
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            APIHandler.Inst.SetOtherPlayerInfo(userName, sessionID);
            photonView.RPC("SetUserInfos", RpcTarget.All, APIHandler.Inst.GetMyProfile().userProfile.username, APIHandler.Inst.GetOhterProfile());
            APIHandler.Inst.ReqBetting();
        }
    }

    [PunRPC]
    void SetUserInfos(string player1Name, string player2Name)
    {
        loadMng.SetVSPanel(player1Name,player2Name);
    }
    
    [PunRPC]
    void DestroyThisObj()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }

    [PunRPC]
    void BettingDisconnect()
    {
        APIHandler.Inst.BettingDisconnect();
    }

    [PunRPC]
    void CancleMatch()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Start");
    }
}
