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

    [Header("Access")]
    [SerializeField] GameObject accessScroll = null;
    Vector3 accessscrollPos;
    [SerializeField] TMP_InputField myNickName = null;
    [SerializeField] Button matchButton = null;
    [SerializeField] Button backButton = null;

    [SerializeField] TextMeshProUGUI matching = null;

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
        PhotonNetwork.ConnectUsingSettings();
        
        gameLogo.SetActive(true);
        buttonScroll.SetActive(false);
        accessScroll.SetActive(false);
        buttonscrollPos = buttonScroll.transform.position;
        accessscrollPos = accessScroll.transform.position;
        StartCoroutine(SetUI());
    }
    IEnumerator SetUI()
    {
        yield return new WaitForSeconds(0.15f);
        buttonScroll.SetActive(true);
    }
    public override void OnConnectedToMaster()  // Call when the master server is connected
    {
        startButton.interactable = true;
    }
    public override void OnDisconnected(DisconnectCause cause)  // Call when the master server is not connected
    {
        startButton.interactable = false;
        matching.text = "Sorry, The game can't be played because all rooms are currently full.";
        PhotonNetwork.ConnectUsingSettings();
    }

    #region //setting ButtonScroll
    public void OnClickStartButton()
    {
        StartCoroutine(StartButton());
    }
    public void OnClickInfoButton()
    {
        startButton.interactable = false;
        settingButton.interactable = false;
        exitButton.interactable = false;
        PhotonNetwork.LoadLevel("Infomation");
    }

    public void OnClickSettingButton()
    {
        startButton.interactable = false;
        infoButton.interactable = false;
        exitButton.interactable = false;
    }
    public void OnClickExitButton()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        //Application.OpenURL("http://google.com");
    }

    IEnumerator StartButton()
    {
        accessScroll.SetActive(true);
        matchButton.interactable = false;

        float time = 0f;

        while (time <= 0.2f)
        {
            time += Time.deltaTime;

            buttonScroll.transform.Translate(Vector3.left * 8f);
            accessScroll.transform.Translate(Vector3.left * 6f);

            yield return null;
        }
        buttonScroll.SetActive(false);

        yield return null;
    }
    #endregion

    
    public void OnEndEdit(string name)
    {
        if (name.Length < 3 || name.Length > 8)
        {
            myNickName.selectionColor = Color.gray;
            myNickName.text = "Please re-enter a nickname (3 ~ 8)";
            return;
        }
        matchButton.interactable = true;
        PhotonNetwork.NickName = name;
    }
    public void OnClickMatchButton()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.NickName) == true) return;
        
        accessScroll.SetActive(false);

        gameLogo.SendMessage("GameLogoMove", SendMessageOptions.DontRequireReceiver);
        matching.text = "Accessing...";
        StartCoroutine("GoRoom");
    }
    IEnumerator GoRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady == false)
        {
            Debug.Log("연결이 안되어 있었네");
            PhotonNetwork.Reconnect();
        }
        
        yield return new WaitUntil(()=> PhotonNetwork.IsConnectedAndReady == true);
        Debug.Log("연결되어 있음");
        if (PhotonNetwork.CountOfPlayersInRooms % 2 == 0 && (PhotonNetwork.CountOfRooms == PhotonNetwork.CountOfPlayersInRooms / 2)) //No room to enter(CountOfPlayersInRooms is even)
        {
            Debug.Log("방 만들어");
            PhotonNetwork.JoinOrCreateRoom($"room{PhotonNetwork.CountOfRooms + 1}", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
        }
        else
        {
            //Debug.Log("방 들어가");
            //JoinRoom
            PhotonNetwork.JoinRandomRoom();
            //Debug.Log("못들어가");
            matching.text = "Disconnected";
        }
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("방 들어간다~!~!");
        PhotonNetwork.LoadLevel("Loading");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 못 들어간다~!~! 꽉찼다");
        if (returnCode == 32765)
        {
            //Debug.Log("방 못 들어간다~!~! 꽉찼다");
            PhotonNetwork.JoinOrCreateRoom($"room{PhotonNetwork.CountOfRooms + 20}", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
        }
        else
        {
            Debug.Log("방 못 들어간다~!~! 모른다");
            matching.text = $"Connection Failed : <{returnCode}> {message}";
            accessScroll.SetActive(true);
        }
    }
    public void OnClickBackButton()
    {
        accessScroll.transform.position = accessscrollPos;
        accessScroll.SetActive(false);

        buttonScroll.SetActive(true);
        buttonScroll.transform.position = buttonscrollPos;
    }
}
