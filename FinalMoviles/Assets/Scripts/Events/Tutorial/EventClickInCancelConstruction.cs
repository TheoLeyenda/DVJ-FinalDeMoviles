using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventClickInCancelConstruction : EventsGame
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    public string CancelDialogue;
    public GameObject arrowSelectConstruction;
    private string auxCurrentDialogue;
    private void OnEnable()
    {
        UIContructionController.OnClickButtonCancel += EventClickButtonCancel;
    }
    private void OnDisable()
    {
        UIContructionController.OnClickButtonCancel -= EventClickButtonCancel;
    }
    public void EventClickButtonCancel(UIContructionController ucc)
    {
        if (eventOf == EventOf.Tutorial)
        {
            if (dialogue.BarDialogue.activeSelf)
            {
                arrowSelectConstruction.SetActive(true);
                auxCurrentDialogue = dialogue.dialogues[dialogue.GetIndexDialogues()].dialogo;
                ChangeDialogue();
            }
        }
    }
    public void ChangeDialogue()
    {
        dialogue.dialogues[dialogue.GetIndexDialogues()].dialogo = CancelDialogue;
        dialogue.CheckDialogue();
        dialogue.SwitchDialogBarPosition();
    }
    public string GetCancelDialogue()
    {
        return CancelDialogue;
    }
    public string GetAuxCurrentDialogue()
    {
        return auxCurrentDialogue;
    }
}
