using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISeleccionLevel : MonoBehaviour
{
    [System.Serializable]
    public class ButtonLevel
    {
        public string NameButton;
        public Image UnlockedImage;
        public Sprite UnlockedSprite;
        public Sprite LockedSprite;
        public Button button;
        public GameObject textNumberLevel;
        public bool interactable;

        public void CheckButton()
        {
            if (interactable)
            {
                UnlockedImage.sprite = UnlockedSprite;
                button.interactable = true;
                textNumberLevel.SetActive(true);
            }
            else
            {
                UnlockedImage.sprite = LockedSprite;
                button.interactable = false;
                textNumberLevel.SetActive(false);
            }
        }
    }
    //(LLENAR ESTA LISTA)
    public List<ButtonLevel> buttonLevels;
    [SerializeField]
    private GameData gd;
    // Start is called before the first frame update
    void Start()
    {
        gd = GameData.instaceGameData;
        CheckButtonsLevels();
       
    }
    public void CheckButtonsLevels()
    {
        for (int i = 0; i < buttonLevels.Count; i++)
        {
            for (int j = 0; j < gd.nameUnlokedObjects.Count; j++)
            {
                if (buttonLevels[i].NameButton == gd.nameUnlokedObjects[j])
                {
                    buttonLevels[i].interactable = true;
                }
                buttonLevels[i].CheckButton();
            }
        }
        
    }
}
