using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject pinballGame = null;
    [SerializeField] GameObject battleGame = null;

    float trueY = 0f;
    float falseY = 0f;
    float ScrollSpeed = 10f;

    Vector3 activeTruePos = Vector3.zero;
    Vector3 activeFalsePos = new Vector3(0f, 25f, 0f);

    private void Awake()
    {
        pinballGame.SetActive(false);
    }
    
    void Start()
    {
        battleGame.transform.position = activeTruePos;
        pinballGame.transform.position = activeFalsePos;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        BattleOver();
    //    }
    //    if (Input.GetKeyDown(KeyCode.N))
    //    {
    //        PinballOver();
    //    }

    //}

    public void BattleOver()
    {
        StartCoroutine(BattleOverTransform());
    }

    IEnumerator BattleOverTransform()
    {
        while(battleGame.transform.position.y  <= 25f 
            || pinballGame.transform.position.y >= 0f)
        {
            battleGame.transform.Translate(Vector3.up * ScrollSpeed * Time.deltaTime);
            pinballGame.transform.Translate(Vector3.down * ScrollSpeed * Time.deltaTime);
            
            yield return null;
        }

        battleGame.transform.position = activeFalsePos;
        battleGame.SetActive(false);
        pinballGame.transform.position = activeTruePos;
        pinballGame.SetActive(true);
    }

    public void PinballOver()
    {
        StartCoroutine(PinballOverTransform());
    }

    IEnumerator PinballOverTransform()
    {
        while (pinballGame.transform.position.y <= 25f
            || battleGame.transform.position.y >= 0f)
        {
            pinballGame.transform.Translate(Vector3.up * ScrollSpeed * Time.deltaTime);
            battleGame.transform.Translate(Vector3.down * ScrollSpeed * Time.deltaTime);

            yield return null;
        }
        pinballGame.transform.position = activeFalsePos;
        pinballGame.SetActive(false);
        battleGame.transform.position = activeTruePos;
        battleGame.SetActive(true);
    }
}

