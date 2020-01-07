﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    // Start is called before the first frame update
    public enum PivotMovement
    {
        None,
        X,
        Y,
        Z,
    }
    public float life;
    public float rangeOfView;
    protected bool constructed;
    public bool rotateStart;
    public PivotMovement pivotMovement;
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
        if (pivotMovement == PivotMovement.Y)
        {
            transform.position = transform.position + new Vector3(0, valueUp, 0);
        }
        else if (pivotMovement == PivotMovement.Z)
        {
            transform.position = transform.position + new Vector3(0, 0, valueUp);
        }
        else if (pivotMovement == PivotMovement.X)
        {
            transform.position = transform.position + new Vector3(valueUp, 0, 0);
        }
    }
    public void DownMovement()
    {
        if (pivotMovement == PivotMovement.Y)
        {
            transform.position = transform.position - new Vector3(0, valueDown, 0);
        }
        else if (pivotMovement == PivotMovement.Z)
        {
            transform.position = transform.position - new Vector3(0, 0, valueDown);
        }
        else if (pivotMovement == PivotMovement.X)
        {
            transform.position = transform.position - new Vector3(valueDown, 0, 0);
        }
    }
}
