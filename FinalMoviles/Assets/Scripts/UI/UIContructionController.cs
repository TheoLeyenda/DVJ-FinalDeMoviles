using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContructionController : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class UIButtonConstruction
    {
        public Button informationButton;
        public Pool poolConstruction;
        public GameObject go_button;
        public GameObject go_Information;
        public Button button;
        public Text nameConstructionText;
        public Image buttonImage;
        public Sprite spriteUnlocked;
        public Sprite spriteLocked;
        public string nameLockedConstruction = "Bloqueado";
        public string nameConstruction;
        public bool lockedButton;
        public void CheckDataButton()
        {
            if (lockedButton)
            {
                nameConstructionText.text = nameLockedConstruction;
                buttonImage.sprite = spriteLocked;
                button.interactable = false;
                button.targetGraphic = null;
                informationButton.interactable = false;
            }
            else
            {
                nameConstructionText.text = nameConstruction;
                buttonImage.sprite = spriteUnlocked;
                button.interactable = true;
                button.targetGraphic = buttonImage;
                informationButton.interactable = true;
            }
        }
    }
    private PoolObject poolObject;
    private GameObject currentConstruction;
    private int currentIndex;
    private Construction construction;
    public ConstructionManager CM;
    public string textInformacion;
    public string textTitulo;
    public Text titulo;
    public List<UIButtonConstruction> buttons;
    public GameObject camvasConfirmationConstruction;
    private void OnEnable()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].CheckDataButton();
            buttons[i].go_Information.SetActive(false);
        }
        camvasConfirmationConstruction.SetActive(false);
        CM.ButtonBack.SetActive(false);
    }
    public void Information(int index)
    {
        if (index < buttons.Count && index >= 0)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].go_button.SetActive(false);
            }
            titulo.text = buttons[index].nameConstructionText.text;
            buttons[index].go_Information.SetActive(true);
            CM.ButtonExit.SetActive(false);
            CM.ButtonBack.SetActive(true);
        }

        //mostrara la informacion de un boton en especifico
    }
    public void BackToConstruction()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].go_Information.activeSelf)
            {
                buttons[i].go_Information.SetActive(false);
            }
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].go_button.SetActive(true);
        }
        titulo.text = textTitulo;
        CM.ButtonExit.SetActive(true);
        CM.ButtonBack.SetActive(false);

    }
    public void PreviousConstruction(int index)
    {
        if (index < buttons.Count && index >= 0)
        {
            GameObject go;
            go = buttons[index].poolConstruction.GetObject();
            currentConstruction = go;
            currentIndex = index;
            construction = go.GetComponent<Construction>();
            go.transform.position = CM.GetCurrentZoneConstruction().transform.position;
            if (construction.rotateStart)
            {
                go.transform.rotation = CM.GetCurrentZoneConstruction().transform.rotation;
            }
            CM.GetCurrentZoneConstruction().SetActive(false);
            CM.camvasContruction.SetActive(false);
            CM.buttonDerecha.SetActive(false);
            CM.buttonIzquierda.SetActive(false);
            camvasConfirmationConstruction.SetActive(true);
            if (construction.valueDown > 0)
            {
                construction.DownMovement();
            }
            if(construction.valueUp > 0)
            {
                construction.UpMovement();
            }
        }
    }
    public void ConfirmConstruction()
    {
        if (construction != null)
        {
            CM.GetCurrentZoneConstruction().SetActive(false);
            construction.SetConstructed(true);
            construction = null;
            camvasConfirmationConstruction.SetActive(false);
            CM.CheckActivationButtons();
        }
    }
    public void CancelConstruction()
    {
        if (construction != null)
        {
            buttons[currentIndex].poolConstruction.Recycle(construction.gameObject);
            construction.gameObject.SetActive(false);
            CM.GetCurrentZoneConstruction().SetActive(true);
            CM.camvasContruction.SetActive(true);
            construction = null;
            camvasConfirmationConstruction.SetActive(false);
        }
    }
    public void RotateStructure(int angle)
    {
        if (construction != null)
        {
            if (construction.rotateStart)
            {
                currentConstruction.transform.Rotate(Vector3.up, angle);
            }
            else
            {
                currentConstruction.transform.Rotate(Vector3.back, angle);
            }
        }
    }
    public void Exit()
    {
        CM.SetCurrentZoneConstruction(null);
        gameObject.SetActive(false);
        CM.CheckActivationButtons();
    }
}
