using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    public string NameSceneLoby;
    public GameData gd;
    public SlotSaveData[] slotsSaveData;
    public int sizeSlotData;
    public static SaveGameManager instaceSaveGameManager;
    public List<string> namesButtonsLoadGame;
    public bool clearSaveData;
    public int loops;
    void Awake()
    {
        if (instaceSaveGameManager == null)
        {
            instaceSaveGameManager = this;
            DontDestroyOnLoad(this);
        }
        else if (instaceSaveGameManager != null)
        {
            Destroy(this);
        }
        slotsSaveData = new SlotSaveData[sizeSlotData];
        for (int i = 0; i < slotsSaveData.Length; i++)
        {
            slotsSaveData[i] = new SlotSaveData();
            slotsSaveData[i].nameLokedObjects = new List<string>();
            slotsSaveData[i].nameUnlokedObjects = new List<string>();
        }
        if (clearSaveData)
        {
            PlayerPrefs.DeleteAll();
        }
     
    }
    private void Start()
    {
        LoadNamesButtons();
        for (int i = 0; i < slotsSaveData.Length; i++)
        {
            slotsSaveData[i].OccupiedSlot = PlayerPrefs.GetInt("Slot " + i + " - " + "OccupiedSlot", 0);
        }
    }
    public void LoadNamesButtons()
    {
        for (int i = 0; i < namesButtonsLoadGame.Count; i++)
        {
            if (PlayerPrefs.GetString("Slot " + i, "None") != "None")
            {
                //Debug.Log(PlayerPrefs.GetString("Slot " + i));
                namesButtonsLoadGame[i] = PlayerPrefs.GetString("Slot " + i);
            }
            else
            {
                namesButtonsLoadGame[i] = "Nueva Partida";
            }
        }
    }
    public void SetGameDataInSlotData(int indexSlot)
    {
        slotsSaveData[indexSlot].nameUnlokedObjects.Clear();
        slotsSaveData[indexSlot].nameLokedObjects.Clear();
        for (int i = 0; i < gd.nameLokedObjects.Count; i++)
        {
            slotsSaveData[indexSlot].nameLokedObjects.Add(gd.nameLokedObjects[i]);
            //slotsSaveData[indexSlot].nameLokedObjects[i] = gd.nameLokedObjects[i];
        }

        for (int i = 0; i < gd.nameUnlokedObjects.Count; i++)
        {
            slotsSaveData[indexSlot].nameUnlokedObjects.Add(gd.nameUnlokedObjects[i]);
            //slotsSaveData[indexSlot].nameUnlokedObjects[i] = gd.nameUnlokedObjects[i];
        }

        slotsSaveData[indexSlot].currentLevel = gd.currentLevel;
        slotsSaveData[indexSlot].currentNameUser = gd.currentNameUser;
        slotsSaveData[indexSlot].generalScore = gd.generalScore;
        //CHECKEO LAS ARMAS DESBLOQUEADAS
        if (gd.dataPlayer.unlockedM4)
        {
            slotsSaveData[indexSlot].unlockedM4 = 1;
        }
        else
        {
            slotsSaveData[indexSlot].unlockedM4 = 0;
        }

        if (gd.dataPlayer.unlockedScar)
        {
            slotsSaveData[indexSlot].unlockedScar = 1;
        }
        else
        {
            slotsSaveData[indexSlot].unlockedScar = 0;
        }

        if (gd.dataPlayer.unlockedSniper)
        {
            slotsSaveData[indexSlot].unlockedSniper = 1;
        }
        else
        {
            slotsSaveData[indexSlot].unlockedSniper = 0;
        }
        //--------------------------//

        slotsSaveData[indexSlot].scarAmmo = gd.dataPlayer.scarAmmo;
        slotsSaveData[indexSlot].M4Ammo = gd.dataPlayer.M4Ammo;
        slotsSaveData[indexSlot].SniperAmmo = gd.dataPlayer.SniperAmmo;
        slotsSaveData[indexSlot].countNukePowerUp = gd.dataPlayer.countNukePowerUp;
        slotsSaveData[indexSlot].countLifeUpPowerUp = gd.dataPlayer.countLifeUpPowerUp;
        slotsSaveData[indexSlot].countRepairConstructionPowerUp = gd.dataPlayer.countRepairConstructionPowerUp;
        slotsSaveData[indexSlot].countMedikitPowerUp = gd.dataPlayer.countMedikitPowerUp;
        slotsSaveData[indexSlot].countIcePowerUp = gd.dataPlayer.countIcePowerUp;
        slotsSaveData[indexSlot].countMeteoroPowerUp = gd.dataPlayer.countMeteoroPowerUp;
    }
    public void DeleteSlot(int indexSlot)
    {
        slotsSaveData[indexSlot].OccupiedSlot = 0;
        namesButtonsLoadGame[indexSlot] = "Nueva Partida";
        PlayerPrefs.SetString("Slot " + indexSlot, namesButtonsLoadGame[indexSlot]);
        gd.nameLokedObjects.Clear();
        gd.nameUnlokedObjects.Clear();
        for (int i = 0; i < gd.auxNameLokedObjects.Count; i++)
        {
            gd.nameLokedObjects.Add(gd.auxNameLokedObjects[i]);
        }
        for (int i = 0; i < gd.auxNameUnlokedObjects.Count; i++)
        {
            gd.nameUnlokedObjects.Add(gd.auxNameUnlokedObjects[i]);
        }
        gd.currentLevel = 0;
        gd.currentNameUser = "None";
        gd.generalScore = 0;
        gd.dataPlayer.unlockedScar = false;
        gd.dataPlayer.unlockedM4 = false;
        gd.dataPlayer.unlockedSniper = false;
        gd.dataPlayer.scarAmmo = 0;
        gd.dataPlayer.M4Ammo = 0;
        gd.dataPlayer.SniperAmmo = 0;
        gd.dataPlayer.countNukePowerUp = 0;
        gd.dataPlayer.countLifeUpPowerUp = 0;
        gd.dataPlayer.countRepairConstructionPowerUp = 0;
        gd.dataPlayer.countMedikitPowerUp = 0;
        gd.dataPlayer.countIcePowerUp = 0;
        gd.dataPlayer.countMeteoroPowerUp = 0;
        SaveGame(indexSlot);
    }
    public void SaveGame(int indexSlot)
    {
        if (slotsSaveData != null)
        {
            if (indexSlot < slotsSaveData.Length)
            {
                SetGameDataInSlotData(indexSlot);
                for (int i = 0; i < namesButtonsLoadGame.Count; i++)
                {
                    //Debug.Log("GUARDE");
                    PlayerPrefs.SetString("Slot " + i, namesButtonsLoadGame[i]);
                }

                for (int i = 0; i < slotsSaveData[indexSlot].nameLokedObjects.Count; i++)
                {
                    string key = "Slot LokedObjects" + indexSlot + " - " + i;
                    PlayerPrefs.SetString(key, slotsSaveData[indexSlot].nameLokedObjects[i]);
                }

                for (int i = 0; i < slotsSaveData[indexSlot].nameUnlokedObjects.Count; i++)
                {
                    string key = "Slot UnlokedObjects" + indexSlot + " - " + i;
                    PlayerPrefs.SetString(key, slotsSaveData[indexSlot].nameUnlokedObjects[i]);
                }

                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "OccupiedSlot", slotsSaveData[indexSlot].OccupiedSlot);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "currentLevel", slotsSaveData[indexSlot].currentLevel);
                PlayerPrefs.SetString("Slot " + indexSlot + " - " + "currentNameUser", slotsSaveData[indexSlot].currentNameUser);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "generalScore", slotsSaveData[indexSlot].generalScore);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "unlockedScar", slotsSaveData[indexSlot].unlockedScar);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "unlockedM4", slotsSaveData[indexSlot].unlockedM4);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "unlockedSniper", slotsSaveData[indexSlot].unlockedSniper);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "scarAmmo", slotsSaveData[indexSlot].scarAmmo);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "M4Ammo", slotsSaveData[indexSlot].M4Ammo);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "SniperAmmo", slotsSaveData[indexSlot].SniperAmmo);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "countNukePowerUp", slotsSaveData[indexSlot].countNukePowerUp);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "countLifeUpPowerUp", slotsSaveData[indexSlot].countLifeUpPowerUp);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "countRepairConstructionPowerUp", slotsSaveData[indexSlot].countRepairConstructionPowerUp);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "countMedikitPowerUp", slotsSaveData[indexSlot].countMedikitPowerUp);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "countIcePowerUp", slotsSaveData[indexSlot].countIcePowerUp);
                PlayerPrefs.SetInt("Slot " + indexSlot + " - " + "countMeteoroPowerUp", slotsSaveData[indexSlot].countMeteoroPowerUp);
            }
                    
        }
    }
    public void LoadGame(int indexSlot)
    {
        if (slotsSaveData != null)
        {
            if (indexSlot < slotsSaveData.Length)
            {
                //gd.PartyCreated = gd.GetPositiveValuePartyCreated();
                LoadNamesButtons();
                slotsSaveData[indexSlot].nameLokedObjects.Clear();
                slotsSaveData[indexSlot].nameUnlokedObjects.Clear();
                for (int i = 0; i < gd.nameLokedObjects.Count; i++)
                {
                    if (gd.nameLokedObjects[i] != "" && gd.nameLokedObjects[i] != " ")
                    {
                        slotsSaveData[indexSlot].nameLokedObjects.Add(gd.nameLokedObjects[i]);
                    }
                    //slotsSaveData[indexSlot].nameLokedObjects[i] = gd.nameLokedObjects[i];
                }
                for (int i = 0; i < gd.nameUnlokedObjects.Count; i++)
                {
                    if (gd.nameUnlokedObjects[i] != "" && gd.nameUnlokedObjects[i] != " ")
                    {
                        slotsSaveData[indexSlot].nameUnlokedObjects.Add(gd.nameUnlokedObjects[i]);
                    }
                    //slotsSaveData[indexSlot].nameUnlokedObjects[i] = gd.nameUnlokedObjects[i];
                }
                gd.nameLokedObjects.Clear();
                gd.nameUnlokedObjects.Clear();
                for (int i = 0; i < loops; i++)
                {
                    string key = " ";
                    //Debug.Log(slotsSaveData[indexSlot].nameLokedObjects.Count);
                    key = "Slot LokedObjects" + indexSlot + " - " + i;
                    if (PlayerPrefs.GetString(key) != "" && PlayerPrefs.GetString(key) != " ")
                    {
                        gd.nameLokedObjects.Add(PlayerPrefs.GetString(key));
                    }
                }

                for (int i = 0; i < loops; i++)
                {
                    string key = " ";
                    //Debug.Log(slotsSaveData[indexSlot].nameUnlokedObjects.Count);
                    key = "Slot UnlokedObjects" + indexSlot + " - " + i;
                    if (PlayerPrefs.GetString(key) != "" && PlayerPrefs.GetString(key) != " ")
                    {
                        gd.nameUnlokedObjects.Add(PlayerPrefs.GetString(key));
                    }
                }
                slotsSaveData[indexSlot].OccupiedSlot = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "OccupiedSlot");
                gd.currentLevel = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "currentLevel");
                gd.currentNameUser = PlayerPrefs.GetString("Slot " + indexSlot + " - " + "currentNameUser");
                gd.generalScore = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "generalScore");
                gd.dataPlayer.unlockedScar = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "unlockedScar") == 1;
                gd.dataPlayer.unlockedM4 = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "unlockedM4") == 1;
                gd.dataPlayer.unlockedSniper = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "unlockedSniper") == 1;
                gd.dataPlayer.scarAmmo = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "scarAmmo");
                gd.dataPlayer.M4Ammo = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "M4Ammo");
                gd.dataPlayer.SniperAmmo = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "SniperAmmo");
                gd.dataPlayer.countNukePowerUp = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "countNukePowerUp");
                gd.dataPlayer.countLifeUpPowerUp = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "countLifeUpPowerUp");
                gd.dataPlayer.countRepairConstructionPowerUp = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "countRepairConstructionPowerUp");
                gd.dataPlayer.countMedikitPowerUp = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "countMedikitPowerUp");
                gd.dataPlayer.countIcePowerUp = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "countIcePowerUp");
                gd.dataPlayer.countMeteoroPowerUp = PlayerPrefs.GetInt("Slot " + indexSlot + " - " + "countMeteoroPowerUp");

                SceneManager.LoadScene(NameSceneLoby);
            }
        }
    }
}
[System.Serializable]
public class SlotSaveData
{
    public int OccupiedSlot;
    public List<string> nameLokedObjects;
    public List<string> nameUnlokedObjects;
    public int currentLevel;
    public string currentNameUser;
    public int generalScore;
    //BOOLEANOS SI VALEN 1 ES TRUE Y SI VALEN 0 ES FALSE
    public int unlockedScar;
    public int unlockedM4;
    public int unlockedSniper;
    //------------------------------------------------//
    public int scarAmmo;
    public int M4Ammo;
    public int SniperAmmo;
    public int countNukePowerUp;
    public int countLifeUpPowerUp;
    public int countRepairConstructionPowerUp;
    public int countMedikitPowerUp;
    public int countIcePowerUp;
    public int countMeteoroPowerUp;

}
