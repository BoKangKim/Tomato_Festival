using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPun
{
    [SerializeField] GameObject pinballGame = null;
    [SerializeField] GameObject battleGame = null;
    [SerializeField] SplitData splitData = null;
    PinBallGame_ItemCheck _itemCheck;
    List<PlayerBattle> players = null;
    AudioControll ac = null;

    public bool isFindPlayers { get; set; } = false;
    float trueY = 0f;
    float falseY = 0f;
    float ScrollSpeed = 5f;

    Vector3 activeTruePos = Vector3.zero;
    Vector3 activeFalsePos = new Vector3(0f, 25f, 0f);

    private void Awake()
    {
        players = new List<PlayerBattle>();
        pinballGame.SetActive(false);
        ac = FindObjectOfType<AudioControll>();
        ac.ChangeBattleMusic();
        _itemCheck = FindObjectOfType<PinBallGame_ItemCheck>();
    }

    void Start()
    {
        battleGame.transform.position = activeTruePos;
        pinballGame.transform.position = activeFalsePos;
    }
    
    public void BattleOver()
    {
        _itemCheck.ResetItem();
        splitData.ResetLists();
        StartCoroutine(BattleOverTransform());
    }
    public void AddPlayers(PlayerBattle player)
    {
        players.Add(player);
    }

    IEnumerator BattleOverTransform()
    {
        while(battleGame.transform.position.y  <= 25f 
            || pinballGame.transform.position.y >= 0f)
        {
            ScrollSpeed += 1f;
            battleGame.transform.Translate(Vector3.up * ScrollSpeed * Time.deltaTime);
            pinballGame.transform.Translate(Vector3.down * ScrollSpeed * Time.deltaTime);
            yield return null;
        }

        battleGame.transform.position = activeFalsePos;
        battleGame.SetActive(false);
        pinballGame.transform.position = activeTruePos;
        pinballGame.SetActive(true);
        battleGame.transform.position = activeFalsePos;
        battleGame.SetActive(false);
        ScrollSpeed = 5f;
        ac.ChangePinballMusic();

        //ScrollSpeed = 5f;
    }

    public void PinballOver()
    {
        splitData.OnDataList();
        StartCoroutine(PinballOverTransform());
    }

    IEnumerator PinballOverTransform()
    {
        while (pinballGame.transform.position.y <= 25f
            || battleGame.transform.position.y >= 0f)
        {
            ScrollSpeed += 1f;
            pinballGame.transform.Translate(Vector3.up * ScrollSpeed * Time.deltaTime);
            battleGame.transform.Translate(Vector3.down * ScrollSpeed * Time.deltaTime);

            yield return null;
        }

        pinballGame.transform.position = activeFalsePos;
        pinballGame.SetActive(false);
        battleGame.transform.position = activeTruePos;
        battleGame.SetActive(true);

        for(int i =0;i < players.Count;i++)
        {
            players[i].gameObject.SetActive(true);
            players[i].photonView.RPC("RPC_ReStart", RpcTarget.Others);
        }

        ac.ChangeBattleMusic();
        ScrollSpeed = 5f;
    }

    
}

