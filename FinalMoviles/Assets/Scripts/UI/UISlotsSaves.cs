using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISlotsSaves : MonoBehaviour
{
    private SaveGameManager sgm;
    private GameData gd;
    public List<Text> textSlot;
    private void OnEnable()
    {
        sgm = SaveGameManager.instaceSaveGameManager;
        gd = GameData.instaceGameData;
        sgm.LoadNamesButtons();
        for (int i = 0; i < textSlot.Count; i++)
        {
            textSlot[i].text = sgm.namesButtonsLoadGame[i];
        }
    }
    /*private void Start()
    {
        sgm = SaveGameManager.instaceSaveGameManager;
        gd = GameData.instaceGameData;
    }*/
    public void SetTextButtonString(string _textButton, int index)
    {
        textSlot[index].text = _textButton;
    }
    public void LoadParty(int indexSlot)
    {
        if (sgm.slotsSaveData[indexSlot].OccupiedSlot == 0)
        {
            sgm.SaveGame(indexSlot);
        }
        else if(sgm.slotsSaveData[indexSlot].OccupiedSlot == 1)
        {
            sgm.LoadGame(indexSlot);
        }
        gd.numberParty = indexSlot;
    }
    public void DeleteParty(int indexSlot)
    {
        sgm.DeleteSlot(indexSlot);
        SetTextButtonString("Nueva Partida", indexSlot);
    }
}
