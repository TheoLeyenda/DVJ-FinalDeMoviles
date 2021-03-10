using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;
using UnityEngine.UI;
public class ControllerElementSurvivalTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public UIContructionController uiContructionController;
    public List<GameObject> stars;
    public GameObject[] ArrowsObject;
    public Button[] buttonsActivate;
    public CapsuleCollider colliderDialogueSoldier;
    public WeaponManager wm;
    public UINextWave uiNextWave;
    public GameObject camvasTeleport;
    public ActivateFPSController FPS;
    public GameObject camvasStory;
    public GameObject camvasDialogue;
    public GameObject dialogueObject;
    public Dialogue dialogue;
    public Vector3 positionTeleport;
    public FPSController fpsPC;
    public FPSController fpsAndroid;
    private FPSController fps;
    public GameObject ConstructionManager;
    private GameData gd;
    private bool once = true;
    void Start()
    {
        
#if UNITY_STANDALONE
        fps = fpsPC;
#else
        fps = fpsAndroid;
#endif
        gd = GameData.instaceGameData;
        if (gd.gameMode == GameData.GameMode.Survival)
        {
            colliderDialogueSoldier.enabled = false;
            FPS.onceDisable = true;
            FPS.gameObject.SetActive(true);

            //CamvasInventory.SetActive(false);
            //CamvasMain.SetActive(false);
            //WeaponManager.SetActive(false);
            //fps.SetActive(false);


            camvasStory.SetActive(false);
            dialogueObject.SetActive(false);
            camvasDialogue.SetActive(false);
            ConstructionManager.SetActive(true);

            

            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].SetActive(false);
            }
            for (int i = 0; i < ArrowsObject.Length; i++)
            {
                ArrowsObject[i].SetActive(false);
            }
        }
        else
        {
            stars[0].SetActive(true);
            stars[stars.Count - 1].SetActive(false);
        }
    }
    private void Update()
    {
        if (gd.gameMode == GameData.GameMode.Survival)
        {
            for (int i = 0; i < buttonsActivate.Length; i++)
                buttonsActivate[i].interactable = true;

            for (int i = 0; i < gd.nameUnlokedObjects.Count; i++)
            {
                for (int j = 0; j < uiContructionController.buttons.Count; j++)
                {
                    if (gd.nameUnlokedObjects[i] == uiContructionController.buttons[j].nameConstruction)
                    {
                        uiContructionController.buttons[j].lockedButton = false;
                        uiContructionController.buttons[j].CheckDataButton();
                    }
                }
            }
        }
    }
    public void CheckEnableFPS()
    {
        if (gd.gameMode == GameData.GameMode.Survival)
        {
            FPS.gameObject.SetActive(true);
            camvasTeleport.SetActive(true);
            fps.transform.localPosition = positionTeleport;
            fps.lockCursor = false;

        }
        else
        {
            stars[0].SetActive(false);
            stars[stars.Count - 1].SetActive(true);
        }
    }
    public void EnableFire()
    {
        if (once)
        {
            wm.enableShoot = true;
            wm.startParty = true;
            uiNextWave.activateElementsCamvasNextWave = true;
#if UNITY_STANDALONE
            uiNextWave.textStartWave.gameObject.SetActive(true);
#else
            uiNextWave.buttonStartWave.gameObject.SetActive(true);
#endif
            once = false;

            stars[stars.Count - 1].SetActive(false);
        }
    }
}
