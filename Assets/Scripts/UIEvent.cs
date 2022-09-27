using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class UIEvent : MonoBehaviourPunCallbacks
{
    public void BtnHome()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void BtnRestart()
    {
        PhotonNetwork.LoadLevel("Loading");
    }

    public void BtnQuit()
    {
        Application.Quit();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Start");
    }
}
