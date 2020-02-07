using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkTreeFPS;
using UnityEngine.SceneManagement;
public class UIPauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Image imageButtonPause;
    public Sprite spritePause;
    public Sprite spriteNotPause;
    public GameObject windowsPause;
    public GameObject windowsOptions;
    public GameObject windowsControlsAndroid;
    public GameObject windowsControlsPC;
    public GameObject prefabControles;
    private GameObject currentWindowsControls;
    public Slider sliderVolumen;
    public Slider sliderSensivility;
    public Text textVolumen;
    public Text textSensivility;
    public float multiplaySensivility = 1;
    private FPSController fpsController;
    public FPSController fpsControllerPC;
    public FPSController fpsControllerAndroid;
    public AudioManager audioManager;
    private float maxValue = 100;
    private float porcentageVolumen;
    private float porcentageSensivility;
    private float currentVolumen = 0.5f;
    void Start()
    {
#if UNITY_ANDROID
        fpsController = fpsControllerAndroid;
        currentWindowsControls = windowsControlsAndroid;
        imageButtonPause.gameObject.SetActive(true);
#endif
#if UNITY_STANDALONE
        fpsController = fpsControllerPC;
        currentWindowsControls = windowsControlsPC;
        imageButtonPause.gameObject.SetActive(false);
#endif
        for (int i = 0; i < audioManager.audioSources.Count; i++)
        {
            audioManager.audioSources[i].volume = currentVolumen;
        }
        porcentageVolumen = sliderVolumen.value * maxValue;
        textVolumen.text = "Volumen: " + porcentageVolumen + "%";

        sliderSensivility.value = fpsController.sensitivity.x / 10;
        //Debug.Log(sliderSensivility.value);
        sliderVolumen.value = fpsController.sensitivity.y / 10;
    }
    private void Update()
    {
        CheckImageButtonPause();
        if (windowsOptions.activeSelf)
        {
            ChangeSensivility();
            ChangeVolumen();
        }
    }
    public void CheckImageButtonPause()
    {
        if (windowsOptions.activeSelf || windowsPause.activeSelf || currentWindowsControls.activeSelf)
        {
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
        //Reinicia el nivel
        SceneManager.LoadScene("LoadScene");
    }
    public void ExitLevel()
    {
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
