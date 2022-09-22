using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


using Photon.Pun;
using Photon.Realtime;

public class StartUIManager : MonoBehaviourPunCallbacks
{
    [Header("Title")]
    [SerializeField] GameObject titleLogo = null;

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
        // Set Screen Size 16 : 9
        Screen.SetResolution(800, 450, false);
        // Improves the performance of the Photon network.
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        
        //PhotonNetwork.GameVersion = gameVersion;
        // Automatically synchronized scenes from other clients when switching scenes from the master server.
        PhotonNetwork.AutomaticallySyncScene = true;
        // Don't get a photonmessage when access the room.
        PhotonNetwork.IsMessageQueueRunning = false;
    }
    
    void Start()
    {
        //Connect network.
        PhotonNetwork.ConnectUsingSettings();
        titleLogo.SetActive(true);

        StartCoroutine(SetUI());
    }
    IEnumerator SetUI()
    {
        yield return new WaitUntil(() => titleLogo.transform.position.x <= -18f);
        buttonscrollPos = buttonScroll.transform.position;
        accessscrollPos = accessScroll.transform.position;

        buttonScroll.SetActive(true);
        accessScroll.SetActive(false);
    }

    public override void OnConnectedToMaster()  // Call when the master server is connected
    {
        startButton.interactable = true;
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        startButton.interactable = false;
    }
    #region //setting ButtonScroll
    public void OnClickStartButton()
    {
        StartCoroutine(StartButton());
    }
    public void OnClickInfoButton()
    {

    }
    public void OnClickSettingButton()
    {

    }
    public void OnClickExitButton()
    {

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
            myNickName.caretColor = Color.gray;
            myNickName.text = "Please re-enter a nickname (3 ~ 8)";
            return;
        }

        matchButton.interactable = true;
        PhotonNetwork.NickName = name;
    }
    public void OnClickMatchButton()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.NickName) == true)
        {
            return;
        }
        accessScroll.SetActive(false);

        titleLogo.SendMessage("GameLogoMove", SendMessageOptions.DontRequireReceiver);
        matching.text = "Accessing...";

        //CreateRoom
        PhotonNetwork.JoinOrCreateRoom("myroom", new RoomOptions { MaxPlayers = 2 }, null);

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        matching.text = $"Connection Failed : <{returnCode}> {message}";
        accessScroll.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Loading");
    }

    public void OnClickBackButton()
    {
        accessScroll.transform.position = accessscrollPos;
        accessScroll.SetActive(false);

        buttonScroll.SetActive(true);
        buttonScroll.transform.position = buttonscrollPos;
    }
}
