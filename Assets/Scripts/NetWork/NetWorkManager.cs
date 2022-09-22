using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    [Header("UI Á¤º¸")]
    [SerializeField] TextMeshProUGUI inputNickName = null;
    [SerializeField] Button btnConnect = null;

    void Start()
    {
        btnConnect.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnValueChanged(string inStr)
    {
        if(inStr.Length >= 2)
        {
            btnConnect.interactable = true;
            PhotonNetwork.NickName = inStr;
        }
    }

    public void OnClick_Connect()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.NickName) == true)
            return;

        PhotonNetwork.JoinOrCreateRoom("myroom", new RoomOptions { MaxPlayers = 2 }, null);
        btnConnect.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected Master Server...");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected Master Server..." + " " + cause);
        btnConnect.interactable = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed Connect Room" + " " + message);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Main");
    }
}
