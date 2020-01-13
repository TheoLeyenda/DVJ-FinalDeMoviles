using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryIntroduction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject go_activate;
    public bool InitialGame;
    private void Start()
    {
        if (!InitialGame)
        {
            gameObject.SetActive(false);
        }
    }
    public void Play()
    {
        go_activate.SetActive(true);
        gameObject.SetActive(false);
    }
}
