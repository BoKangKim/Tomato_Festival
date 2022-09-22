using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamEffect : MonoBehaviour
{
    float effectTime; // Time for the camera to shake time
    float effecting; // for Time check
    // Start is called before the first frame update
    void Start()
    {
        effectTime = 0.25f;
        effecting = 0f;
    }

    // Update is called once per frame

    public void StartCamEffectCoroutine()
    {
        Debug.Log("StartCamEffectCoroutine");
        StartCoroutine(CamEffectStart());
    }

    IEnumerator CamEffectStart()
    {
        while(effecting < effectTime)
        {
            effecting += Time.deltaTime;
            float randomrotation_x = Random.Range(-0.7f, 0.7f);  // Random value for camera's rotation.x
            float randomrotation_y = Random.Range(-3f, 3f); // Random value for camera's rotation.y
            transform.rotation = Quaternion.Euler(randomrotation_x, randomrotation_y, 0); //camera's rotation.z value is fixed
            yield return null;
        }
        effecting = 0f; //Reset
        transform.rotation = Quaternion.Euler(0, 0, 0); //Reset
    }
}
