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
