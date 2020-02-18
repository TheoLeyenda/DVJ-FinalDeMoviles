using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instanceGameManager;
    public GameObject arrowTutorial;
    public Button[] enableButtonsTutorial;
    public ConstructionManager CM;
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
    public GameObject MenuPause;
    public InputManager inputManager;
    public bool isFinishLevel;
    public bool inSurvivalMode;
    public GameObject CountdownTitle;
    public GameObject CountdownText;
    public GameObject checkEventsGameInTutorial;
    [Header("Unloked Items")]
    public bool UnlokedItem;
    public string UnlockedItemName;
    public string NameLevelUnlocked;
    [HideInInspector]
    public bool lockedTutorial;
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
    void Awake()
    {
        if (instanceGameManager == null)
        {
            instanceGameManager = this;
        }
        else if (instanceGameManager != null)
        {
            Destroy(this);
        }

    }
    void Start()
    {
        lockedTutorial = false;
        gd = GameData.instaceGameData;
        enableStartGame = false;

#if UNITY_ANDROID
        player = PlayerAndroid;
#endif
#if UNITY_STANDALONE
        player = PlayerPC;
#endif
        playerStats = player.gameObject.GetComponent<PlayerStats>();
        if (!generateEnemyManager.usingDelayStartRound)
        {
            CountdownTitle.SetActive(false);
            CountdownText.SetActive(false);
        }
        for (int i = 0; i < gd.nameUnlokedObjects.Count; i++)
        {
            if (gd.nameUnlokedObjects[i] == "Nivel 2")
            {
                lockedTutorial = true;
                if (arrowTutorial != null)
                {
                    arrowTutorial.SetActive(false);
                }
            }
        }
        if (lockedTutorial)
        {
            for (int i = 0; i < enableButtonsTutorial.Length; i++)
            {
                enableButtonsTutorial[i].interactable = true;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(inputManager.Pause))
        {
            GamePause();
        }
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
    public void GamePause()
    {
        Time.timeScale = 0;
        MenuPause.SetActive(true);
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
