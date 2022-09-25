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
        DontDestroyOnLoad(gameObject);

        logoImg = GetComponent<SpriteRenderer>();

        startLogoPos = new Vector3(-20f, 10f, 0f);
        startLogoScale = new Vector3(1.5f, 1.5f, 1f);

        gameLogoPos = new Vector3(33f, 17f, 0f);
        gameLogoScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    private void Start()
    {
        transform.localScale = startLogoScale;
        transform.position = startLogoPos;
    }


    public void GameLogoMove()
    {
        transform.position = gameLogoPos;
        transform.localScale = gameLogoScale;
    }
}
