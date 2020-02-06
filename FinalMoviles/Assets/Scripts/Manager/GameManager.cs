using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;

public class GameManager : MonoBehaviour
{
    private bool enableStartGame;
    public bool InTutorial;
    public TutorialElements tutorialElements;
    [HideInInspector]
    public FPSController player;
    [HideInInspector]
    public PlayerStats playerStats;
    public FPSController PlayerPC;
    public FPSController PlayerAndroid;
    public GenerateEnemyManager generateEnemyManager;
    public TeleportController TC;
    public bool isFinishLevel;
    public bool inSurvivalMode;

    [Header("Unloked Items")]
    public bool UnlokedItem;
    public string UnlockedItemName;
    public string NameLevelUnlocked;
    [HideInInspector]
    public GameData gd;

    public UIStadistics uIStadistics;
    public UIGameOver uIGameOver;

    private bool gameOver = false;

    [System.Serializable]
    public struct TutorialElements
    {
        public Dialogue dialogue;
        [HideInInspector]
        public bool skipTutorial;
        public GameObject PrefabEvents;
    }

    [Header("Game Data")]
    public int countLifes;

    /*private void OnEnable()
    {
        //Enemy.OnFinishRoute += SubstractLifes;
    }
    private void OnDisable()
    {
        //Enemy.OnFinishRoute -= SubstractLifes;
    }
    public void SubstractLifes(Enemy e)
    {
        //countLifes = countLifes - e.DamageLifes;
    }*/
    void Start()
    {
        gd = GameData.instaceGameData;
        enableStartGame = false;

#if UNITY_ANDROID
        player = PlayerAndroid;
#endif
#if UNITY_STANDALONE
        player = PlayerPC;
#endif
        playerStats = player.gameObject.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (!gameOver)
        {
            if (generateEnemyManager.GetFinishGenerator() && UnlokedItem)
            {
                gd.UnlokedObject(NameLevelUnlocked);
                gd.UnlokedObject(UnlockedItemName);
                uIStadistics.camvasStadistics.SetActive(true);
                uIStadistics.unlockedConstruction = true;
                player.lockCursor = false;
            }
            else if (generateEnemyManager.GetFinishGenerator())
            {
                gd.UnlokedObject(NameLevelUnlocked);
                uIStadistics.camvasStadistics.SetActive(true);
                uIStadistics.unlockedConstruction = false;
                player.lockCursor = false;
            }
        }
        CheckGameOver();
    }
    public void SetEnableStartGame(bool startGame)
    {
        enableStartGame = startGame;
    }
    public bool GetEnableStartGame()
    {
        return enableStartGame;
    }
    public void CloseWindowsTutorial()
    {
        tutorialElements.dialogue.WindowsSkipTutorial.SetActive(false);
    }
    public void SkipTutorial()
    {
        tutorialElements.dialogue.BarDialogue.SetActive(false);
        CloseWindowsTutorial();
        tutorialElements.skipTutorial = true;
        tutorialElements.PrefabEvents.SetActive(false);
    }
    public void ResumeStoryLevel(int newIndexDialogue)
    {
        if (tutorialElements.dialogue.GetIndexDialogues() < newIndexDialogue)
        {
            if (tutorialElements.skipTutorial)
            {
                if (newIndexDialogue < tutorialElements.dialogue.dialogues.Count)
                {
                    tutorialElements.dialogue.BarDialogue.SetActive(true);
                    tutorialElements.dialogue.dialogues[newIndexDialogue - 1].events = Dialogue.DataDialogue.Events.None;
                    tutorialElements.dialogue.dialogues[newIndexDialogue - 1].events2 = Dialogue.DataDialogue.Events.None;
                    tutorialElements.dialogue.dialogues[newIndexDialogue - 1].events3 = Dialogue.DataDialogue.Events.None;
                    tutorialElements.dialogue.dialogues[newIndexDialogue - 1].events4 = Dialogue.DataDialogue.Events.None;
                    tutorialElements.dialogue.dialogues[newIndexDialogue - 1].events5 = Dialogue.DataDialogue.Events.None;
                    tutorialElements.dialogue.SetIndexDialogues(newIndexDialogue - 1);
                    tutorialElements.dialogue.CheckDialogue();
                }
            }
        }
    }
    public void EnableCursor()
    {
        player.lockCursor = false;
    }
    public void DisableCursor()
    {
        player.lockCursor = true;
    }
    public void CheckGameOver()
    {
        gameOver = true;
        for (int i = 0; i < TC.buttonsTeleports.Count; i++)
        {
            if (!TC.buttonsTeleports[i].disableButton)
            {
                //Debug.Log("ENTRE");
                gameOver = false;
            }
        }
        if (countLifes <= 0 || playerStats.health <= 0)
        {
            gameOver = true;
        }
        if (gameOver)
        {
            //PROGRAMAR LO QUE PASA CUANDO PERDES.
            //Debug.Log("GameOver");
            uIGameOver.gameObject.SetActive(true);
        }
    }
    public void SetGameOver(bool _gameOver)
    {
        gameOver = _gameOver;
    }
    public bool GetGameOver()
    {
        return gameOver;
    }
}
