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

    // Start is called before the first frame update
    void Start()
    {
        loadingEffect.SetActive(true);
        clickEffect.SetActive(true);
        loadingSlider.gameObject.SetActive(true);
        matching.text = "Searching for match";

        StartCoroutine(loadingSliderValueChange());

        Debug.Log("총 플레이어 수" + PhotonNetwork.CountOfPlayers);
        
        Debug.Log("현재방 플레이어 수" + PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log(PhotonNetwork.CurrentRoom.Name);

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
        while (PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            if (loadingSlider.value > 0.7f)
            {
                if (loadingSlider.value > 0.8f)
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
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            loadingSlider.value = 1f;
        }
    }

    IEnumerator GoMain()
    {
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount == 2);
        loadingEffect.SendMessage("Pouring", SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(2f);
        PhotonNetwork.LoadLevel("Main"); // ????????? ???·? ?? ???
    }

    private void Update()
    {
        Debug.Log("방에 들어간 총 플레이어 수" + PhotonNetwork.CountOfPlayersInRooms);
    }
    /*
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("?????? ???? : " + newMasterClient.ToString());
    }
    
    // 방 정보가 바뀔때 자동으로 호출되는 함수
    // 로비에 접속 시
    // 새로운 룸이 만들어질 경우
    // 룸이 삭제되는 경우
    // 룸의 IsOpen 값이 변화할 경우
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        
    }
    // 방에서 나가고 실행되는 콜백함수
    public override void OnLeftRoom()
    {
        
        // 마스터가 방을 나가고 콜백함수가 실행되면
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Main");
        }
    }
    */

}
