using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

public class CreateInstance : MonoBehaviourPunCallbacks
{
    GameObject pool = null;
    GameObject Player1 = null;
    GameObject Player2 = null;

    private void Awake()
    {
        StartCoroutine(CheckPlayerCount());
    }
    void Start()
    {
        if(PhotonNetwork.IsConnected == true)
        {
            pool = FindObjectOfType<Pool>().gameObject;
            pool.SetActive(false);
            if (PhotonNetwork.IsMasterClient)
            {
                Player1 = PhotonNetwork.Instantiate("PlayerBattle", new Vector3(-9f, -6f, 0f), Quaternion.identity);
            }
            else
            {
                Player2 = PhotonNetwork.Instantiate("PlayerBattle", new Vector3(9f, -6f, 0f), Quaternion.identity);
            }

            pool.gameObject.SetActive(true);
        }
    }

    IEnumerator CheckPlayerCount()
    {

        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount != 2);

        pool.gameObject.SetActive(false);
        if (Player1 != null)
            PhotonNetwork.Destroy(Player1.gameObject);
        if (Player2 != null)
            PhotonNetwork.Destroy(Player2.gameObject);

        if(PhotonNetwork.IsMasterClient == true)
        {
            APIHandler.Inst.DeclareWinner(APIHandler.Inst.GetMyProfile().userProfile._id,false);
        }
        else
        {
            yield return new WaitUntil(() => PhotonNetwork.IsMasterClient == true);
            APIHandler.Inst.DeclareWinner(APIHandler.Inst.GetMyProfile().userProfile._id,false);
        }
    }
   
}
