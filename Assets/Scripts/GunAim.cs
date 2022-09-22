using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class GunAim : MonoBehaviourPun
{
    Vector3 Mouse;
    Vector2 dir;
    float angle;
    bool facingRight; //check the direction the gun is looking at


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
            return;

        Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Transforms a point from screen space into world space (mouse's screen position)
        Mouse.z = 0f;
        dir = (Mouse - transform.position).normalized; //direction from gun to mouse
       
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //Rotation angle degrees relative to z-axis

        

        if(transform.position.x <=  Mouse.x)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
            
        
        if (facingRight)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, angle); //from euler to quaternion
            // transform.rotation = Quaternion.LookRotation(Vector3.forward, Mouse - transform.position) * Quaternion.Euler(0, 180, 0);
        }
        else
        {
            //this.transform.rotation = Quaternion.Euler(180, 0, angle);
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.AngleAxis(180, Vector3.right);
        }


        /*
        var usingFlip = transform.position - target;

        var scale = transform.localScale;
        if (usingFlip.x > 0)
        {
            scale.x *= -1f;
        }
        transform.localScale = scale;
        */
    }
}
