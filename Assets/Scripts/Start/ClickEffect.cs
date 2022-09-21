using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : ObjectPool<PopTomato>
{
    #region Singleton
    private static ClickEffect instance = null;

    public static ClickEffect Inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ClickEffect>();
                if (instance == null)
                    instance = new GameObject("ClickEffect").AddComponent<ClickEffect>();
            }

            return instance;
        }
    }

    #endregion

    [SerializeField] PopTomato popTomatoPrefab = null;
    Vector3 MousePos = Vector3.zero;

    public override PopTomato CreatePool()
    {
        return popTomatoPrefab;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        popTomatoPrefab = Resources.Load<PopTomato>("Poptomato");
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Poping());
    }
    IEnumerator Poping()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) == true);
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePos.z = 0f;
            Debug.Log($"{MousePos}");

            PopTomato poptoInst = Get();
            if (poptoInst != null)
            {
                Debug.Log("토마토오 생성!");
            }

            poptoInst.transform.parent = this.transform;
            Debug.Log($"{poptoInst.transform.parent}");
            poptoInst.transform.position = MousePos;
        }

    }
}
