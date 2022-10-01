using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UIEvent : MonoBehaviour
{
    public void BtnHomeEvent()
    {
        PhotonNetwork.LoadLevel("Start");
    }

    public void BtnRestartEvent()
    {
        PhotonNetwork.LoadLevel("Loading");
    }

    public void BtnQuitEvent()
    {
        Application.Quit();
    }
}
