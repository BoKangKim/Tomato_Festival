using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTomato : MonoBehaviour
{
    private SpriteRenderer poptoImg = null;
    float time = 0f;
    private void Awake()
    {
        poptoImg = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update

    private void OnEnable()
    {
        poptoImg.transform.localScale = new Vector3(1f, 1f, 1f);
        poptoImg.color = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(MeltAway());
    }
    IEnumerator MeltAway()
    {
        time = 0f;
        
        while (time <= 1.5f)
        {
            time += Time.deltaTime;

            poptoImg.transform.localScale = Vector3.Lerp(poptoImg.transform.localScale, new Vector3(0.01f, 0.01f, 0.01f), Time.deltaTime);
            poptoImg.color = Color.Lerp(poptoImg.color, new Color(1f, 1f, 1f, 0f), Time.deltaTime);

            yield return null;
        }
        
        ClickEffect.Inst.Release(this);
    }
}
