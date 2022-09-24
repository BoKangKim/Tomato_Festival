using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogo : MonoBehaviour
{
    private SpriteRenderer logoImg = null;

    float time = 0f;
    Color curcolor;

    Vector3 setLogoPos;
    Vector3 setLogoScale;

    Vector3 gameLogoPos;
    Vector3 gameLogoScale;

    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        logoImg = GetComponent<SpriteRenderer>();

        setLogoPos = new Vector3(-20f, 10f, 0f);
        setLogoScale = new Vector3(1.5f, 1.5f, 1f);

        gameLogoPos = new Vector3(30f, 17f, 0f);
        gameLogoScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    private void Start()
    {
        transform.localScale = setLogoScale;
        transform.position = setLogoPos;

    }


    public void GameLogoMove()
    {
        transform.position = gameLogoPos;
        transform.localScale = gameLogoScale;
    }
    
}
