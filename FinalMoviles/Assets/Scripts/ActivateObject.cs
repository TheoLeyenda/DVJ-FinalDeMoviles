using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public void EnableObject(GameObject go)
    {
        if(!go.activeSelf)
        go.SetActive(true);       
    }
    public void DisableObject(GameObject go)
    {
        if(go.activeSelf)
        go.SetActive(false);
    }
}
