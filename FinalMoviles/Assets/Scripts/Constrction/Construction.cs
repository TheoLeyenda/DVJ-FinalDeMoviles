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
    private void OnEnable()
    {
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
