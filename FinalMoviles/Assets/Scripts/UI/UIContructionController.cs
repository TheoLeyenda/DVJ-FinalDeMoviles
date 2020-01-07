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
        }
        camvasConfirmationConstruction.SetActive(false);
    }
    public void Information(int index)
    {
        if (index < buttons.Count && index >= 0)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].go_button.SetActive(false);
            }
            titulo.text = textInformacion;
            buttons[index].go_Information.SetActive(true);
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
            go.transform.rotation = CM.GetCurrentZoneConstruction().transform.rotation;
            CM.GetCurrentZoneConstruction().SetActive(false);
            CM.camvasContruction.SetActive(false);
            camvasConfirmationConstruction.SetActive(true);
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
    public void RotateStructure()
    {
        currentConstruction.transform.Rotate(Vector3.up, 15);
    }
    public void Exit()
    {
        CM.SetCurrentZoneConstruction(null);
        gameObject.SetActive(false);
    }
}
