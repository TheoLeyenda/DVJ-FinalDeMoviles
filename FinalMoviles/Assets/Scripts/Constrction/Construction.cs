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

    // Esto sirve para acomodar el edificio en caso de que este no este en el piso cuando se genere
    [Header("Start Movement Construction")]
    public float valueDown;
    public float valueUp;
    
    public void SetConstructed(bool _constructed)
    {
        constructed = _constructed;
    }
    public bool GetConstructed()
    {
        return constructed;
    }
    public void UpMovement()
    {
        if (rotateStart)
        {
            transform.position = transform.position + new Vector3(0, valueUp, 0);
        }
        else
        {
            transform.position = transform.position + new Vector3(0, 0, valueUp);
        }
    }
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
}
