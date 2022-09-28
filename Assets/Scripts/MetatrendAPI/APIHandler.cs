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
            if(Instance == null)
            {
                Instance = FindObjectOfType<APIHandler>();
                if (Instance == null)
                    Instance = new GameObject("APIHandler").AddComponent<APIHandler>();
            }

            return Instance;
        }
    }
    #endregion

    // TEST 모드 일 때만 나중에 다운받아서 Path 확인 후 고쳐야 됨....

    readonly MODE mode = MODE.TEST;
    string path = "C:/Users/User/AppData/Local/Osiris-SAT/app_launcher.exe";
    string SetupURL = "";
    string FullAppsURL = "";
    // TomatoFestival Project API_KEY
    string API_KEY = "440hXzu38SsiUIBkYiSTlD";

    // Respone 받을 클래스 변수들
    Res_GetUserProfile playerProfile = null;
    Res_GetSessionID sessionId = null;

    string getBaseURL()
    {
        return FullAppsURL;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SetURL();
    }

    void SetURL()
    {
        if(mode == MODE.TEST)
        {
            FullAppsURL = "https://odin-api-sat.browseosiris.com";
            SetupURL = "https://osiris-v2-test.s3.ap-southeast-1.amazonaws.com/osirisR2/sat/Osiris+Setup-Staging+v2.2.2.53.exe";
        }
        else if(mode == MODE.PRODUCTION)
        {
            FullAppsURL = "https://odin-api.browseosiris.com";
            SetupURL = "https://www.browseosiris.com/";
        }
    }

    public void GetUserProfile()
    {
        StartCoroutine(ResGetUserProfile());
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
            if(response != null)
            {
                sessionId = response;
                FindObjectOfType<StartUIManager>().SetStartPanel(playerProfile.userProfile.username, playerProfile.userProfile.email_id, true);
                Debug.Log(sessionId.ToString());
            }
            else
            {
                string Error = "Can't Response SessionID, Please Check Your DappX Wallet And Retry Start";
                FindObjectOfType<StartUIManager>().SetErrorPanel(Error);
            }
        });
    }

    delegate void CallBackUserProfile(Res_GetUserProfile response);
    IEnumerator RequestUserProfile(CallBackUserProfile callback)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8546/api/getuserprofile");
        yield return www.SendWebRequest();
        Res_GetUserProfile resPlayerProfile = JsonUtility.FromJson<Res_GetUserProfile>(www.downloadHandler.text);
        callback(resPlayerProfile);
    }

    delegate void CallBackSessionID(Res_GetSessionID response);
    IEnumerator RequestSessionID(CallBackSessionID callback)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8546/api/getsessionid ");
        yield return www.SendWebRequest();
        Res_GetSessionID resSessionID = JsonUtility.FromJson<Res_GetSessionID>(www.downloadHandler.text);
        callback(resSessionID);
    }

}
