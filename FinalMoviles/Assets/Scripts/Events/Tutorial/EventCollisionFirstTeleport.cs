using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCollisionFirstTeleport : EventsGame
{
    // Start is called before the first frame update
    private bool bocle = false;
    private bool once = true;
    public Dialogue dialogue;
    public CustomTeleporter customTeleporter;
    private void OnEnable()
    {
        CustomTeleporter.OnTriggerWhitMe += EventCollisionWhitCustomTeleporter;
    }
    private void OnDisable()
    {
        CustomTeleporter.OnTriggerWhitMe -= EventCollisionWhitCustomTeleporter;
    }
    public void EventCollisionWhitCustomTeleporter(CustomTeleporter ct)
    {
        if (eventOf == EventOf.Tutorial && once)
        {
            bocle = true;
        }
    }
    public void ActivateObjectDialogue()
    {
        if (customTeleporter.camvasTeleport.activeSelf)
        {
            dialogue.BarDialogue.SetActive(true);
            bocle = false;
            once = false;
            /*dialogue.dialogues[dialogue.GetIndexDialogues()].events = Dialogue.DataDialogue.Events.None;
            dialogue.dialogues[dialogue.GetIndexDialogues()].events2 = Dialogue.DataDialogue.Events.None;
            dialogue.dialogues[dialogue.GetIndexDialogues()].events3 = Dialogue.DataDialogue.Events.None;
            dialogue.dialogues[dialogue.GetIndexDialogues()].events4 = Dialogue.DataDialogue.Events.None;
            dialogue.dialogues[dialogue.GetIndexDialogues()].events5 = Dialogue.DataDialogue.Events.None;*/
        }
    }
    private void Update()
    {
        if (bocle)
        {
            ActivateObjectDialogue();
        }
    }
    // Update is called once per frame
}
