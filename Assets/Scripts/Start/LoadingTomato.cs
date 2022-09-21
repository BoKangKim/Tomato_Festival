using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LoadingTomato : MonoBehaviour
{
    
    Vector3 loadingTomatoPos = Vector3.zero;
    public float DownSpeed { get; set; } = 20f;
    private void OnEnable()
    {
        loadingTomatoPos = new Vector3((int)Random.Range(-33, 33), (int)Random.Range(10, 17), 0f);
        transform.position = loadingTomatoPos;
        
    }
    private void Start()
    {
        LoadingEffect.Inst.callbackDestroy = CallBackDestroy;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * DownSpeed);
        if(transform.position.y <= -19f)
        {
            LoadingEffect.Inst.Release(this);
        }
    }

    void CallBackDestroy()
    {
        LoadingEffect.Inst.Release(this);
    }
}
