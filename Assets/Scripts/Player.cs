using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 20f;
    Vector3 mousePos = Vector3.zero;
    Vector3 fireDir = Vector3.zero;
    Vector3 knockBackDir = Vector3.zero;
    Camera cam;
    [SerializeField] Bullet bullet;
    [SerializeField] Player enemy;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (gameObject.name == "Enemy")
            return;

        #region 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos = mousePos - new Vector3(0f,0f, mousePos.z);
            fireDir = (mousePos - transform.position).normalized;
            Bullet bulletInst = Instantiate(bullet,transform.position + fireDir, Quaternion.identity);
            bulletInst.MoveDir = fireDir;
            bulletInst.Enemy = this.enemy;
        }

        #endregion

        #region 플레이어 움직임
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        transform.Translate((xAxis * Vector3.right + yAxis * Vector3.up) * moveSpeed * Time.deltaTime);
        #endregion
    }

    public void StartKnockBackCoroutine(Vector3 bulletVec)
    {
        knockBackDir = (transform.position - bulletVec).normalized;
        StartCoroutine(KnockBack());
    }

    IEnumerator KnockBack()
    {
        float curTime = 0;

        while (curTime < 1f)
        {
            curTime += Time.deltaTime;
            transform.Translate(knockBackDir * Time.deltaTime * 2f);
            yield return null;
        }
    }
}
