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
    [SerializeField] GameObject loadingEffect = null;
    [SerializeField] TextMeshProUGUI matching = null;
    [SerializeField] Slider loadingSlider = null;



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

        loadingEffect.SetActive(false);
        loadingSlider.gameObject.SetActive(false);
        matching.text = "";
    }

    public override void OnConnectedToMaster()  // ������ ������ ���� �����ÿ� ȣ��(���濡�� ȣ��)
    {
        startButton.interactable = true;

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        startButton.interactable = false;
    }
    #region
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

        loadingEffect.SetActive(true);

        loadingSlider.gameObject.SetActive(true);
        StartCoroutine(loadingSliderValueChange());

        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            StartCoroutine(GoMain());
        }
        else
        {
            loadingEffect.SendMessage("Pouring", SendMessageOptions.DontRequireReceiver);
        }
    }
    IEnumerator loadingSliderValueChange()//changing loadingbar's value
    {
        while(PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            if(loadingSlider.value > 0.7f)
            {
                if(loadingSlider.value > 0.8f)
                {
                    if (loadingSlider.value > 0.9f)
                    {
                        loadingSlider.value += 0.00001f;
                    }
                    else
                        loadingSlider.value += 0.0001f;
                }
                else
                    loadingSlider.value += 0.001f;
            }
            else
                loadingSlider.value += 0.01f;
            yield return new WaitForSeconds(0.5f);
        }
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            loadingSlider.value = 1f;
        }
    }

    IEnumerator GoMain()
    {
        yield return new WaitUntil(()=> PhotonNetwork.CurrentRoom.PlayerCount == 2);
        loadingEffect.SendMessage("Pouring", SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(2f);
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
