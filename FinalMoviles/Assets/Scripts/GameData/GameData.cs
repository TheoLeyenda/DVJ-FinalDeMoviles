using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;

public class GameData : MonoBehaviour
{
    public enum GameMode
    {
        None,
        Survival,
        Story,
    }
    // Data a guardar.
    public GameMode gameMode;
    public int numberParty;
    public List<string> nameLokedObjects;
    public List<string> nameUnlokedObjects;
    //[HideInInspector]
    public List<string> auxNameLokedObjects;
    //[HideInInspector]
    public List<string> auxNameUnlokedObjects;
    public int currentLevel;
    public string currentNameUser = "None";
    public int generalScore;

    public int scoreForDieEnemy = 30;
    public int scoreForHitEnemy = 10;
    public int countEnemysDie;
    public int countBulletsShoots;
    public int currentScore;

    public static GameData instaceGameData;

    //public const int positiveValuePartyCreated = 1;
    //public int PartyCreated = 0; // *si este valor es igual a 0 quiere decir que la partida no fue creada.
                                  // *si este valor es igual a 1 quiere decir que la partida fue creada y debera
                                  // ser cargada automaticamente.

    [System.Serializable]
    public struct InventoryPlayer
    {
        public int maxAmmoScar;
        public int maxAmmoM4;
        public int maxAmmoSniper;

        public int maxCountNukePowerUp;
        public int maxCountLifeUpPowerUp;
        public int maxCountRepairConstructionPowerUp;
        public int maxCountMedikitPowerUp;
        public int maxCountIcePowerUp;
        public int maxCountMeteoroPowerUp;

        public bool unlockedScar;
        public bool unlockedM4;
        public bool unlockedSniper;

        public int scarAmmo;
        public int M4Ammo;
        public int SniperAmmo;

        public int countNukePowerUp;
        public int countLifeUpPowerUp;
        public int countRepairConstructionPowerUp;
        public int countMedikitPowerUp;
        public int countIcePowerUp;
        public int countMeteoroPowerUp; // Cambias a camara vertical y tiras meteoros por todo el mapa este 
                                        // poder dura por cierto tiempo.
    }

    public InventoryPlayer dataPlayer;
    public InventoryPlayer auxDataPlayer;
    void Awake()
    {
        gameMode = GameMode.None;
        if (instaceGameData == null)
        {
            instaceGameData = this;
            DontDestroyOnLoad(this);
        }
        else if (instaceGameData != null)
        {
            Destroy(this);
        }

        auxNameLokedObjects = new List<string>();
        auxNameUnlokedObjects = new List<string>();

        for (int i = 0; i < nameLokedObjects.Count; i++)
        {
            auxNameLokedObjects.Add(nameLokedObjects[i]);
        }
        for (int i = 0; i < nameUnlokedObjects.Count; i++)
        {
            auxNameUnlokedObjects.Add(nameUnlokedObjects[i]);
        }
    }
    private void Start()
    {
        currentLevel = 0;
    }
    private void OnEnable()
    {
        Enemy.LifeIsZero += AddEnemysDie;
        BalisticProjectile.OnPlayerShoot += AddShootPlayer;
    }
    private void OnDisable()
    {
        Enemy.LifeIsZero -= AddEnemysDie;
        BalisticProjectile.OnPlayerShoot -= AddShootPlayer;
    }
    public void SaveAuxData()
    {
        auxDataPlayer.unlockedScar = dataPlayer.unlockedScar;
        auxDataPlayer.unlockedM4 = dataPlayer.unlockedM4;
        auxDataPlayer.unlockedSniper = dataPlayer.unlockedSniper;

        auxDataPlayer.scarAmmo = dataPlayer.scarAmmo;
        auxDataPlayer.M4Ammo = dataPlayer.M4Ammo;
        auxDataPlayer.SniperAmmo = dataPlayer.SniperAmmo;

        auxDataPlayer.countNukePowerUp = dataPlayer.countNukePowerUp;
        auxDataPlayer.countLifeUpPowerUp = dataPlayer.countLifeUpPowerUp;
        auxDataPlayer.countRepairConstructionPowerUp = dataPlayer.countRepairConstructionPowerUp;
        auxDataPlayer.countMedikitPowerUp = dataPlayer.countMedikitPowerUp;
        auxDataPlayer.countIcePowerUp = dataPlayer.countIcePowerUp;
        auxDataPlayer.countMeteoroPowerUp = dataPlayer.countMeteoroPowerUp;
    }
    public void LoadAuxData()
    {
        dataPlayer.unlockedScar = auxDataPlayer.unlockedScar;
        dataPlayer.unlockedM4 = auxDataPlayer.unlockedM4;
        dataPlayer.unlockedSniper = auxDataPlayer.unlockedSniper;

        dataPlayer.scarAmmo = auxDataPlayer.scarAmmo;
        dataPlayer.M4Ammo = auxDataPlayer.M4Ammo;
        dataPlayer.SniperAmmo = auxDataPlayer.SniperAmmo;

        dataPlayer.countNukePowerUp = auxDataPlayer.countNukePowerUp;
        dataPlayer.countLifeUpPowerUp = auxDataPlayer.countLifeUpPowerUp;
        dataPlayer.countRepairConstructionPowerUp = auxDataPlayer.countRepairConstructionPowerUp;
        dataPlayer.countMedikitPowerUp = auxDataPlayer.countMedikitPowerUp;
        dataPlayer.countIcePowerUp = auxDataPlayer.countIcePowerUp;
        dataPlayer.countMeteoroPowerUp = auxDataPlayer.countMeteoroPowerUp;
    }
    public void ClearData()
    {
        dataPlayer.unlockedScar = false;
        dataPlayer.unlockedM4 = false;
        dataPlayer.unlockedSniper = false;

        dataPlayer.scarAmmo = 0;
        dataPlayer.M4Ammo = 0;
        dataPlayer.SniperAmmo = 0;

        dataPlayer.countNukePowerUp = 0;
        dataPlayer.countLifeUpPowerUp = 0;
        dataPlayer.countRepairConstructionPowerUp = 0;
        dataPlayer.countMedikitPowerUp = 0;
        dataPlayer.countIcePowerUp = 0;
        dataPlayer.countMeteoroPowerUp = 0;
    }
    public void AddEnemysDie(Enemy e)
    {
        countEnemysDie++;
    }
    public void AddShootPlayer(BalisticProjectile bp)
    {
        countBulletsShoots++;
    }
    public bool CheckUnlokedObject(string name)
    {
        for(int i = 0; i < nameUnlokedObjects.Count; i++)
        {
            if (nameLokedObjects[i] == name)
            {
                return true;
            }
        }
        return false;
    }
    //public void CreatedParty()
    //{
        //if (PartyCreated != positiveValuePartyCreated)
        //{
            //PartyCreated = positiveValuePartyCreated;
        //}
    //}
    //public int GetPositiveValuePartyCreated()
    //{
        //return 0;
        //return positiveValuePartyCreated;
    //}
    public void UnlokedObject(string name)
    {
        for (int i = 0; i < nameLokedObjects.Count; i++)
        {
            if (name == nameLokedObjects[i])
            {
                nameUnlokedObjects.Add(nameLokedObjects[i]);
                nameLokedObjects[i] = " ";
            }
        }
    }
}
