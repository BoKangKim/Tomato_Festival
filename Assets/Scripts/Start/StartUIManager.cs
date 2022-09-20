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

    [SerializeField] TextMeshProUGUI matching = null;
    
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
    public override void OnConnectedToMaster()  // 마스터 서버 접속 성공시에 호출(포톤에서 호출)
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

        while (time <= 0.25f)
        {
            time += Time.deltaTime;

            buttonScroll.transform.Translate(Vector3.left * 8f);
            accessScroll.transform.Translate(Vector3.left * 8f);

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
        if (PhotonNetwork.IsMasterClient)
            matching.text = "Searching for match";
        else
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
