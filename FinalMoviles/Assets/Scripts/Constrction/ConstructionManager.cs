using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ConstructionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StartGameButton;
    public List<GameObject> objectsDisables;
    public List<GameObject> objectsActivate;
    public List<GameObject> constructionZone;
    public List<GameObject> waypoints;
    public GameObject waypointGeneralVision;
    public int rangeOfRayCast;
    public GameObject cursorCamera;
    public float speedTraslationCamera;
    public GameObject buttonIzquierda;
    public GameObject buttonDerecha;
    public GameObject buttonMap;
    public GameObject camvasContruction;
    public GameManager gm;
    public GameObject ButtonBack;
    public GameObject ButtonExit;
    private int indexConstructionZone;
    private Vector3 finishPositionCamera;
    private float magnitudeFinishMovementCamera = 0.1f;
    private bool inGeneralVision = false;
    private GameObject currentZoneConstruction;
    private bool enableClickConstruction = true;
    //public Camera cameraInConstruction;
    public static event Action<ConstructionManager> OnClickConstructionZone;
    public static event Action<ConstructionManager> OnClickGeneralView;
    public static event Action<ConstructionManager> OnClickPlayButton;

    [HideInInspector]
    public ConstructionZone CZ;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartGameButton.SetActive(false);
        indexConstructionZone = 0;
        SetActiveObjects(false,objectsDisables);
        SetActiveObjects(true, objectsActivate);
        camvasContruction.SetActive(false);
    }
    private void OnDisable()
    {
        SetActiveObjects(true,objectsDisables);
        SetActiveObjects(false, objectsActivate);
    }
    public void SetActiveObjects(bool _active,List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(_active);
        }
    }
    private void Start()
    {
        indexConstructionZone = 0;
        if (constructionZone[0] != null)
        {
            cursorCamera.transform.position = waypoints[indexConstructionZone].transform.position;
            cursorCamera.transform.rotation = waypoints[indexConstructionZone].transform.rotation;
        }
    }
    private void Update()
    {
        CheckClickInConstructionZone();
    }
    public void CheckStartGameEnable()
    {
        bool enableStartGame = true;
        for (int i = 0; i < constructionZone.Count; i++)
        {
            if (constructionZone[i].activeSelf)
            {
                enableStartGame = false;
            }
        }
        gm.SetEnableStartGame(enableStartGame);
    }
    public void CheckClickInConstructionZone()
    {
        if (Camera.main != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, rangeOfRayCast) && Input.GetButtonDown("InputSeleccion") && !camvasContruction.activeSelf && enableClickConstruction)
            {
                if (hit.transform != null)
                {
                    if (hit.transform.tag == "ZonaConstruccion")
                    {
                        //Debug.Log("ENTRE");
                        CZ = hit.transform.gameObject.GetComponent<ConstructionZone>();
                        if (OnClickConstructionZone != null && gm.InTutorial)
                        {
                            OnClickConstructionZone(this);
                        }
                        camvasContruction.SetActive(true);
                        currentZoneConstruction = hit.transform.gameObject;
                        buttonDerecha.SetActive(false);
                        buttonIzquierda.SetActive(false);
                        buttonMap.SetActive(false);
                    }
                }
            }
        }
    }
    public void GeneralView()
    {
        enableClickConstruction = !enableClickConstruction;
        inGeneralVision = !inGeneralVision;
        if (gm.InTutorial && OnClickGeneralView != null)
        {
            OnClickGeneralView(this);
        }
        if (inGeneralVision)
        {
            CheckStartGameEnable();
            cursorCamera.transform.position = waypointGeneralVision.transform.position;
            cursorCamera.transform.rotation = waypointGeneralVision.transform.rotation;
            buttonDerecha.SetActive(false);
            buttonIzquierda.SetActive(false);
            if (gm.GetEnableStartGame())
            {
                StartGameButton.SetActive(true);
            }
        }
        else
        {
            cursorCamera.transform.position = waypoints[indexConstructionZone].transform.position;
            cursorCamera.transform.rotation = waypoints[indexConstructionZone].transform.rotation;
            if (indexConstructionZone <= 0)
            {
                buttonDerecha.SetActive(true);
                buttonIzquierda.SetActive(false);
            }
            else if (indexConstructionZone >= constructionZone.Count - 1)
            {
                buttonDerecha.SetActive(false);
                buttonIzquierda.SetActive(true);
            }
            else
            {
                buttonDerecha.SetActive(true);
                buttonIzquierda.SetActive(true);
            }
            StartGameButton.SetActive(false);
        }

        
    }
    public void CheckActivationButtons()
    {
        if (indexConstructionZone >= constructionZone.Count - 1)
        {
            buttonDerecha.SetActive(false);
            buttonIzquierda.SetActive(true);
        }
        else if (indexConstructionZone <= 0)
        {
            buttonDerecha.SetActive(true);
            buttonIzquierda.SetActive(false);
        }
        else
        {
            buttonDerecha.SetActive(true);
            buttonIzquierda.SetActive(true);
        }
        if(!buttonMap.activeSelf)
            buttonMap.SetActive(true);
    }
    public void NextConstruction()
    {
        if (indexConstructionZone < constructionZone.Count - 1)
        {
            indexConstructionZone++;
            cursorCamera.transform.position = waypoints[indexConstructionZone].transform.position;
            cursorCamera.transform.rotation = waypoints[indexConstructionZone].transform.rotation;
            buttonDerecha.SetActive(true);
            buttonIzquierda.SetActive(true);
        }
        CheckActivationButtons();
        
    }
    public void PrevConstruction()
    {
        if (indexConstructionZone > 0)
        {
            indexConstructionZone--;
            cursorCamera.transform.position = waypoints[indexConstructionZone].transform.position;
            cursorCamera.transform.rotation = waypoints[indexConstructionZone].transform.rotation;
            buttonDerecha.SetActive(true);
            buttonIzquierda.SetActive(true);
        }
        CheckActivationButtons();
    }
    public void CloseStageConstruction()
    {
        if (gm.InTutorial && OnClickPlayButton != null)
        {
            OnClickPlayButton(this);
        }
        gameObject.SetActive(false);
    }
    public GameObject GetCurrentZoneConstruction()
    {
        return currentZoneConstruction;
    }
    public void SetCurrentZoneConstruction(GameObject go)
    {
        currentZoneConstruction = go;
    }
    public int GetIndexConstructionZone()
    {
        return indexConstructionZone;
    }
}
