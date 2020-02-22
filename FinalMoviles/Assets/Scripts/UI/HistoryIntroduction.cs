using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryIntroduction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject go_activate;
    public GameData gd;
    public bool InitialGame;
    private void Start()
    {
        gd = GameData.instaceGameData;
        if (!InitialGame)
        {
            gameObject.SetActive(false);
        }
    }
    public void Play()
    {
        if (gd.gameMode != GameData.GameMode.Survival)
        {
            go_activate.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
