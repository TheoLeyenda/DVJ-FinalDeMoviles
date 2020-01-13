using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventClickInButtonConstruction : EventsGame
{
    // Start is called before the first frame update
    public GameObject arrowButtonConstruction;
    public EventClickInCancelConstruction eventClickInCancelConstruction;
    public Dialogue dialogue;
    public Button buttonRotation;
    public Button buttonCancel;
    public Button buttonConfirmed;

    private void OnEnable()
    {
        UIContructionController.OnClickButtonConstruction += EventClickButtonConstruction;
    }
    private void OnDisable()
    {
        UIContructionController.OnClickButtonConstruction -= EventClickButtonConstruction;
    }
    private void Update()
    {
    }
    public void EventClickButtonConstruction(UIContructionController ucc)
    {
        if (dialogue.BarDialogue.activeSelf)
        {
            DisableArrowButtonConstruction();
        }
    }
    public void DisableArrowButtonConstruction()
    {
        if (eventOf == EventOf.Tutorial)
        {
            buttonCancel.interactable = false;
            buttonConfirmed.interactable = false;
            buttonRotation.interactable = false;
            arrowButtonConstruction.SetActive(false);

            dialogue.dialogues[dialogue.GetIndexDialogues()].events = Dialogue.DataDialogue.Events.None;
            dialogue.dialogues[dialogue.GetIndexDialogues()].events2 = Dialogue.DataDialogue.Events.None;
            dialogue.dialogues[dialogue.GetIndexDialogues()].events3 = Dialogue.DataDialogue.Events.None;
            dialogue.dialogues[dialogue.GetIndexDialogues()].events4 = Dialogue.DataDialogue.Events.None;
            dialogue.dialogues[dialogue.GetIndexDialogues()].events5 = Dialogue.DataDialogue.Events.None;
            if (dialogue.dialogues[dialogue.GetIndexDialogues()].dialogo == eventClickInCancelConstruction.GetCancelDialogue())
            {
                dialogue.dialogues[dialogue.GetIndexDialogues()].dialogo = eventClickInCancelConstruction.GetAuxCurrentDialogue();
                dialogue.dialogues[dialogue.GetIndexDialogues()].events = Dialogue.DataDialogue.Events.NotNextDialogue;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events2 = Dialogue.DataDialogue.Events.ActivateButtons;
                dialogue.SwitchDialogBarPosition();
            }
            dialogue.CheckDialogue();
        }
    }
}
