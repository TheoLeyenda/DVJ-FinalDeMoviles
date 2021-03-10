using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject go_dialogue;
    public bool disableCollider;
    private CapsuleCollider CC;
    private void Start()
    {
        CC = GetComponent<CapsuleCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DarkTreeFPS.FPSController fpsController = other.GetComponent<DarkTreeFPS.FPSController>();

            fpsController.lockCursor = false;
            go_dialogue.SetActive(true);
            if (disableCollider)
            {
                CC.enabled = false;
            }
        }
    }
}
