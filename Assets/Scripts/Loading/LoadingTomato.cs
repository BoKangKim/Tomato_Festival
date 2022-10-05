using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LoadingTomato : MonoBehaviour
{
    
    Vector3 loadingTomatoPos = Vector3.zero;
    public float DownSpeed { get; set; }
    private void OnEnable()
    {
        //loadingTomatoPos = new Vector3((int)Random.Range(-33, 33), (int)Random.Range(10, 17), 0f);
        loadingTomatoPos = new Vector3((int)Random.Range(-30, 30), (int)Random.Range(10, 10), 0f);

        transform.position = loadingTomatoPos;
        DownSpeed = 10f;

        LoadingEffect.Inst.callbackDestroy += CallBackDestroy;
    }
    
    // Update is called once per frame
    void Update()
    {
        DownSpeed += 0.1f;
        transform.Translate(Vector3.down * Time.deltaTime * DownSpeed);
        //if (transform.position.y <= -19f)
            
        if (transform.position.y <= -15f)

            {
                LoadingEffect.Inst.Release(this);
        }

    }

    void CallBackDestroy()
    {
        LoadingEffect.Inst.Release(this);
    }
}
