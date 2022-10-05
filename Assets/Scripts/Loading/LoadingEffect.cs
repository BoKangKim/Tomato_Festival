using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public delegate void LoadingTomatoDestroy();

public class LoadingEffect : ObjectPool<LoadingTomato>
{
    #region Singleton
    private static LoadingEffect instance = null;

    public static LoadingEffect Inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LoadingEffect>();
                if (instance == null)
                    instance = new GameObject("LoadingEffect").AddComponent<LoadingEffect>();
            }

            return instance;
        }
    }

    #endregion


    public LoadingTomatoDestroy callbackDestroy = null;
    [SerializeField] LoadingTomato loadingTomatoPrefab = null;
    

    public override LoadingTomato CreatePool()
    {
        return loadingTomatoPrefab;
    }

    // Start is called before the first frame update
    void Awake()
    {
        loadingTomatoPrefab = Resources.Load<LoadingTomato>("Loadingtomato");
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine("LoadingTomatoes");
    }
    IEnumerator LoadingTomatoes()
    {
        while (true)
        {
            float Rtime = Random.Range(0.0f, 2.0f);
            yield return new WaitForSeconds(Rtime);
            
            LoadingTomato loadingtoInst = Get();
            
            loadingtoInst.transform.parent = this.transform;

        }

    }
    public void Pouring()
    {
        StopCoroutine("LoadingTomatoes");

        StartCoroutine("PouringTomato");

    }

    IEnumerator PouringTomato()
    {
        yield return new WaitForSeconds(0.2f);
        if (callbackDestroy != null)
        {
            callbackDestroy();
        }

        float time = 0f;
        int numcount = 0;
        while (time <= 1.5f)
        {
            time += Time.deltaTime;
            float Rtime = Random.Range(0.0f, 0.05f);
            yield return new WaitForSeconds(Rtime);

            LoadingTomato loadingtoInst = Get();
           
            loadingtoInst.transform.parent = this.transform;
            loadingtoInst.DownSpeed = 40f;
            
            numcount++;

            if (base.Pool_Max_Size -2 <= numcount)
            {
                yield break;
            }
            
        }
    }



}
