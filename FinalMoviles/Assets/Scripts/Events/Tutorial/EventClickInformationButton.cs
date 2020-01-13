using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventClickInformationButton : EventsGame
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    private bool once = false;
    private void OnEnable()
    {
        UIContructionController.OnClickButtonInformation += EventClickInformation;    
    }
    private void OnDisable()
    {
        UIContructionController.OnClickButtonInformation -= EventClickInformation;
    }
    public void EventClickInformation(UIContructionController ucc)
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
                dialogue.BarDialogue.transform.position += new Vector3(0, -500f, 0);
                once = true;
            }
        }
    }
}
