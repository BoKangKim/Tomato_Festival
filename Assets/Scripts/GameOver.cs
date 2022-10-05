using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Photon.Pun;
public class GameOver : MonoBehaviourPun
{
    [SerializeField] GameObject BattleGame;
    [SerializeField] GameObject PinballGame;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] Pool pool;
    [SerializeField] TextMeshProUGUI WinnerAmount;
    [SerializeField] TextMeshProUGUI LoseCoin;
    PlayerBattle[] players = null;
    GameController gameControll = null;

    int winCount = 0;
    int loseCount = 0;
    public bool GameOvercheck { get; private set; } = false;

    private void Awake()
    {
        gameControll = FindObjectOfType<GameController>();
        
    }



    private void OnDisable()
    {
        winCount = 0;
        loseCount = 0;
    }
        
    public void SetWinCount()
    {
        winCount = winCount + 1;
        if (winCount >= 3)
        {
            photonView.RPC("Winner",RpcTarget.MasterClient, APIHandler.Inst.GetMyProfile().userProfile._id);
        }
        else
        {
            gameControll.BattleOver();
        }
    }

    [PunRPC]
    public void Winner(string winner_player_id)
    {
        APIHandler.Inst.DeclareWinner(winner_player_id,true);
    }
    public void SetLoseCount()
    {
        if (players == null)
            players = FindObjectsOfType<PlayerBattle>();
        //gameControll.SetPlayers(players);

        loseCount = loseCount + 1;
        if (loseCount < 3)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].SendMessage("PlayerSetActiveFalse", SendMessageOptions.DontRequireReceiver);
            }
            gameControll.BattleOver();
        }
    }

    [PunRPC]
    void SetEndGame(string winAmount,string loseCoin)
    {
        APIHandler.Inst.InitSetValues();
        if (winCount >= 3)
        {
            WinGame(winAmount);
        }
        else if(loseCount >= 3)
        {
            LoseGame(loseCoin);
        }
    }


    void WinGame(string winAmount)
    {
        pool.gameObject.SetActive(false);
        WinnerAmount.text = "WIN AMOUNT : " + winAmount;
        GameOvercheck = true;
        WinPanel.SetActive(true);
        StartCoroutine("leave");
    }

    void LoseGame(string loseCoin)
    {
        GameOvercheck = true;
        LoseCoin.text = "LOSE COIN : " + loseCoin;
        LosePanel.SetActive(true);
        pool.gameObject.SetActive(false);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].photonView.IsMine)
                PhotonNetwork.Destroy(players[i].gameObject);
            else
                players[i].SendMessage("DestroyPlayer", SendMessageOptions.DontRequireReceiver);
        }

        StartCoroutine("leave");
    }

    IEnumerator leave()
    {
        yield return new WaitForSeconds(0.1f);
        PhotonNetwork.LeaveRoom();
    }

    public void LeftOnePlayerWin(string WinAmount)
    {
        BattleGame.SetActive(false);
        PinballGame.SetActive(false);

        if (WinPanel.activeSelf == false)
        {
            this.WinnerAmount.text = "WIN AMOUNT : " + WinAmount;
            WinPanel.SetActive(true);
        }
    }
}
