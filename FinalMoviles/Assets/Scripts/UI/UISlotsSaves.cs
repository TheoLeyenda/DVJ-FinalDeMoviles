using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISlotsSaves : MonoBehaviour
{
    public MainMenuController mmc;
    private SaveGameManager sgm;
    private GameData gd;
    public List<Text> textSlot;
    public List<GameObject> buttonDeleteSlots;
    private bool windowsSureDeleteSlotON;
    public List<GameObject> windowsSureDeleteSlot;
    private void OnEnable()
    {
        sgm = SaveGameManager.instaceSaveGameManager;
        gd = GameData.instaceGameData;
        sgm.LoadNamesButtons();
        for (int i = 0; i < textSlot.Count; i++)
        {
            textSlot[i].text = sgm.namesButtonsLoadGame[i];
        }
        CheckSlot();
        for (int i = 0; i < windowsSureDeleteSlot.Count; i++)
        {
            windowsSureDeleteSlot[i].SetActive(false);
        }
        windowsSureDeleteSlotON = false;
    }
    /*private void Start()
    {
        sgm = SaveGameManager.instaceSaveGameManager;
        gd = GameData.instaceGameData;
    }*/
    public void DeleteAllData()
    {
        sgm.DeleteAll();
    }
    public void CheckSlot()
    {
        for (int i = 0; i < sgm.slotsSaveData.Length; i++)
        {
            if (sgm.slotsSaveData[i].OccupiedSlot == 1)
            {
                buttonDeleteSlots[i].SetActive(true);
            }
            else if (sgm.slotsSaveData[i].OccupiedSlot == 0)
            {
                buttonDeleteSlots[i].SetActive(false);
            }
        }
    }
    private void Update()
    {
        CheckSlot();
    }
    public void SureDeleteSlotWindows(int indexWindows)
    {
        windowsSureDeleteSlotON = !windowsSureDeleteSlotON;
        if (windowsSureDeleteSlotON)
        {
            windowsSureDeleteSlot[indexWindows].SetActive(true);
        }
        else
        {
            windowsSureDeleteSlot[indexWindows].SetActive(false);
        }
    }
    public void SetTextButtonString(string _textButton, int index)
    {
        textSlot[index].text = _textButton;
    }
    public void LoadParty(int indexSlot)
    {
        if (sgm.slotsSaveData[indexSlot].OccupiedSlot == 0)
        {
            sgm.SaveGame(indexSlot);
            mmc.InsertName();
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
        windowsSureDeleteSlot[indexSlot].SetActive(false);
    }
}
