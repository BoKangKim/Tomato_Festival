using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class UIEvent : MonoBehaviourPunCallbacks
{
    bool isHome = false;
    bool isRestart = false;
    public void BtnHome()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            PhotonNetwork.LoadLevel("Start");
        }
        
    }
    public override void OnLeftRoom()
    {
        if (isHome)
        {
            PhotonNetwork.LoadLevel("Start");
        }
    }

    public void BtnRestart()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            if (PhotonNetwork.CountOfPlayersInRooms % 2 == 0 && (PhotonNetwork.CountOfRooms == PhotonNetwork.CountOfPlayersInRooms / 2)) //No room to enter(CountOfPlayersInRooms is even)
            {
                PhotonNetwork.JoinOrCreateRoom($"room{PhotonNetwork.CountOfRooms + 1}", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
            }
            else
            {
                //JoinRoom
                PhotonNetwork.JoinRandomRoom();
            }
        }
        
    }

    public void BtnQuit()
    {
        Application.Quit();
    }
    

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Loading");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.JoinOrCreateRoom($"room{PhotonNetwork.CountOfRooms + 10}", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }
}
