using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class ItemUIImg : MonoBehaviourPun
{
    [SerializeField] AnimationCurve OffsetCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 40f) });
    [SerializeField] AnimationCurve ScaleCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(1f, 0f) });


    float startTime = 0f;
    Vector3 oriPos = Vector3.zero;
    Vector3 oriScale = Vector3.one;
    Vector3 curOffset = Vector3.zero;
    Vector3 curScale = Vector3.one;



    // Start is called before the first frame update
    void Start()
    {
        oriPos = transform.position;    // 시작할 때의 위치도 기록
        oriScale = transform.localScale;
        startTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //
        // OffsetCurve
        // 지정된 시간의 값을(그래프 참조해서) 가져오게 한다.
        curOffset.y = OffsetCurve.Evaluate(startTime); // 0 리턴        
        // 나의 위치를 변경한다.
        transform.position = oriPos + curOffset;

        //
        // ScaleCurve
        curScale = Vector3.one * ScaleCurve.Evaluate(startTime);
        // 나의 스케일을 변경
        transform.localScale = curScale;


        // 시간 누적
        startTime += Time.deltaTime;

        // AnimationCurve 안에서 마지막 시간을 알아오자.
        if (OffsetCurve.keys[OffsetCurve.keys.Length - 1].time <= startTime)
        {
            //startTime = 0;  // 시간은 다시 처음부터
            //transform.position = oriPos;     // 원래 위치로 돌아가게 하자.
            //transform.localScale = oriScale;

            // 비활성화
            //PhotonNetwork.Destroy(this.gameObject);

        }
    }
}
