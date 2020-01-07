using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    // Start is called before the first frame update
    public float life;
    public float rangeOfView;
    protected bool constructed;
    public bool rotateStart;
    public float valueDown;

    public void DownMovement()
    {
        if (rotateStart)
        {
            transform.position = transform.position - new Vector3(0, valueDown, 0);
        }
        else
        {
            transform.position = transform.position - new Vector3(0, 0, valueDown);
        }
    }
    public void SetConstructed(bool _constructed)
    {
        constructed = _constructed;
    }
    public bool GetConstructed()
    {
        return constructed;
    }
}
