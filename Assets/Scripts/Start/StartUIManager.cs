using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


using Photon.Pun; // 포톤 라이브러리를 유니티 컴포넌트로 사용할 수 있게 하는 라이브러리
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
        // 게임 시작시 스크린 사이즈를 맞춰줌 16 : 9 사이즈
        Screen.SetResolution(800, 450, false);
        // 네트워크 동기화를 더 쾌적하게 하기위한 설정
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        // 게임 버전 설정
        //PhotonNetwork.GameVersion = gameVersion;
        // 마스터(호스트)가 씬을 이동하면 클라이언트 플레이어들이 자동적으로 싱크된다
        PhotonNetwork.AutomaticallySyncScene = true;
        // 방에서 게임씬으로 로딩될때 방에서 보내는 메세지를 받지 않는다
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

    public override void OnConnectedToMaster()  // 마스터 서버에 접속 성공시에 호출(포톤에서 호출)
    {
        Debug.Log("연결");
        startButton.interactable = true;

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("연결 불안정");
        startButton.interactable = false;
    }
    #region
    public void OnClickStartButton()
    {
        Debug.Log("시작버튼");
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
            Debug.Log("다시 입력");
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


        //룸에 접속 시도 /없으면 걍 CreateRoom
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
        PhotonNetwork.LoadLevel("Main"); // 서버연결된 상태로 씬 전환
    }
    /*
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("마스터 변경 : " + newMasterClient.ToString());
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
