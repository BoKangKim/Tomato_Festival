using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLogo : MonoBehaviour
{
    private SpriteRenderer logoImg = null;

    [SerializeField] AnimationCurve AlphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(2f, 1f), new Keyframe(3f, 0f) });

    Vector3 titleLogoPos;
    Vector3 titleLogoScale;

    float time = 0f;
    Color curcolor;

    void Awake()
    {
        logoImg = GetComponent<SpriteRenderer>();

        titleLogoPos = new Vector3(0f, 0f, 0f);
        titleLogoScale = new Vector3(3f, 3f, 1f);
    }

    void Start()
    {
        StartCoroutine(TitleGameLogo());
    }

    IEnumerator TitleGameLogo()
    {
        time = 0f;
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
                yield return new WaitForSeconds(0.01f);
                SceneManager.LoadScene("Start");
            }
            yield return null;
        }
    }
}
