using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> nameLokedObjects;
    public List<string> nameUnlokedObjects;
    public int currentLevel;

    public int countEnemysDie;
    public int countBulletsShoots;
    public int currentScore;
    public int generalScore;

    public static GameData instaceGameData;

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
            gameObject.SetActive(false);
        }
        
    }
    private void Start()
    {
        currentLevel = 0;
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
