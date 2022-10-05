using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogo : MonoBehaviour
{
    private SpriteRenderer logoImg = null;

    float time = 0f;
    Color curcolor;

    Vector3 startLogoPos;
    Vector3 startLogoScale;

    Vector3 gameLogoPos;
    Vector3 gameLogoScale;


    void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        logoImg = GetComponent<SpriteRenderer>();

        startLogoPos = transform.position;
        startLogoScale = transform.lossyScale;

        gameLogoPos = new Vector3(32.5f, 14f, 0f);
        gameLogoScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    public void GameLogoMove()
    {
        transform.position = gameLogoPos;
        transform.localScale = gameLogoScale;
    }
    public void GameLogoStartPos()
    {
        transform.position = startLogoPos;
        transform.localScale = startLogoScale;
    }
}
