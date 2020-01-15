using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;

public class SingletonObject : MonoBehaviour
{
    // Start is called before the first frame update
    public FPSController fpsControllerAndroid;
    public FPSController fpsControllerPC;
    public static FPSController instanceSingletonObject;
    private void OnEnable()
    {
        if (instanceSingletonObject == null)
        {
#if UNITY_ANDROID
            instanceSingletonObject = fpsControllerAndroid;
#endif
#if UNITY_STANDALONE
            instanceSingletonObject = fpsControllerPC;
#endif
        }
        else if(instanceSingletonObject != fpsControllerAndroid && instanceSingletonObject != fpsControllerPC)
        {
            gameObject.SetActive(false);
            //Destroy(this);
        }
    }
}
