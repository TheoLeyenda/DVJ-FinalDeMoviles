using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;
public class DetectedTeleport : MonoBehaviour
{
    public CustomTeleporter CT;
    public int indexTeleport;
    public GameObject CamvasTeleport;
    public FPSController fpsPC;
    public FPSController fpsAndroid;
    public GameObject parentCT;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Teleporter")
        {
            Transform t;
            GameObject go;
            CT = other.GetComponent<CustomTeleporter>();
            CT.camvasTeleport = CamvasTeleport;
            CT.fpsAndroid = fpsAndroid;
            CT.fpsPC = fpsPC;
            t = CT.GetComponentInParent<Transform>();
            go = t.gameObject.transform.parent.gameObject;
            if (t != null)
            {
                parentCT = go;
            }
        }
    }
    private void Update()
    {
        if (parentCT != null)
        {
            if (!parentCT.activeSelf)
            {
                CT = null;
                parentCT = null;
            }
        }
    }
    public CustomTeleporter GetCustomTeleport()
    {
        return CT;
    }
}
