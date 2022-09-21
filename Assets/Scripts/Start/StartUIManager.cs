using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


using Photon.Pun; // ���� ���̺귯���� ����Ƽ ������Ʈ�� ����� �� �ְ� �ϴ� ���̺귯��
using Photon.Realtime;


public class StartUIManager : MonoBehaviourPunCallbacks
{
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

    [Header("In Room")]
    [SerializeField] TextMeshProUGUI matching = null;


    void Awake()
    {
        // ���� ���۽� ��ũ�� ����� ������ 16 : 9 ������
        Screen.SetResolution(800, 450, false);
        // ��Ʈ��ũ ����ȭ�� �� �����ϰ� �ϱ����� ����
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        // ���� ���� ����
        //PhotonNetwork.GameVersion = gameVersion;
        // ������(ȣ��Ʈ)�� ���� �̵��ϸ� Ŭ���̾�Ʈ �÷��̾���� �ڵ������� ��ũ�ȴ�
        PhotonNetwork.AutomaticallySyncScene = true;
        // �濡�� ���Ӿ����� �ε��ɶ� �濡�� ������ �޼����� ���� �ʴ´�
        PhotonNetwork.IsMessageQueueRunning = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

        buttonscrollPos = buttonScroll.transform.position;
        accessscrollPos = accessScroll.transform.position;
        buttonScroll.SetActive(true);
        startButton.interactable = false;
        accessScroll.SetActive(false);
        matching.text = "";
    }

    public override void OnConnectedToMaster()  // ������ ������ ���� �����ÿ� ȣ��(���濡�� ȣ��)
    {
        Debug.Log("����");
        startButton.interactable = true;

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("���� �Ҿ���");
        startButton.interactable = false;
    }
    #region
    public void OnClickStartButton()
    {
        Debug.Log("���۹�ư");
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
            accessScroll.transform.Translate(Vector3.left * 4f);

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
            myNickName.text = "";
            Debug.Log("�ٽ� �Է�");
            return;
        }

        matchButton.interactable = true;
        PhotonNetwork.NickName = name;
        Debug.Log(name);
    }
    public void OnClickMatchButton()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.NickName) == true)
        {
            return;
        }
        accessScroll.SetActive(false);

        

        //�뿡 ���� �õ� /������ �� CreateRoom
        matching.text = "Accessing...";
        PhotonNetwork.JoinOrCreateRoom("myroom", new RoomOptions { MaxPlayers = 2 }, null);
    }
    
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        matching.text = $"Connection Failed : <{returnCode}> {message}";
    }

    public override void OnJoinedRoom()
    {
        matching.text = "Searching for match";
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(GoMain());
        }  
        

    }
    
    IEnumerator GoMain()
    {
        yield return new WaitUntil(()=> PhotonNetwork.CurrentRoom.PlayerCount == 2);
        
        PhotonNetwork.LoadLevel("Main"); // ��������� ���·� �� ��ȯ
    }
    /*
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("������ ���� : " + newMasterClient.ToString());
    }
    */

    public void OnClickBackButton()
    {
        accessScroll.transform.position = accessscrollPos;
        accessScroll.SetActive(false);

        buttonScroll.SetActive(true);
        buttonScroll.transform.position = buttonscrollPos;
    }
}
