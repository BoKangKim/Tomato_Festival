using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float PI = 3.14f;
    float moveSpeed = 20f;
    float xAxis = 0f;
    float jumpSpeed = 2f;
    Vector3 mousePos = Vector3.zero;
    Vector3 fireDir = Vector3.zero;
    Vector3 knockBackDir = Vector3.zero;
    public bool isJump { get; set; } = false;
    public bool isFall { get; set; } = false;
    Camera cam;
    [SerializeField] Bullet bullet;
    [SerializeField] Player enemy;
    Rigidbody2D myRigidbody;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (gameObject.name == "Enemy")
            return;
        // 점프
        if (Input.GetKeyDown(KeyCode.W) && isJump == false)
        {
            isJump = true;
            StartCoroutine(Jump(xAxis));
        }
    }

    void FixedUpdate()
    {
        if (gameObject.name == "Enemy")
            return;

        #region 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos = mousePos - new Vector3(0f, 0f, mousePos.z);
            fireDir = (mousePos - transform.position).normalized;
            Bullet bulletInst = Instantiate(bullet, transform.position + fireDir, Quaternion.identity);
            bulletInst.MoveDir = fireDir;
            bulletInst.Enemy = this.enemy;
        }

        #endregion

        #region 플레이어 움직임
        // 좌 우
        //if (isJump == true)
        //    return;
        xAxis = Input.GetAxis("Horizontal");
       
        transform.Translate(xAxis * Vector3.right * moveSpeed * Time.deltaTime);
        #endregion
    }

    public void StartKnockBackCoroutine(Vector3 bulletVec)
    {
        knockBackDir = (transform.position - bulletVec).normalized;
        StartCoroutine(KnockBack());
    }

    private void SetIsJump(bool isJump)
    {
        this.isJump = isJump;
    }

    private void SetIsFall(bool isFall)
    {
        this.isFall = isFall;
    }

    private void StartFall()
    {
        if (isJump == false)
        {
            StartCoroutine(Fall());
        }
        else
        {
            isFall = false;
        }
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
    
    IEnumerator Jump(float xAxis)
    {
        float sinY = 0f;
        float startPosX = transform.position.x;
        float startPosY = transform.position.y;

        while (isJump == true)
        {
            if(sinY > PI)
            {
                isFall = true;
                StartCoroutine(Fall());
                yield break;
            }
            transform.position = new Vector3(transform.position.x, 15 * Mathf.Sin(sinY) + startPosY, 0f);

            sinY += Time.deltaTime * jumpSpeed;
            yield return null;
        }

    }
    
    IEnumerator Fall()
    {
        while(isFall == true)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 13f);
            yield return null;
        }
    }
}
