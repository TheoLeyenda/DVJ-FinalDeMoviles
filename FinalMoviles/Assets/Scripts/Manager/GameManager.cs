using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    private bool enableStartGame;
    public bool InTutorial;
    public TutorialElements tutorialElements;
    private FPSController player;
    public FPSController PlayerPC;
    public FPSController PlayerAndroid;
    public GenerateEnemyManager generateEnemyManager;

    [Header("Unloked Items")]
    public bool UnlokedItem;
    public string UnlockedItemName;
    public string NameLevelUnlocked;
    [HideInInspector]
    public GameData gd;

    public UIStadistics uIStadistics;

    [System.Serializable]
    public struct TutorialElements
    {
        public Dialogue dialogue;
        [HideInInspector]
        public bool skipTutorial;
        public GameObject PrefabEvents;
    }

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
    }
    private void Update()
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
}
