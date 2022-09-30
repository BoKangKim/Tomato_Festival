using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
enum MODE 
{
    TEST,
    PRODUCTION,
    MAX
}

public class APIHandler : MonoBehaviour
{
    #region SingleTon
    private static APIHandler Instance = null;

    public static APIHandler Inst
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<APIHandler>();
                if (Instance == null)
                    Instance = new GameObject("APIHandler").AddComponent<APIHandler>();
            }

            return Instance;
        }
    }
    #endregion

    // TEST ��� �� ���� ���߿� �ٿ�޾Ƽ� Path Ȯ�� �� ���ľ� ��....

    readonly MODE mode = MODE.TEST;
    string path = "C:/Users/User/AppData/Local/Osiris-SAT/app_launcher.exe";
    string SetupURL = "";
    string FullAppsURL = "";
    // TomatoFestival Project API_KEY
    string API_KEY = "qSGCSgX53RhbhBBit2u97";

    // Respone ���� Ŭ���� ������
    Res_GetUserProfile playerProfile = null;
    Res_GetSessionID sessionId = null;
    Res_Settings betSettings = null;
    // UI ǥ�ø� ���� Ŭ����
    StartUIManager uiManager = null;

    //Betting ����

    string getBaseURL()
    {
        return FullAppsURL;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        uiManager = FindObjectOfType<StartUIManager>();
        SetURL();
    }

    void SetURL()
    {
        if (mode == MODE.TEST)
        {
            FullAppsURL = "https://odin-api-sat.browseosiris.com";
            SetupURL = "https://osiris-v2-test.s3.ap-southeast-1.amazonaws.com/osirisR2/sat/Osiris+Setup-Staging+v2.2.2.53.exe";
        }
        else if (mode == MODE.PRODUCTION)
        {
            FullAppsURL = "https://odin-api.browseosiris.com";
            SetupURL = "https://www.browseosiris.com/";
        }
    }

    public void GetUserProfile()
    {
        StartCoroutine(ResGetUserProfile());
    }

    public void GetZeraBalaneAndBetSettings()
    {
        StartCoroutine(ResGetBettingSettings());
    }

    IEnumerator ResGetUserProfile()
    {
        yield return RequestUserProfile((response) =>
        {
            if (response != null)
            {
                playerProfile = response;
                StartCoroutine(ResGetSessionID());
                Debug.Log(playerProfile.ToString());
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                    Application.OpenURL(SetupURL);
                }
            }
        });
    }

    IEnumerator ResGetSessionID()
    {
        yield return RequestSessionID((response) =>
        {
            if (response != null)
            {
                sessionId = response;
                uiManager.SetStartPanel(playerProfile.userProfile.username, playerProfile.userProfile.email_id, true);
                Debug.Log(sessionId.ToString());
            }
            else
            {
                string Error = "Can't Response SessionID, Please Check Your DappX Wallet And Retry Start";
                uiManager.SetErrorPanel(Error);
            }
        });
    }

    IEnumerator ResGetZeraBalance()
    {
        yield return RequestZeraBalance(sessionId.sessionId, (response) => { 
            if(response != null)
            {
                bool isPosibleStart = false;
                if(betSettings.data.bets[0].amount > response.data.balance)
                {
                    isPosibleStart = false;
                }
                else if(betSettings.data.bets[0].amount <= response.data.balance)
                {
                    isPosibleStart = true;
                }
                Debug.Log(response.data.balance + " Zera Balance");
                uiManager.SetBalancePanel(response.ToString(), betSettings.data.bets[0].amount.ToString(),isPosibleStart);
            }
            else
            {
                Debug.LogError("Can't Response Zera Balance");
            }
        });
    }

    IEnumerator ResGetBettingSettings()
    {
        yield return RequestBettingSettings((response) =>
        {
            if (response != null)
            {
                Debug.Log(response.ToString());
                betSettings = response;
                StartCoroutine(ResGetZeraBalance());
            }
            else
            {
                Debug.LogError("Can't Response Zera Balance");
            }
        });
    }

    // ���� ������ ��������
    delegate void CallBackUserProfile(Res_GetUserProfile response);
    IEnumerator RequestUserProfile(CallBackUserProfile callback)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8546/api/getuserprofile");
        yield return www.SendWebRequest();
        Res_GetUserProfile resPlayerProfile = JsonUtility.FromJson<Res_GetUserProfile>(www.downloadHandler.text);
        callback(resPlayerProfile);
    }

    // ���Ǿ��̵� ��������
    delegate void CallBackSessionID(Res_GetSessionID response);
    IEnumerator RequestSessionID(CallBackSessionID callback)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8546/api/getsessionid ");
        yield return www.SendWebRequest();
        Res_GetSessionID resSessionID = JsonUtility.FromJson<Res_GetSessionID>(www.downloadHandler.text);
        callback(resSessionID);
    }


    // ��û�� �÷������� ���� ZERA
    delegate void CallBackZeraBalance(Res_BalanceInfo response);
    IEnumerator RequestZeraBalance(string sessionID, CallBackZeraBalance callback)
    {
        string url = getBaseURL() + ("/v1/betting/" + "zera" + "/balance/" + sessionID);
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("api-key",API_KEY);
        yield return www.SendWebRequest();
        Res_BalanceInfo res = JsonUtility.FromJson<Res_BalanceInfo>(www.downloadHandler.text);
        Debug.Log(www.downloadHandler.text);
        callback(res);
    }

    delegate void CallBackBettingSettings(Res_Settings response);
    IEnumerator RequestBettingSettings(CallBackBettingSettings callback)
    {
        string url = getBaseURL() + "/v1/betting/settings";
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("api-key", API_KEY);
        yield return www.SendWebRequest();
        Res_Settings res = JsonUtility.FromJson<Res_Settings>(www.downloadHandler.text);
        callback(res);
    }

    delegate void CallBackPlaceBet(ResBettingPlaceBet response);
    IEnumerator RequestCoinPlaceBet(ReqBettingPlaceBet req, CallBackPlaceBet callback)
    {
        string url = getBaseURL() +"/v1/betting/" + "zera" + "/place-bet";

        string reqJsonData = JsonUtility.ToJson(req);

        yield return null;
    }

}
