using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFloor : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    void Update()
    {
        this.transform.Rotate(new Vector3(0f,0f, rotationSpeed));
    }
}
