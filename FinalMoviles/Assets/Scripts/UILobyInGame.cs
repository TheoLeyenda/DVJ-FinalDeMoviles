using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobyInGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textNameParty;
    public Text textCurrentScore;
    private GameData gd;
    private SaveGameManager sgm;
    public UISlotsSaves uISlotsSaves;

    private void OnEnable()
    {
        
        gd = GameData.instaceGameData;
        sgm = SaveGameManager.instaceSaveGameManager;
        textNameParty.text = "Partida: " + gd.currentNameUser + ".";
        textCurrentScore.text = "Puntaje: " + gd.generalScore + "$";
        sgm.LoadNamesButtons();
        gd.gameMode = GameData.GameMode.None;

        //Debug.Log(sgm.slotsSaveData[gd.numberParty].OccupiedSlot);
        if (sgm.slotsSaveData[gd.numberParty].OccupiedSlot == 0)
        {
            //Debug.Log("ENTRE");
            sgm.namesButtonsLoadGame[gd.numberParty] = gd.currentNameUser;
            sgm.slotsSaveData[gd.numberParty].OccupiedSlot = 1;
        }
        uISlotsSaves.SetTextButtonString(gd.currentNameUser, gd.numberParty);
        sgm.SaveGame(gd.numberParty);
    }
}
