using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventClickInButtonMap : EventsGame
{
    public Dialogue dialogue;
    public Button buttonGeneralView;
    public GameObject arrowSelectGeneralView;
    public EventClickAceptButton eventClickAceptButton;
    private int count = 0;
    private void OnEnable()
    {
        ConstructionManager.OnClickGeneralView += EventClickMap;
    }
    private void OnDisable()
    {
        ConstructionManager.OnClickGeneralView -= EventClickMap;
    }
    public void EventClickMap(ConstructionManager cm)
    {
        if (eventOf == EventOf.Tutorial)
        {
            if (dialogue.BarDialogue.activeSelf)
            {
                count++;
                //Debug.Log(count);
                if (count == 1)
                {
                    buttonGeneralView.interactable = false;
                }
                else if (count == 2)
                {
                    arrowSelectGeneralView.SetActive(false);
                }
                if (count == 1 || count == 2 || eventClickAceptButton.GetCountConfirmConstruction() >= eventClickAceptButton.constructionManager.constructionZone.Count)
                {
                    dialogue.dialogues[dialogue.GetIndexDialogues()].events = Dialogue.DataDialogue.Events.None;
                    dialogue.dialogues[dialogue.GetIndexDialogues()].events2 = Dialogue.DataDialogue.Events.None;
                    dialogue.dialogues[dialogue.GetIndexDialogues()].events3 = Dialogue.DataDialogue.Events.None;
                    dialogue.dialogues[dialogue.GetIndexDialogues()].events4 = Dialogue.DataDialogue.Events.None;
                    dialogue.dialogues[dialogue.GetIndexDialogues()].events5 = Dialogue.DataDialogue.Events.None;
                    dialogue.CheckDialogue();
                }
            }
        }
    }
}
