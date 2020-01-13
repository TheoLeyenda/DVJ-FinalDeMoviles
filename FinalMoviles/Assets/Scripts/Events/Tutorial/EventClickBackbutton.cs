using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventClickBackbutton : EventsGame
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    public Button buttonTower;
    private bool once = false;
    private void OnEnable()
    {
        UIContructionController.OnClickButtonBack += EventClickBack;
    }
    private void OnDisable()
    {
        UIContructionController.OnClickButtonBack -= EventClickBack;
    }
    public void EventClickBack(UIContructionController ucc)
    {
        if (eventOf == EventOf.Tutorial)
        {
            if (!once)
            {
                dialogue.dialogues[dialogue.GetIndexDialogues()].events = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events2 = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events3 = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events4 = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events5 = Dialogue.DataDialogue.Events.None;
                dialogue.CheckDialogue();
                once = true;
                buttonTower.interactable = true;
            }
        }
    }
}
