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

    // Start is called before the first frame update
    void Start()
    {
        loadingEffect.SetActive(true);
        clickEffect.SetActive(true);
        loadingSlider.gameObject.SetActive(true);
        matching.text = "Searching for match";

        loadingSlider.value = 0f;
        StartCoroutine("loadingSliderValueChange");
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2) //MasterClient
        {
            StartCoroutine("GoMain");
        }
        else
        {
            loadingEffect.SendMessage("Pouring", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator loadingSliderValueChange() //changing loadingbar's value
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
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount == 2); //completed Matching

        loadingEffect.SendMessage("Pouring", SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(2f);
        PhotonNetwork.LoadLevel("Main_Scroll"); // load main scene
    }
    public void OnClickBackStartSButton()
    {
        StopCoroutine("loadingSliderValueChange");
        StopCoroutine("GoMain");
        PhotonNetwork.LeaveRoom();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("�� �����ٰ� ������ �����ߴ�");
        PhotonNetwork.LoadLevel("Start");
    }


}
