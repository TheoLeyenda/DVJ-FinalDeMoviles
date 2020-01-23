using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClickAceptButton : EventsGame
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    public ConstructionManager constructionManager;
    public EventClickInConstructionZone eventClickInConstructionZone;
    private int countConfirmConstruction;
    private bool once = false;
    private GameManager gm;
    private void Start()
    {
        countConfirmConstruction = 0;
        gm = GameObject.Find("GamePrefab").GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        UIContructionController.OnClickButtonAcepted += EventClickAceptConstruction;
    }
    private void OnDisable()
    {
        UIContructionController.OnClickButtonAcepted -= EventClickAceptConstruction;
    }
    public void EventClickAceptConstruction(UIContructionController ucc)
    {
        countConfirmConstruction++;
        if (gm.InTutorial)
        {
            if (!once)
            {
                eventClickInConstructionZone.SetEnableInformationButton(true);
                once = true;
            }
            if (dialogue.BarDialogue.activeSelf)
            {
                dialogue.dialogues[dialogue.GetIndexDialogues()].events = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events2 = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events3 = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events4 = Dialogue.DataDialogue.Events.None;
                dialogue.dialogues[dialogue.GetIndexDialogues()].events5 = Dialogue.DataDialogue.Events.None;
                dialogue.CheckDialogue();
            }
            if (countConfirmConstruction >= constructionManager.constructionZone.Count)
            {
                dialogue.BarDialogue.SetActive(true);
            }
        }
        /*else if (!gm.InTutorial)
        {
            Debug.Log(countConfirmConstruction+"="+constructionManager.constructionZone.Count);
            Debug.Log(gm.GetEnableStartGame());
            if (countConfirmConstruction >= constructionManager.constructionZone.Count)
            {
                gm.SetEnableStartGame(true);
            }
        }*/

    }
    public int GetCountConfirmConstruction()
    {
        return countConfirmConstruction;
    }
}
