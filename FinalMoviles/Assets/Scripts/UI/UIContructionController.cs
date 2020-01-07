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
            }
            else
            {
                nameConstructionText.text = nameConstruction;
                buttonImage.sprite = spriteUnlocked;
                button.interactable = true;
                button.targetGraphic = buttonImage;
            }
        }
    }
    private PoolObject poolObject;
    public ConstructionManager CM;
    public string textInformacion;
    public string textTitulo;
    public Text titulo;
    public List<UIButtonConstruction> buttons;
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
            go.transform.position = CM.GetCurrentZoneConstruction().transform.position;
            go.transform.rotation = CM.GetCurrentZoneConstruction().transform.rotation;
            CM.GetCurrentZoneConstruction().SetActive(false);
        }
    }
    public void ConfirmConstruction(int index)
    {
        if (index < buttons.Count && index >= 0)
        {
            Construction construction;
            construction = buttons[index].poolConstruction.GetObject().GetComponent<Construction>();
            CM.GetCurrentZoneConstruction().SetActive(false);
            construction.SetConstructed(true);
        }
    }
    public void CancelConstruction(int index)
    {
        if (index < buttons.Count && index >= 0)
        {
            Construction construction;
            construction = buttons[index].poolConstruction.GetObject().GetComponent<Construction>();
            if (!construction.GetConstructed())
            {
                buttons[index].poolConstruction.Recycle(construction.gameObject);
                construction.gameObject.SetActive(false);
                CM.GetCurrentZoneConstruction().SetActive(true);
            }
        }
    }
    public void RotateStructure(GameObject go)
    {
        go.transform.Rotate(go.transform.position, 15);
    }

}
