using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


using Photon.Pun;
using Photon.Realtime;

public class LoadingManager : MonoBehaviourPunCallbacks
{
    [Header("In Room")]
    [SerializeField] GameObject loadingEffect = null;
    [SerializeField] GameObject clickEffect = null;
    [SerializeField] TextMeshProUGUI matching = null;
    [SerializeField] Slider loadingSlider = null;
    [SerializeField] Button backStartSButton = null;

    [Header("User Info")]
    [SerializeField] GameObject VSPanel = null;
    [SerializeField] TextMeshProUGUI Player1Info = null;
    [SerializeField] TextMeshProUGUI Player2Info = null;
   
    // Start is called before the first frame update
    void Start()
    {
        loadingEffect.SetActive(true);
        clickEffect.SetActive(true);
        
        matching.text = "Searching for match";

        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if(PhotonNetwork.IsMasterClient == false)
                PhotonNetwork.Instantiate("SendUserInfo",Vector3.zero,Quaternion.identity);
        }
        else
        {
            loadingEffect.SendMessage("Pouring", SendMessageOptions.DontRequireReceiver);
        }

        StartCoroutine(CheckPlayerCountTwo());
    }

    public void SetVSPanel(string player1Name, string player2Name)
    {
        Player1Info.text = player1Name;
        Player2Info.text = player2Name;
        VSPanel.SetActive(true);
    }

    public void SetVSInit()
    {
        Player1Info.text = "";
        Player2Info.text = "";
        VSPanel.SetActive(false);
    }

    #region Roading Slider
    //IEnumerator loadingSliderValueChange() //changing loadingbar's value
    //{
    //    while (PhotonNetwork.CurrentRoom.PlayerCount != 2)
    //    {
    //        if (loadingSlider.value > 0.7f)
    //        {
    //            if (loadingSlider.value > 0.8f)
    //            {
    //                if (loadingSlider.value > 0.9f)
    //                {
    //                    loadingSlider.value += 0.00001f;
    //                }
    //                else
    //                    loadingSlider.value += 0.0001f;
    //            }
    //            else
    //                loadingSlider.value += 0.001f;
    //        }
    //        else
    //            loadingSlider.value += 0.01f;
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //    if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
    //    {
    //        loadingSlider.value = 1f;
    //    }
    //}
    #endregion

    IEnumerator GoMain()
    {
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount == 2); //completed Matching

        loadingEffect.SendMessage("Pouring", SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(2f);
        PhotonNetwork.LoadLevel("BKMAIN"); // load main scene
    }

    public void OnClickBackStartSButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnClickCancleButton()
    {
        UserInfo info = FindObjectOfType<UserInfo>();

        if (info != null)
        {
            info.photonView.RPC("BettingDisconnect", RpcTarget.MasterClient);
        }
    }

    public void OnClickAcceptButton()
    {
        UserInfo info = FindObjectOfType<UserInfo>();

        if(info != null)
        {
            info.AcceptLoadGame();
        }
    }

    IEnumerator CheckPlayerCountTwo()
    {
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount == 2);
        StartCoroutine(CheckPlayerCount());
    }

    IEnumerator CheckPlayerCount()
    {
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount != 2);

        if (PhotonNetwork.IsMasterClient)
        {
            APIHandler.Inst.BettingDisconnect();
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            yield return new WaitUntil(() => PhotonNetwork.IsMasterClient == true);
            APIHandler.Inst.BettingDisconnect();
            PhotonNetwork.LeaveRoom();
        }

    }


    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Start");
    }
}
