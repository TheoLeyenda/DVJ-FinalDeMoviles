using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkTreeFPS;

public class PowerUpController : MonoBehaviour
{
    public GameObject lookSniperPC;
    public GameObject lookSniperAndroid;
    private GameObject lookSniper;
    private GameData gd;
    public InputManager inputManager;
    public GameManager gm;
    public WeaponManager wm;
    public GameObject frameworkPowerUps;
    public GameObject buttonOpenFrameWorkPowerUps;
    private bool enableFrameworkPowerUps = false;
    public FPSController playerPC;
    public FPSController playerAndroid;
    public GameObject camvasAndroid;
    public Text textCountLifes;

    [Header("DATA: PowerUp Nuke")]
    public Button buttonNuke;
    public Text textButtonNuke;
    public GameObject objectNuke;
    public float delayNuke;
    public float auxDelayNuke;
    private bool inNuke = false;

    [Header("DATA: PowerUp LifeUp")]
    public Button buttonLifeUp;
    public Text textButtonLifeUp;
    public int countLifeRecovered;

    [Header("DATA: PowerUp RepairConstruction")]
    public Button buttonRepairConstruction;
    public Text textButtonRepairConstruction;
    public int countRepairRecovered;
    public GameObject objectRepairConstructions;
    public float delayRepairConstruction;
    public float auxDelayRepairConstruction;
    private bool inRepairConstructions = false;

    [Header("DATA: PowerUp Medikit")]
    public Button buttonMedikit;
    public Text textButtonMedikit;
    public int countHealthRecovered;
    public int countMaxHealth = 100;

    [Header("DATA: PowerUp Ice")]
    public Button buttonIce;
    public Text textButtonIce;
    public float timeIceEnemy;
    public float auxTimeIceEnemy;
    private bool inIcePowerUp = false;
    public GameObject objectIce;

    [Header("DATA: PowerUp Meteoro")]
    public Button buttonMeteoro;
    public Text textButtonMeteoro;
    public float timeMeteoroON;
    public float auxTimeMeteoroON;
    public GameObject cameraMeteoro;
    private bool inMeteorito;

    private PlayerStats playerStats;
    private FPSController player;

    private void Start()
    {
        player = gm.player;
        if (player == null)
        {
#if UNITY_ANDROID
        player = playerAndroid;
        lookSniper = lookSniperAndroid;
#endif
#if UNITY_STANDALONE
            player = playerPC;
            lookSniper = lookSniperPC;
#endif
        }
        playerStats = player.gameObject.GetComponent<PlayerStats>();
        //Debug.Log(player);
        gd = GameData.instaceGameData;
        cameraMeteoro.SetActive(false);
    }
    private void Update()
    {
        
        CheckEnableButtons();
        CheckPowerUpNuke();
        CheckPowerUpIce();
        CheckPowerUpRepairConstruction();
        CheckPowerUpMeteorito();
        if (Input.GetKeyDown(inputManager.inventoryPowerUp) && wm.enableShoot)
        {
            CheckEnableFrameworkPowerUps();
        }
        else if ((!wm.enableShoot || lookSniper.activeSelf) && buttonOpenFrameWorkPowerUps.activeSelf)
        {
            buttonOpenFrameWorkPowerUps.SetActive(false);
        }
        else if (wm.enableShoot)
        {
            if (!buttonOpenFrameWorkPowerUps.activeSelf && !lookSniper.activeSelf)
            {
                buttonOpenFrameWorkPowerUps.SetActive(true);
            }
        } 
    }
    public void CheckEnableButtons()
    {
        if (gd.dataPlayer.countNukePowerUp > 0)
        {
            buttonNuke.interactable = true;
            textButtonNuke.text = "" + gd.dataPlayer.countNukePowerUp;
        }
        else
        {
            buttonNuke.interactable = false;
            textButtonNuke.text = "0";
        }

        if (gd.dataPlayer.countLifeUpPowerUp > 0)
        {
            buttonLifeUp.interactable = true;
            textButtonLifeUp.text = "" + gd.dataPlayer.countLifeUpPowerUp;
        }
        else
        {
            buttonLifeUp.interactable = false;
            textButtonLifeUp.text = "0";
        }

        if (gd.dataPlayer.countRepairConstructionPowerUp > 0)
        {
            buttonRepairConstruction.interactable = true;
            textButtonRepairConstruction.text = "" + gd.dataPlayer.countRepairConstructionPowerUp;
        }
        else
        {
            buttonRepairConstruction.interactable = false;
            textButtonRepairConstruction.text = "0";
        }

        if (gd.dataPlayer.countMedikitPowerUp > 0)
        {
            buttonMedikit.interactable = true;
            textButtonMedikit.text = "" + gd.dataPlayer.countMedikitPowerUp;
        }
        else
        {
            buttonMedikit.interactable = false;
            textButtonMedikit.text = "0";
        }

        if (gd.dataPlayer.countIcePowerUp > 0)
        {
            buttonIce.interactable = true;
            textButtonIce.text = "" + gd.dataPlayer.countIcePowerUp;
        }
        else
        {
            buttonIce.interactable = false;
            textButtonIce.text = "0";
        }

        if (gd.dataPlayer.countMeteoroPowerUp > 0)
        {
            buttonMeteoro.interactable = true;
            textButtonMeteoro.text = "" + gd.dataPlayer.countMeteoroPowerUp;
        }
        else
        {
            buttonMeteoro.interactable = false;
            textButtonMeteoro.text = "0";
        }
    }
    public void CheckEnableFrameworkPowerUps()
    {
        enableFrameworkPowerUps = !enableFrameworkPowerUps;
        if (enableFrameworkPowerUps)
        {
            gm.player.lockCursor = false;
            frameworkPowerUps.SetActive(true);
        }
        else
        {
#if !UNITY_ANDROID
            gm.player.lockCursor = true;
#endif
            frameworkPowerUps.SetActive(false);
        }
    }

    public void PowerUpNuke()
    {
        inNuke = true;
        gd.dataPlayer.countNukePowerUp--;
    }
    public void CheckPowerUpNuke()
    {
        if (inNuke)
        {
            if (delayNuke > 0)
            {
                objectNuke.SetActive(true);
                delayNuke = delayNuke - Time.deltaTime;
            }
            else if (delayNuke <= 0)
            {
                objectNuke.SetActive(false);
                inNuke = false;
                delayNuke = auxDelayNuke;
            }
        }
    }
    public void PowerUpLifeUp()
    {
        gm.countLifes = gm.countLifes + countLifeRecovered;
        textCountLifes.text = "" + gm.countLifes;
        gd.dataPlayer.countLifeUpPowerUp--;
    }

    public void PowerUpRepairConstruction()
    {
        inRepairConstructions = true;
        gd.dataPlayer.countRepairConstructionPowerUp--;
    }
    public void CheckPowerUpRepairConstruction()
    {
        if (inRepairConstructions)
        {
            if (delayRepairConstruction > 0)
            {
                delayRepairConstruction = delayRepairConstruction - Time.deltaTime;
                objectRepairConstructions.SetActive(true);
            }
            else if (delayRepairConstruction <= 0)
            {
                delayRepairConstruction = auxDelayRepairConstruction;
                objectRepairConstructions.SetActive(false);
                inRepairConstructions = false;
            }
        }
    }

    public void PowerUpMedikit()
    {
        playerStats.health = playerStats.health + countHealthRecovered;
        if (playerStats.health > countMaxHealth)
        {
            playerStats.health = countMaxHealth;
        }
        gd.dataPlayer.countMedikitPowerUp--;
    }

    public void PowerUpIce()
    {
        inIcePowerUp = true;
        gd.dataPlayer.countIcePowerUp--;
    }
    public void CheckPowerUpIce()
    {
        if (inIcePowerUp)
        {
            if (timeIceEnemy > 0)
            {
                timeIceEnemy = timeIceEnemy - Time.deltaTime;
                objectIce.SetActive(true);
            }
            else if (timeIceEnemy <= 0)
            {
                timeIceEnemy = auxTimeIceEnemy;
                objectIce.SetActive(false);
                inIcePowerUp = false;
            }
        }
    }

    public void PowerUpMeteoro()
    {
        inMeteorito = true;
        gd.dataPlayer.countMeteoroPowerUp--;
        //PROGRAMAR LOS METEORITOS.
    }
    public void CheckPowerUpMeteorito()
    {
        if (inMeteorito)
        {
            if (timeMeteoroON > 0)
            {
                camvasAndroid.SetActive(false);
                timeMeteoroON = timeMeteoroON - Time.deltaTime;
                cameraMeteoro.SetActive(true);
                player.gameObject.SetActive(false);
                player.lockCursor = false;
                
            }
            else if (timeMeteoroON <= 0)
            {
                camvasAndroid.SetActive(true);
                player.gameObject.SetActive(true);
                player.lockCursor = true;
                timeMeteoroON = auxTimeMeteoroON;
                cameraMeteoro.SetActive(false);
                inMeteorito = false;
            }
        }
    }
    // CRAR SCRIPTS PROPIOS PARA EL PowerUp DE LOS METEORITOS, LA NUKE, EL ICE (CONGELAMIENTO) Y PARA 
    //LA REPARACION DE EDIFICIOS
}
