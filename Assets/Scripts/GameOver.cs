using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
public class GameOver : MonoBehaviourPun
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] Pool pool;
    int winCount = 0;
    int loseCount = 0;


    private void OnDisable()
    {
        winCount = 0;
        loseCount = 0;
    }

    public void SetWinCount()
    {
        winCount = winCount + 3;
        if(winCount >= 3)
        {
            WinPanel.SetActive(true);
            pool.gameObject.SetActive(false);
        }
    }

    public void SetLoseCount()
    {
        loseCount = loseCount + 3;
        if (loseCount >= 3)
        {
            LosePanel.SetActive(true);
            pool.gameObject.SetActive(false);
            Player[] players = FindObjectsOfType<Player>();
            Debug.Log(players.Length);
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].photonView.IsMine)
                    PhotonNetwork.Destroy(players[i].gameObject);
                else
                    players[i].SendMessage("DestroyPlayer",SendMessageOptions.DontRequireReceiver);
            }
            
        }
    }

}
