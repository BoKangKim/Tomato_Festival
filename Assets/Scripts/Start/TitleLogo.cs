using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogo : MonoBehaviour
{
    private SpriteRenderer logoImg = null;

    [SerializeField] AnimationCurve AlphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(2f, 1f), new Keyframe(3f, 0f)});

    float time = 0f;
    Color oricolor;
    Color curcolor;

    Vector3 titleLogoPos;
    Vector3 titleLogoScale;

    Vector3 setLogoPos;
    Vector3 setLogoScale;

    Vector3 gameLogoPos;
    Vector3 gameLogoScale;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        logoImg = GetComponent<SpriteRenderer>();

        titleLogoPos = new Vector3(0f, 0f, 0f);
        titleLogoScale = new Vector3(3f, 3f, 1f);
        
        setLogoPos = new Vector3(-20f, 10f, 0f);
        setLogoScale = new Vector3(1.5f, 1.5f, 1f);

        gameLogoPos = new Vector3(30f, 17f, 0f);
        gameLogoScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
    
    private void Start()
    {
        transform.position = titleLogoPos;
        transform.localScale = titleLogoScale;

        StartCoroutine(TitleGameLogo());
    }

    IEnumerator TitleGameLogo()
    {
        time = 0f;
        oricolor = new Color(1f, 1f, 1f, 0f);
        curcolor = new Color(1f, 1f, 1f, 0f);

        while (true)
        {
            // AlphaCurve
            curcolor.a = AlphaCurve.Evaluate(time);
            // Change a titleloge's Alpha Value
            logoImg.color = curcolor;

            //Count time
            time += Time.deltaTime;

            // Check AnimationCurve lasttime
            if (AlphaCurve.keys[AlphaCurve.keys.Length - 1].time <= time)
            {
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(GameLogoSetting());
                yield break;
            }
            yield return null;
        }
        
    }

    IEnumerator GameLogoSetting()
    {
        time = 0f;
        curcolor.a = 1f;
        logoImg.color = curcolor;

        while (time <= 1.5f)
        {
            time += Time.deltaTime;

            transform.localScale = Vector3.Lerp(transform.localScale, setLogoScale, Time.deltaTime * 3);
            transform.position = Vector3.Lerp(logoImg.transform.position, setLogoPos, Time.deltaTime * 3);

            yield return null;
        }
    }

    public void GameLogoMove()
    {
        transform.position = gameLogoPos;
        transform.localScale = gameLogoScale;
    }

}
