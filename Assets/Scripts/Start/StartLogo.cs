using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLogo : MonoBehaviour
{
    [Header("Logo")]
    [SerializeField] GameObject gameLogoimg = null;
    [SerializeField] GameObject uiManager = null;
    [SerializeField] GameObject clickEffect = null;
    private SpriteRenderer logoImg = null;

    [SerializeField] AnimationCurve AlphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(3f, 150 / 255f)});

    float time = 0f;
    Color oricolor;
    Color curcolor;

    void Awake()
    {
        logoImg = gameLogoimg.GetComponent<SpriteRenderer>();
        gameLogoimg.SetActive(false);
        uiManager.SetActive(false);
        clickEffect.SetActive(false);

    }
    private void Start()
    {
        oricolor = new Color(1f, 1f, 1f, 0f);
        curcolor = new Color(1f, 1f, 1f, 0f);
        time = 0f;
        StartCoroutine(StartGameLogo());
    }

    IEnumerator StartGameLogo()
    {
        gameLogoimg.SetActive(true);

        while (true)
        {
            // AlphaCurve
            curcolor.a = AlphaCurve.Evaluate(time);
            // Alpha 변경
            logoImg.color = curcolor;

            // 시간 누적
            time += Time.deltaTime;

            // AnimationCurve 마지막 시간
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
            
            gameLogoimg.transform.localScale = Vector3.Lerp(gameLogoimg.transform.localScale, new Vector3(1.5f, 1.5f, 1f), Time.deltaTime*3);
            gameLogoimg.transform.position = Vector3.Lerp(logoImg.transform.position, new Vector3(-20f, 10f, 0f), Time.deltaTime*3);

            yield return null;
        }
        
        uiManager.gameObject.SetActive(true);
        clickEffect.SetActive(true);
    }


}
