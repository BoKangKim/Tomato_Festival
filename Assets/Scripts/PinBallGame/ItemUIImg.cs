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
        oriPos = transform.position;    // ������ ���� ��ġ�� ���
        oriScale = transform.localScale;
        startTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //
        // OffsetCurve
        // ������ �ð��� ����(�׷��� �����ؼ�) �������� �Ѵ�.
        curOffset.y = OffsetCurve.Evaluate(startTime); // 0 ����        
        // ���� ��ġ�� �����Ѵ�.
        transform.position = oriPos + curOffset;

        //
        // ScaleCurve
        curScale = Vector3.one * ScaleCurve.Evaluate(startTime);
        // ���� �������� ����
        transform.localScale = curScale;


        // �ð� ����
        startTime += Time.deltaTime;

        // AnimationCurve �ȿ��� ������ �ð��� �˾ƿ���.
        if (OffsetCurve.keys[OffsetCurve.keys.Length - 1].time <= startTime)
        {
            //startTime = 0;  // �ð��� �ٽ� ó������
            //transform.position = oriPos;     // ���� ��ġ�� ���ư��� ����.
            //transform.localScale = oriScale;

            // ��Ȱ��ȭ
            //PhotonNetwork.Destroy(this.gameObject);

        }
    }
}
