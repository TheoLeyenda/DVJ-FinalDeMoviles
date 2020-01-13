using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool enableStartGame;
    public bool InTutorial;
    void Start()
    {
        enableStartGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetEnableStartGame(bool startGame)
    {
        enableStartGame = startGame;
    }
    public bool GetEnableStartGame()
    {
        return enableStartGame;
    }
}
