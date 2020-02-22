using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerElementSurvival : MonoBehaviour
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    public GameObject objectDialogue;
    private GameData gd;
    void Start()
    {
        gd = GameData.instaceGameData;
        if (gd.gameMode == GameData.GameMode.Survival)
        {
            dialogue.SetIndexDialogues(dialogue.dialogues.Count - 1);
            objectDialogue.SetActive(false);
        }
    }
    
}
