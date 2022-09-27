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
            WinPanel.SetActive(true);
            pool.gameObject.SetActive(false);
        }
        else
        {
            gameControll.BattleOver();
        }
    }

    public void SetLoseCount()
    {
        PlayerBattle[] players = FindObjectsOfType<PlayerBattle>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].photonView.IsMine)
                PhotonNetwork.Destroy(players[i].gameObject);
            else
                players[i].SendMessage("DestroyPlayer", SendMessageOptions.DontRequireReceiver);
        }

        loseCount = loseCount + 1;
        if (loseCount >= 3)
        {
            LosePanel.SetActive(true);
            pool.gameObject.SetActive(false);
        }
        else
        {
            gameControll.BattleOver();
        }
    }

}
