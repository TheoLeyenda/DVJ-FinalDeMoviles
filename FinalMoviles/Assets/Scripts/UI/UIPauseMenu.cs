using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkTreeFPS;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
public class UIPauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Image imageButtonPause;
    //public RectTransform imageLook;

    public Sprite spritePause;
    public Sprite spriteNotPause;
    public GameObject windowsPause;
    public GameObject windowsOptions;
    public GameObject windowsControlsAndroid;
    public GameObject windowsControlsPC;
    public GameObject prefabControles;
    private GameObject currentWindowsControls;
    public Slider sliderVolumen;
    //public Slider sliderLook;
    public Slider sliderSensivility;
    //public Text textScaleLook;
    public Text textVolumen;
    public Text textSensivility;
    public float multiplaySensivility = 1;
    public float multiplayScale = 10f;
    private FPSController fpsController;
    public FPSController fpsControllerPC;
    public FPSController fpsControllerAndroid;
    public AudioManager audioManager;
    private float maxValue = 100;
    private float porcentageVolumen;
    private float porcentageSensivility;
    private float porcentageScaleLook;
    private float currentVolumen = 0.5f;
    private GameData gd;

    

    [Header("Scale And Position Slider Sensivility")]
    public Vector3 scaleAndroidSliderSensivility;
    public Vector3 scalePCSliderSensivility;
    public Vector3 positionAndroidSliderSensivility;
    public Vector3 positionPCSliderSensivility;

    [Header("Objects Options Type Control Android")]
    public GameObject touchPadControl;
    public TouchPad scriptTouchPad;
    public GameObject analogicControl;
    public Joystick scriptAnalogic;

    public Vector3 positionAnalogicJumpAndroid;
    public Vector3 positionAnalogicLookAndroid;
    public Vector3 positionAnalogicRunAndroid;

    public Vector3 positionTouchPadJumpAndroid;
    public Vector3 positionTouchPadLookAndroid;
    public Vector3 positionTouchPadRunAndroid;

    public GameObject buttonJumpAndroid;
    public GameObject buttonLookAndroid;
    public GameObject buttonRunAndroid;
    public GameObject buttonTypeControl;
    public Text textOptionSelected;


    void Start()
    {
        gd = GameData.instaceGameData;
#if UNITY_ANDROID
        fpsController = fpsControllerAndroid;
        currentWindowsControls = windowsControlsAndroid;
        imageButtonPause.gameObject.SetActive(true);
        sliderSensivility.transform.localScale = scaleAndroidSliderSensivility;
        sliderSensivility.transform.localPosition = positionAndroidSliderSensivility;
        buttonTypeControl.SetActive(true);
        switch (gd.typeAndroidControl)
        {
            case GameData.TypeAndroidControl.Analogic:
                analogicControl.SetActive(true);
                touchPadControl.SetActive(false);
                scriptAnalogic.enabled = true;
                scriptTouchPad.enabled = false;
                buttonJumpAndroid.transform.localPosition = positionAnalogicJumpAndroid;
                buttonLookAndroid.transform.localPosition = positionAnalogicLookAndroid;
                buttonRunAndroid.transform.localPosition = positionAnalogicRunAndroid;
                textOptionSelected.text = "Analogico";
                break;
            case GameData.TypeAndroidControl.TouchPad:
                touchPadControl.SetActive(true);
                analogicControl.SetActive(false);
                scriptTouchPad.enabled = true;
                scriptAnalogic.enabled = false;
                buttonJumpAndroid.transform.localPosition = positionTouchPadJumpAndroid;
                buttonLookAndroid.transform.localPosition = positionTouchPadLookAndroid;
                buttonRunAndroid.transform.localPosition = positionTouchPadRunAndroid;
                textOptionSelected.text = "Pad";
                break;
        }
#endif
#if UNITY_STANDALONE
        fpsController = fpsControllerPC;
        currentWindowsControls = windowsControlsPC;
        imageButtonPause.gameObject.SetActive(false);
        sliderSensivility.transform.localScale = scalePCSliderSensivility;
        sliderSensivility.transform.localPosition = positionPCSliderSensivility;
        buttonTypeControl.SetActive(false);
#endif
        //for (int i = 0; i < audioManager.audioSources.Count; i++)
        //{
           // audioManager.audioSources[i].volume = currentVolumen;
        //}
        //porcentageVolumen = sliderVolumen.value * maxValue;
        //textVolumen.text = "Volumen: " + porcentageVolumen + "%";

        sliderSensivility.value = gd.sensivility.x / 10;
        sliderSensivility.value = gd.sensivility.y / 10;
        
        porcentageSensivility = sliderSensivility.value * maxValue;
        textSensivility.text = "Senibilidad: " + Mathf.Round(porcentageSensivility) + "%";

       
        //sliderLook.value = gd.scaleLook.x / 10;
        //sliderLook.value = gd.scaleLook.y / 10;
        //porcentageScaleLook = sliderLook.value * maxValue;
        //textScaleLook.text = "Tamaño De La Mira: " + Mathf.Round(porcentageSensivility) +"%";
    }
    public void SwitchControlAndroid()
    {
        if (gd.typeAndroidControl == GameData.TypeAndroidControl.Analogic)
        {
            gd.typeAndroidControl = GameData.TypeAndroidControl.TouchPad;
        }
        else if (gd.typeAndroidControl == GameData.TypeAndroidControl.TouchPad)
        {
            gd.typeAndroidControl = GameData.TypeAndroidControl.Analogic;
        }
        switch (gd.typeAndroidControl)
        {
            case GameData.TypeAndroidControl.Analogic:
                analogicControl.SetActive(true);
                touchPadControl.SetActive(false);
                scriptAnalogic.enabled = true;
                scriptTouchPad.enabled = false;
                buttonJumpAndroid.transform.localPosition = positionAnalogicJumpAndroid;
                buttonLookAndroid.transform.localPosition = positionAnalogicLookAndroid;
                buttonRunAndroid.transform.localPosition = positionAnalogicRunAndroid;
                textOptionSelected.text = "Analogico";
                
                break;
            case GameData.TypeAndroidControl.TouchPad:
                touchPadControl.SetActive(true);
                analogicControl.SetActive(false);
                scriptTouchPad.enabled = true;
                scriptAnalogic.enabled = false;
                buttonJumpAndroid.transform.localPosition = positionTouchPadJumpAndroid;
                buttonLookAndroid.transform.localPosition = positionTouchPadLookAndroid;
                buttonRunAndroid.transform.localPosition = positionTouchPadRunAndroid;
                textOptionSelected.text = "Pad";
                break;
        }
    }
    private void Update()
    {
        if(gd == null)
        {
            gd = GameData.instaceGameData;
        }
        CheckImageButtonPause();
        if (windowsOptions.activeSelf)
        {
            ChangeSensivility();
            //ChangeVolumen();
            gd.sensivility.x = sliderSensivility.value * multiplaySensivility;
            gd.sensivility.y = sliderSensivility.value * multiplaySensivility;
            //imageLook.localScale = (new Vector3(sliderLook.value,sliderLook.value, 1)*multiplayScale);
            //gd.scaleLook = imageLook.localScale;
        }
        
    }
    private void OnDisable()
    {
        gd.sensivility.x = (sliderSensivility.value * multiplaySensivility) * 2;
        gd.sensivility.y = (sliderSensivility.value * multiplaySensivility) * 2;

        //gd.scaleLook.x = (sliderLook.value * multiplayScale) * 2;
        //gd.scaleLook.y = (sliderLook.value * multiplayScale) * 2;
    }
    public void CheckImageButtonPause()
    {
        if (windowsOptions.activeSelf || windowsPause.activeSelf || currentWindowsControls.activeSelf)
        {
            fpsControllerPC.lockCursor = false;
            imageButtonPause.sprite = spritePause;
        }
        else
        {
            imageButtonPause.sprite = spriteNotPause;
        }
    }
    public void ChangeVolumen()
    {
        porcentageVolumen = sliderVolumen.value * maxValue;
        textVolumen.text = "Volumen: " + Mathf.Round(porcentageVolumen) + "%";
        for (int i = 0; i < audioManager.audioSources.Count; i++)
        {
            audioManager.audioSources[i].volume = currentVolumen;
        }
    }
    public void ChangeSensivility()
    {
        fpsController.sensitivity.x = sliderSensivility.value * multiplaySensivility;
        fpsController.sensitivity.y = sliderSensivility.value * multiplaySensivility;
        //fpsController.smoothing = fpsController.sensitivity;
        if (fpsController.sensitivity.x <= 0 && fpsController.sensitivity.y <= 0)
        {
            fpsController.sensitivity.x = 0.01f;
            fpsController.sensitivity.y = 0.01f;
        }
        porcentageSensivility = sliderSensivility.value * maxValue;
        textSensivility.text = "Senibilidad: " + Mathf.Round(porcentageSensivility) + "%";
    }
    public void Reanude()
    {
        fpsControllerPC.lockCursor = true;
        Time.timeScale = 1;
        windowsPause.SetActive(false);
        windowsOptions.SetActive(false);
    }
    public void Options()
    {
        windowsPause.SetActive(false);
        windowsOptions.SetActive(true);
    }
    public void RestartLevel()
    {
        if (gd.gameMode == GameData.GameMode.Survival)
        {
            gd.ClearData();
        }
        gd.currentScore = 0;
        Time.timeScale = 1;
        //Reinicia el nivel
        SceneManager.LoadScene("LoadScene");
        
    }
    public void ExitLevel()
    {
        /*if (gd.gameMode == GameData.GameMode.Survival)
        {
            gd.LoadAuxData();
        }*/
        gd.generalScore = gd.generalScore + gd.currentScore;
        gd.currentScore = 0;
        Time.timeScale = 1;
        //Sale a la pantalla de loby.
        SceneManager.LoadScene("LobyScene");
    }
    public void Controls()
    {
        prefabControles.SetActive(true);
        currentWindowsControls.SetActive(true);
        windowsOptions.SetActive(false);
    }
    public void BackToPauseMenu()
    {
        windowsPause.SetActive(true);
        windowsOptions.SetActive(false);
    }
    public void BackToOptions()
    {
        prefabControles.SetActive(false);
        currentWindowsControls.SetActive(false);
        windowsOptions.SetActive(true);
    }
}
