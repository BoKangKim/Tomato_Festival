using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoRotat : MonoBehaviour
{
    [SerializeField] float rotatSpeed = 150;



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotatSpeed * Time.deltaTime));
    }
}
