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
    public GameManager gm;
    public GenerateEnemyManager generateEnemyManager;
    public GameObject CountdownObject;
    public Text textCountdownObject;

    // Update is called once per frame
    void Update()
    {
        CheckElementsCamvasNextWave();
        CheckCountdown();
    }
    public void CheckCountdown()
    {
        if (generateEnemyManager.DelayStartRound > 0.5 )
        {
            textCountdownObject.text = "" + Mathf.Round(generateEnemyManager.DelayStartRound);
        }
        else
        {
            CountdownObject.SetActive(false);
        }
    }
    public void CheckElementsCamvasNextWave()
    {
        if (!activateElementsCamvasNextWave)
        {
            if (buttonStartWave.activeSelf)
            {
#if UNITY_ANDROID
                buttonStartWave.SetActive(false);
                CountdownObject.SetActive(false);
#endif
            }
            if (textStartWave.gameObject.activeSelf)
            {
#if UNITY_STANDALONE
                //Debug.Log("Desactive texto");
                textStartWave.gameObject.SetActive(false);
                CountdownObject.SetActive(false);
#endif
            }
        }
        else if (activateElementsCamvasNextWave)
        {
            if (!buttonStartWave.activeSelf)
            {
#if UNITY_ANDROID
                buttonStartWave.SetActive(true);
                if (gm.InTutorial)
                {
                    CountdownObject.SetActive(true);
                }
                else if (generateEnemyManager.currentWave > 0) 
                {
                    CountdownObject.SetActive(true);
                }
#endif
            }
            if (!textStartWave.gameObject.activeSelf)
            {
#if UNITY_STANDALONE
                textStartWave.gameObject.SetActive(true);
                if (gm.InTutorial)
                {
                    CountdownObject.SetActive(true);
                }
                else if (generateEnemyManager.currentWave > 0) 
                {
                    CountdownObject.SetActive(true);
                }
#endif
            }
            if (textStartWave.gameObject.activeSelf)
            {
                if (Input.GetKeyDown(keyCodeStartWave))
                {
                    StartWave();
                }
            }
        }
        
    }
    public void StartWave()
    {
        if (generateEnemyManager.infinityGenerator)
        {
            activateElementsCamvasNextWave = false;
            generateEnemyManager.EnableNextRound();
            generateEnemyManager.DelayStartRound = 0;
        }
        generateEnemyManager.ActivateAllGenerators();
    }
    public void SetActivateElementsCamvasNextWave(bool _activateElementsCamvasNextWave)
    {
        activateElementsCamvasNextWave = _activateElementsCamvasNextWave;
    }
}
