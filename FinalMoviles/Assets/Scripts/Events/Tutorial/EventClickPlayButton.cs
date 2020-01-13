using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClickPlayButton : EventsGame
{
    public Dialogue dialogue;
    private void OnEnable()
    {
        ConstructionManager.OnClickPlayButton += EventClickPlay;
    }
    private void OnDisable()
    {
        ConstructionManager.OnClickPlayButton -= EventClickPlay;
    }
    public void EventClickPlay(ConstructionManager cm)
    {

        dialogue.dialogues[dialogue.GetIndexDialogues()].events = Dialogue.DataDialogue.Events.None;
        dialogue.dialogues[dialogue.GetIndexDialogues()].events2 = Dialogue.DataDialogue.Events.None;
        dialogue.dialogues[dialogue.GetIndexDialogues()].events3 = Dialogue.DataDialogue.Events.None;
        dialogue.dialogues[dialogue.GetIndexDialogues()].events4 = Dialogue.DataDialogue.Events.None;
        dialogue.dialogues[dialogue.GetIndexDialogues()].events5 = Dialogue.DataDialogue.Events.None;
        dialogue.CheckDialogue();
    }
}
