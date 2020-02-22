using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotateVelocity = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, rotateVelocity);
    }
}
