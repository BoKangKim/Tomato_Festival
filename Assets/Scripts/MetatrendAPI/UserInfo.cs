using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class UserInfo : MonoBehaviourPunCallbacks
{
    LoadingManager loadMng = null;
    bool isAccept = false;
    int acceptCount = 0;

    private void Awake()
    {
        loadMng = FindObjectOfType<LoadingManager>();
        if (photonView.IsMine == true)
        {
            photonView.RPC("SendUserInfos", RpcTarget.MasterClient, APIHandler.Inst.GetMyProfile().userProfile.username, APIHandler.Inst.GetMySessionID().sessionId);
        }
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
    void SetBettingID(string betting_id)
    {
        APIHandler.Inst.SetBettingID(betting_id);
        if (PhotonNetwork.IsMasterClient == false)
            Debug.Log(APIHandler.Inst.GetBettingID());
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

    public void AcceptLoadGame()
    {
        if(isAccept == false)
        {
            isAccept = true;
            photonView.RPC("AsyncAcceptCount",RpcTarget.All, 1);
        }
        else
        {
            isAccept = false;
            photonView.RPC("AsyncAcceptCount", RpcTarget.All, -1);
        }
    }

    [PunRPC]
    void AsyncAcceptCount(int count)
    {
        this.acceptCount += count;
        Debug.Log(acceptCount);
        if(this.acceptCount == 2)
        {
            PhotonNetwork.LoadLevel("Main_Scroll");
        }
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
