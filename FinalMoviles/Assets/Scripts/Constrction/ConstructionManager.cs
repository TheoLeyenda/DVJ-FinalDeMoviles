using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> objectsDisables;
    public List<GameObject> objectsActivate;
    public List<GameObject> constructionZone;
    private int substractVec_xCamera = 20;
    private int indexConstructionZone;
    public Camera cameraInConstruction;
    private void OnEnable()
    {
        indexConstructionZone = 0;
        SetActiveObjects(false,objectsDisables);
        SetActiveObjects(true, objectsActivate);
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
    }
    private void Update()
    {
        
    }
    public void CheckClickInConstructionZone()
    {
        RaycastHit raycastHit;
        /*if (Physics.Raycast())
        {

        }*/
    }
    public void NextConstruction()
    {

    }
    public void PrevConstruction()
    {

    }
}
