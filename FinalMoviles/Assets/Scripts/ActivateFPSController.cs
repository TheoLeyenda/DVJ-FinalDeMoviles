using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFPSController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabPlayerPC;
    public GameObject prefabPlayerAndroid;
    public GameObject CamvasInventory;
    public GameObject CamvasMain;
    public GameObject WeaponManager;
    public GameObject dialogueObject;
    private void OnEnable()
    {
#if !UNITY_ANDROID
        prefabPlayerPC.SetActive(true);
#else
            prefabPlayerAndroid.SetActive(true);
#endif
        CamvasInventory.SetActive(true);
        CamvasMain.SetActive(true);
        WeaponManager.SetActive(true);
        if (dialogueObject != null)
        {
            dialogueObject.SetActive(true);
        }
    }
}
