using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventClickInConstructionZone : EventsGame
{
    // Start is called before the first frame update
    public GameObject arrowConstructionZone;
    public GameObject arrowButtonConstruction;
    public Button ButtonNextContructionZone;
    public Button ButtonGeneralVision;
    public Button Tower_WoodButton;
    public Dialogue dialogue;
    public EventClickInButtonConstruction eventClickInButtonConstruction;
    public EventClickAceptButton eventClickAceptButton;
    private bool enableInformationButton = false;
    private void Start()
    {
        ButtonNextContructionZone.interactable = false;
        ButtonGeneralVision.interactable = false;
    }
    private void OnEnable()
    {
        ConstructionManager.OnClickConstructionZone += EventClickZoneConstruction;
    }
    private void OnDisable()
    {
        ConstructionManager.OnClickConstructionZone -= EventClickZoneConstruction;
    }

    public void EventClickZoneConstruction(ConstructionManager cm)
    {
        if (dialogue.BarDialogue.activeSelf || enableInformationButton)
        {
            DisableArrowTutorial();
        }
    }
    public void DisableArrowTutorial()
    {
        if (eventOf == EventOf.Tutorial)
        {
            if (enableInformationButton)
            {
                dialogue.BarDialogue.SetActive(true);
                enableInformationButton = false;
                Tower_WoodButton.interactable = false;
            }
            else if(eventClickAceptButton.GetCountConfirmConstruction() == 0)
            {
                arrowConstructionZone.SetActive(false);
                arrowButtonConstruction.SetActive(true);
                dialogue.dialogues[dialogue.GetIndexDialogues()].events = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events2 = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events3 = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events4 = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events5 = Dialogue.DataDialogue.Events.None;
                dialogue.CheckDialogue();
            }
            
        }
    }
    public void SetEnableInformationButton(bool _enableInformationButton)
    {
        enableInformationButton = _enableInformationButton;
    }
    public bool GetEnableInformationButton()
    {
        return enableInformationButton;
    }
}
