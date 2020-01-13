using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform BarDialoguePosition1;
    public Transform BarDialoguePosition2;
    public GameObject PlayerPC;
    public GameObject PlayerAndroid;
    public GameObject objectActived;
    public Sprite Face;
    public Image imageFaceTalk;
    public List<DataDialogue> dialogues;
    public Text textDialogues;
    public bool activateObjectInFinishDialogue;
    public GameObject BarDialogue;
    protected int indexDialogues;
    private Rigidbody rigPlayer;
    private bool switchPosition;
    [System.Serializable]
    public class DataDialogue
    {
        public string dialogo;
        public Events events;
        public Events events2;
        public Events events3;
        public Events events4;
        public Events events5;
        public List<Button> buttons;
        public List<GameObject> activateObjects;
        public List<GameObject> disableObjects;
        public enum Events
        {
            None,
            ActivateObject,
            ActivateButtons,
            NotNextDialogue,
            SwitchPosition,
            DisableObjects,
            ActivatedObjects,
            DisableButton,
            FredomPlayer,
            FreezePlayer,
        }
        public void ActivateObjectEvent(GameObject go)
        {
            go.SetActive(true);
        }
        public void SetEventDialogue(Events e)
        {
            events = e;
        }
        public void ActivateObjects()
        {
            for (int i = 0; i < activateObjects.Count; i++)
            {
                activateObjects[i].SetActive(true);
            }
        }
        public void DisableObjects()
        {
            for (int i = 0; i < disableObjects.Count; i++)
            {
                disableObjects[i].SetActive(false);
            }
        }
        public void DisableButtons()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = false;
            }
        }
        public void FreezePlayer(Rigidbody rig)
        {
            rig.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    private void OnEnable()
    {
        switchPosition = false;
        transform.position = BarDialoguePosition1.position;
        if (objectActived != null)
        {
            objectActived.SetActive(false);
        }
#if UNITY_STANDALONE
        rigPlayer = PlayerPC.GetComponent<Rigidbody>();
#endif
#if UNITY_ANDROID
        rigPlayer = PlayerAndroid.GetComponent<Rigidbody>();
#endif
        rigPlayer.constraints = RigidbodyConstraints.FreezeAll;
        indexDialogues = 0;
        textDialogues.text = dialogues[indexDialogues].dialogo;
        imageFaceTalk.sprite = Face;
    }
    private void OnDisable()
    {
        rigPlayer.constraints = RigidbodyConstraints.None;
        rigPlayer.constraints = RigidbodyConstraints.FreezeRotation;
    }
    void Update()
    {
        if (Input.GetButtonDown("InputSeleccion") && BarDialogue.activeSelf)
        {
            CheckDialogue();
        }
        
    }

    public void CheckDialogue()
    {
        if (indexDialogues < dialogues.Count)
        {
            if (dialogues[indexDialogues].events != DataDialogue.Events.NotNextDialogue
            && dialogues[indexDialogues].events2 != DataDialogue.Events.NotNextDialogue
            && dialogues[indexDialogues].events3 != DataDialogue.Events.NotNextDialogue
            && dialogues[indexDialogues].events4 != DataDialogue.Events.NotNextDialogue
            && dialogues[indexDialogues].events5 != DataDialogue.Events.NotNextDialogue)
            {
                indexDialogues++;
                if (indexDialogues < dialogues.Count)
                {
                    if (dialogues[indexDialogues].events == DataDialogue.Events.SwitchPosition
                        || dialogues[indexDialogues].events2 == DataDialogue.Events.SwitchPosition
                        || dialogues[indexDialogues].events3 == DataDialogue.Events.SwitchPosition
                        || dialogues[indexDialogues].events4 == DataDialogue.Events.SwitchPosition
                        || dialogues[indexDialogues].events5 == DataDialogue.Events.SwitchPosition)
                    {
                        SwitchDialogBarPosition();
                    }
                }
            }
        }

        if (indexDialogues < dialogues.Count)
        {
            if (dialogues[indexDialogues].events == DataDialogue.Events.DisableButton
                || dialogues[indexDialogues].events2 == DataDialogue.Events.DisableButton
                || dialogues[indexDialogues].events3 == DataDialogue.Events.DisableButton
                || dialogues[indexDialogues].events4 == DataDialogue.Events.DisableButton
                || dialogues[indexDialogues].events5 == DataDialogue.Events.DisableButton)
            {
                dialogues[indexDialogues].DisableButtons();
            }
            if (dialogues[indexDialogues].events == DataDialogue.Events.ActivateButtons
                || dialogues[indexDialogues].events2 == DataDialogue.Events.ActivateButtons
                || dialogues[indexDialogues].events3 == DataDialogue.Events.ActivateButtons
                || dialogues[indexDialogues].events4 == DataDialogue.Events.ActivateButtons
                || dialogues[indexDialogues].events5 == DataDialogue.Events.ActivateButtons)
            {
                if (dialogues[indexDialogues].buttons.Count > 0)
                {
                    for (int i = 0; i < dialogues[indexDialogues].buttons.Count; i++)
                    {
                        dialogues[indexDialogues].buttons[i].interactable = true;
                        dialogues[indexDialogues].buttons[i].gameObject.SetActive(true);
                    }
                }
            }

            if (dialogues[indexDialogues].events == DataDialogue.Events.ActivateObject
                || dialogues[indexDialogues].events2 == DataDialogue.Events.ActivateObject
                || dialogues[indexDialogues].events3 == DataDialogue.Events.ActivateObject
                || dialogues[indexDialogues].events4 == DataDialogue.Events.ActivateObject
                || dialogues[indexDialogues].events5 == DataDialogue.Events.ActivateObject)
            {
                objectActived.SetActive(true);
            }
            if (dialogues[indexDialogues].events == DataDialogue.Events.ActivatedObjects
                || dialogues[indexDialogues].events2 == DataDialogue.Events.ActivatedObjects
                || dialogues[indexDialogues].events3 == DataDialogue.Events.ActivatedObjects
                || dialogues[indexDialogues].events4 == DataDialogue.Events.ActivatedObjects
                || dialogues[indexDialogues].events5 == DataDialogue.Events.ActivatedObjects)
            {
                dialogues[indexDialogues].ActivateObjects();
            }
            if (dialogues[indexDialogues].events == DataDialogue.Events.DisableObjects
                || dialogues[indexDialogues].events2 == DataDialogue.Events.DisableObjects
                || dialogues[indexDialogues].events3 == DataDialogue.Events.DisableObjects
                || dialogues[indexDialogues].events4 == DataDialogue.Events.DisableObjects
                || dialogues[indexDialogues].events5 == DataDialogue.Events.DisableObjects)
            {
                dialogues[indexDialogues].DisableObjects();
            }
            if(dialogues[indexDialogues].events == DataDialogue.Events.FredomPlayer
                || dialogues[indexDialogues].events2 == DataDialogue.Events.FredomPlayer
                || dialogues[indexDialogues].events3 == DataDialogue.Events.FredomPlayer
                || dialogues[indexDialogues].events4 == DataDialogue.Events.FredomPlayer
                || dialogues[indexDialogues].events5 == DataDialogue.Events.FredomPlayer)
            {
                rigPlayer.constraints = RigidbodyConstraints.None;
                rigPlayer.constraints = RigidbodyConstraints.FreezeRotation;
            }
            if (dialogues[indexDialogues].events == DataDialogue.Events.FreezePlayer
               || dialogues[indexDialogues].events2 == DataDialogue.Events.FreezePlayer
               || dialogues[indexDialogues].events3 == DataDialogue.Events.FreezePlayer
               || dialogues[indexDialogues].events4 == DataDialogue.Events.FreezePlayer
               || dialogues[indexDialogues].events5 == DataDialogue.Events.FreezePlayer)
            {
                dialogues[indexDialogues].FreezePlayer(rigPlayer);
            }
            
            textDialogues.text = dialogues[indexDialogues].dialogo;
        }
        else if (indexDialogues >= dialogues.Count)
        {
            if (activateObjectInFinishDialogue)
            {
                objectActived.SetActive(true);
            }
            BarDialogue.gameObject.SetActive(false);
        }
    }
    public void SetIndexDialogues(int index)
    {
        indexDialogues = index;
    }
    public int GetIndexDialogues()
    {
        return indexDialogues;
    }
    public void SwitchDialogBarPosition()
    {
        switchPosition = !switchPosition;
        if (switchPosition)
        {
            BarDialogue.transform.position = BarDialoguePosition2.position;
        }
        else if (!switchPosition)
        {
            BarDialogue.transform.position = BarDialoguePosition1.position;
        }
    }
}
