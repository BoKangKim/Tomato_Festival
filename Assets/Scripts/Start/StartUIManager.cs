using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


using Photon.Pun;
using Photon.Realtime;

public class StartUIManager : MonoBehaviourPunCallbacks
{
    [Header("Logo")]
    [SerializeField] GameObject gameLogo = null;

    [Header("Option")]
    [SerializeField] GameObject buttonScroll = null;
    Vector3 buttonscrollPos;
    [SerializeField] Button startButton = null;
    [SerializeField] Button infoButton = null;
    [SerializeField] Button settingButton = null;
    [SerializeField] Button exitButton = null;

    [Header("UserProfile")]
    [SerializeField] GameObject StartPanel;
    [SerializeField] TextMeshProUGUI UserName;
    [SerializeField] TextMeshProUGUI Email_ID;
    [SerializeField] TextMeshProUGUI ErrorMsg;
    [SerializeField] GameObject ErrorPanel;
    [SerializeField] TextMeshProUGUI matching;

    [Header("BetInfo")]
    [SerializeField] GameObject BalancePanel;
    [SerializeField] TextMeshProUGUI ZerBalanceText;
    [SerializeField] TextMeshProUGUI BetText;
    [SerializeField] GameObject CannotStartPanel;

    bool isPosibleStart = false;

    #region Network
    void Awake()
    {
        // Set Screen Size 16 : 9 fullscreen false
        Screen.SetResolution(800, 450, false);
        // Improves the performance of the Photon network.
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        // Automatically synchronized scenes from other clients when switching scenes from the master server.
        PhotonNetwork.AutomaticallySyncScene = true;
        // Don't get a photonmessage when access the room.
        PhotonNetwork.IsMessageQueueRunning = false;
    }
    
    void Start()
    {
        //Connect network.
        APIHandler.Inst.SetUIManager(this);
        PhotonNetwork.ConnectUsingSettings();
        gameLogo.SetActive(true);

        buttonscrollPos = buttonScroll.transform.position;

        StartCoroutine(SetUI());
    }

    // 왜 코루틴??>?????????
    IEnumerator SetUI()
    {
        yield return new WaitForSeconds(0.1f);
        buttonScroll.SetActive(true);
    }

    public override void OnConnectedToMaster()  // Call when the master server is connected
    {
        startButton.interactable = true;
    }
    public override void OnDisconnected(DisconnectCause cause)  // Call when the master server is not connected
    {
        startButton.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion

    // StartPanel Setting
    public void SetStartPanel(string UserName, string Email_ID, bool StartPanel) 
    {
        this.UserName.text = UserName;
        this.Email_ID.text = Email_ID;
        this.StartPanel.SetActive(StartPanel);
    }

    public void SetErrorPanel(string msg)
    {
        this.ErrorMsg.text = msg;
        this.ErrorPanel.SetActive(true);
    }

    public void SetBalancePanel(string zeraBalance,string bet, bool isPosibleStart)
    {
        this.ZerBalanceText.text = zeraBalance;
        this.BetText.text = "BET : " + bet;
        this.BalancePanel.SetActive(true);
        this.StartPanel.SetActive(false);
        this.isPosibleStart = isPosibleStart;
    }

    public void OnClickAcceptButton()
    {
        APIHandler.Inst.GetZeraBalaneAndBetSettings();
    }

    public void OnClickMatchButton()
    {
        if(isPosibleStart == false)
        {
            BalancePanel.SetActive(false);
            CannotStartPanel.SetActive(true);
        }
        else if(isPosibleStart == true)
        {
            gameLogo.SendMessage("GameLogoMove", SendMessageOptions.DontRequireReceiver);
            matching.text = "Accessing...";

            if (PhotonNetwork.CountOfPlayersInRooms % 2 == 0) //No room to enter(CountOfPlayersInRooms is even)
            {
                if (PhotonNetwork.CountOfRooms > 10) // CountOfPlayers > 20
                {
                    matching.text = "Sorry, The game can't be played because all rooms are currently full.";
                }
                else
                {
                    PhotonNetwork.JoinOrCreateRoom($"room{PhotonNetwork.CountOfRooms + 1}", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
                }
            }
            else
            {
                //JoinRoom
                PhotonNetwork.JoinRandomRoom();
            }
        }
    }

    public void OnClickCloseBtn()
    {
        if(StartPanel.activeSelf == true)
            StartPanel.SetActive(false);
        else if (ErrorPanel.activeSelf == true)
            ErrorPanel.SetActive(false);
        else if (BalancePanel.activeSelf == true)
            BalancePanel.SetActive(false);
        else if (CannotStartPanel.activeSelf == true)
            CannotStartPanel.SetActive(false);
    }

    #region //setting ButtonScroll
    public void OnClickStartButton()
    {
        APIHandler.Inst.GetUserProfile();
    }
    public void OnClickInfoButton()
    {
        //startButton.interactable = false;
        //settingButton.interactable = false;
        //exitButton.interactable = false;
    }
    public void OnClickSettingButton()
    {
        //startButton.interactable = false;
        //infoButton.interactable = false;
        //exitButton.interactable = false;
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }
    #endregion

    

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Loading");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        matching.text = $"Connection Failed : <{returnCode}> {message}";
    }
    public void OnClickBackButton()
    {
        buttonScroll.SetActive(true);
        buttonScroll.transform.position = buttonscrollPos;
    }
}
