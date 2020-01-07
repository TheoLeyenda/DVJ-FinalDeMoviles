using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionManager : MonoBehaviour
{ 
    // Start is called before the first frame update
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
    public GameObject camvasContruction;
    private int indexConstructionZone;
    private Vector3 finishPositionCamera;
    private float magnitudeFinishMovementCamera = 0.1f;
    private bool inGeneralVision = false;
    private GameObject currentZoneConstruction;

    //public Camera cameraInConstruction;
    private void OnEnable()
    {
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
    public void CheckClickInConstructionZone()
    {
        //Debug.Log(Camera.main);
        if (Camera.main != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, rangeOfRayCast) && Input.GetButton("InputSeleccion") && !camvasContruction.activeSelf)
            {
                if (hit.transform != null)
                {
                    if (hit.transform.tag == "ZonaConstruccion")
                    {
                        camvasContruction.SetActive(true);
                        currentZoneConstruction = hit.transform.gameObject;
                        buttonDerecha.SetActive(false);
                        buttonIzquierda.SetActive(false);
                    }
                }
            }
        }
    }
    public void VistaGeneral()
    {
        inGeneralVision = !inGeneralVision;
        if (inGeneralVision)
        {
            cursorCamera.transform.position = waypointGeneralVision.transform.position;
            cursorCamera.transform.rotation = waypointGeneralVision.transform.rotation;
            buttonDerecha.SetActive(false);
            buttonIzquierda.SetActive(false);
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
