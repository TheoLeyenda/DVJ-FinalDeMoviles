﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button buttonInformation;
    private float delayDisableInformationButton = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instanceGameManager.lockedTutorial)
        {
            if (delayDisableInformationButton <= 0)
            {
                buttonInformation.interactable = false;
            }
            else
            {
                delayDisableInformationButton = delayDisableInformationButton - Time.deltaTime;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        if (buttonInformation != null)
        {
            if (buttonInformation.gameObject.activeSelf)
            {
                buttonInformation.interactable = true;
            }
        }
    }
}
