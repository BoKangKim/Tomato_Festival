using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class PinBallManager : Singleton<PinBallManager>
{
    [SerializeField] GameObject P_PlayerBall, P_ParticalRed;
    [SerializeField] GameObject BallPreview, Arrow;
    //[SerializeField] TextMeshProUGUI[] TextInfos;
    [SerializeField] TextMeshProUGUI playCount;
    [SerializeField] TextMeshProUGUI ballCount;
    [SerializeField] GameObject WinnerInfo;
    [SerializeField] TextMeshProUGUI BlockHP;
    [SerializeField] LineRenderer MouseLR, BallLR;
    [SerializeField] GameObject blockSpawner;

    //public Quaternion QI = Quaternion.identity;

    BlockSpawner blockSpawn;

    public int Playcount = 3;
    public int GetPlayercount { get { return Playcount; } }

    bool isGameOver = false;

    void Start()
    {
        playCount.text = "Play Count : " + Playcount;
    }

    public void MinusPlayCount()
    {
        Playcount --;
        playCount.text = "Play Count : " + Playcount;

        // playCount.text = "PlayCount" + Playcount;

        //if ( Playcount == 0 && isGameOver == true)
        //{
        //    playCount.gameObject.SetActive(true);
        //    WinnerInfo.SetActive(true);
        //}
    }

   

    void Update()
    {
        
    }
}
