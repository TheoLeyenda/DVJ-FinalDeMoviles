using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public void EnableObject(GameObject go)
    {
        go.SetActive(true);       
    }
    public void DisableObject(GameObject go)
    {
        go.SetActive(false);
    }
}
