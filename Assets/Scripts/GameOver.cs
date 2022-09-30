using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
public class GameOver : MonoBehaviourPun
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] Pool pool;
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
            pool.gameObject.SetActive(false);
            GameOvercheck = true;
            WinPanel.SetActive(true);
            StartCoroutine("leave");
        }
        else
        {
            gameControll.BattleOver();
            
        }
    }

    public void SetLoseCount()
    {
        PlayerBattle[] players = FindObjectsOfType<PlayerBattle>();
        //gameControll.SetPlayers(players);

        loseCount = loseCount + 1;
        if (loseCount >= 3)
        {
            GameOvercheck = true;
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
        else
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].SendMessage("PlayerSetActiveFalse", SendMessageOptions.DontRequireReceiver);
            }
            gameControll.BattleOver();

        }
    }
    IEnumerator leave()
    {
        yield return new WaitForSeconds(0.1f);
        PhotonNetwork.LeaveRoom();
    }
}
