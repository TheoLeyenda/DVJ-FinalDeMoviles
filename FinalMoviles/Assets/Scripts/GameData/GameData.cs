using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;

public class GameData : MonoBehaviour
{
    // Data a guardar.
    public int numberParty;
    public List<string> nameLokedObjects;
    public List<string> nameUnlokedObjects;
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

    void Awake()
    {
        if (instaceGameData == null)
        {
            instaceGameData = this;
            DontDestroyOnLoad(this);
        }
        else if (instaceGameData != null)
        {
            Destroy(this);
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
