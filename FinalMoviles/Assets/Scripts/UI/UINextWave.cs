using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UINextWave : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textStartWave;
    public GameObject buttonStartWave;
    public KeyCode keyCodeStartWave;
    [HideInInspector]
    public bool activateElementsCamvasNextWave;
    public GenerateEnemyManager generateEnemyManager;

    // Update is called once per frame
    void Update()
    {
        CheckElementsCamvasNextWave();
    }
    public void CheckElementsCamvasNextWave()
    {
        if (!activateElementsCamvasNextWave)
        {
            if (buttonStartWave.activeSelf)
            {
#if UNITY_ANDROID
                buttonStartWave.SetActive(false);
#endif
            }
            if (textStartWave.gameObject.activeSelf)
            {
#if UNITY_STANDALONE
                textStartWave.gameObject.SetActive(false);
#endif
            }
        }
        else if (activateElementsCamvasNextWave)
        {
            if (!buttonStartWave.activeSelf)
            {
#if UNITY_ANDROID
                buttonStartWave.SetActive(true);
#endif
            }
            if (!textStartWave.gameObject.activeSelf)
            {
#if UNITY_STANDALONE
                textStartWave.gameObject.SetActive(true);
#endif
            }
            if (textStartWave.gameObject.activeSelf)
            {
                if (Input.GetKeyDown(keyCodeStartWave))
                {
                    generateEnemyManager.ActivateAllGenerators();
                }
            }
        }
        
    }
    public void SetActivateElementsCamvasNextWave(bool _activateElementsCamvasNextWave)
    {
        activateElementsCamvasNextWave = _activateElementsCamvasNextWave;
    }

}
