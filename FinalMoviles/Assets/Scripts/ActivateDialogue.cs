using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject go_dialogue;
    private CapsuleCollider CC;
    private void Start()
    {
        CC = GetComponent<CapsuleCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            go_dialogue.SetActive(true);
            CC.enabled = false;
        }
    }
}
