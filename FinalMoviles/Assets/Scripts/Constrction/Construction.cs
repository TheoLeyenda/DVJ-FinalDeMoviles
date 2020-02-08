using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;
using UnityEngine.UI;
public class Construction : MonoBehaviour
{
    // Start is called before the first frame update
    public enum PivotMovement
    {
        None,
        X,
        Y,
        Z,
    }
    public enum PivotRotation
    {
        None,
        X,
        Y,
        Z,
    }
    public float life;
    public float maxLife;
    protected bool constructed;
    public CustomTeleporter myTeleporter;
    private TeleportController TC;
    [SerializeField]
    private FPSController player;
    public bool rotateStart;
    public PivotMovement pivotMovement;
    public PivotRotation pivotRotation;
    public float rotation;
    public List<MeshRenderer> meshRenderers;
    public List<Collider> colliders;
    public ParticleSystem particleSystemTeleport;
    public GameObject teleportPosition;
    // Esto sirve para acomodar el edificio en caso de que este no este en el piso cuando se genere
    [Header("Start Movement Construction")]
    public float valueDown;
    public float valueUp;
    private int indexConstruction; // este index va a ser igual al index del boton de construccion al que pertenece.
    private GameManager gm;
    private bool DestroyedConstruction;

    private void Start()
    {
        DestroyedConstruction = false;
        GameObject go = GameObject.Find("GamePrefab");
        TC = go.GetComponent<TeleportController>();
        gm = go.GetComponent<GameManager>();
        if (pivotRotation == PivotRotation.X)
        {
            transform.Rotate(Vector3.right, rotation);
        }
        else if (pivotRotation == PivotRotation.Y)
        {
            transform.Rotate(Vector3.up, rotation);
        }
        else if (pivotRotation == PivotRotation.Z)
        {
            transform.Rotate(Vector3.back, rotation);
        }
        player = gm.player;
    }
    public void SetConstructed(bool _constructed)
    {
        constructed = _constructed;
    }
    public bool GetConstructed()
    {
        return constructed;
    }
    public void UpMovement()
    {
        if (pivotMovement == PivotMovement.Y)
        {
            transform.position = transform.position + new Vector3(0, valueUp, 0);
        }
        else if (pivotMovement == PivotMovement.Z)
        {
            transform.position = transform.position + new Vector3(0, 0, valueUp);
        }
        else if (pivotMovement == PivotMovement.X)
        {
            transform.position = transform.position + new Vector3(valueUp, 0, 0);
        }
    }
    public void DownMovement()
    {
        if (pivotMovement == PivotMovement.Y)
        {
            transform.position = transform.position - new Vector3(0, valueDown, 0);
        }
        else if (pivotMovement == PivotMovement.Z)
        {
            transform.position = transform.position - new Vector3(0, 0, valueDown);
        }
        else if (pivotMovement == PivotMovement.X)
        {
            transform.position = transform.position - new Vector3(valueDown, 0, 0);
        }
    }
    private void Update()
    {
        if (TC != null)
        {
            if (!TC.buttonsTeleports[indexConstruction].go_imageLifeConstruction.activeSelf)
            {
                TC.buttonsTeleports[indexConstruction].go_imageLifeConstruction.SetActive(true);
            }
        }
        if (life <= 0)
        {
            DestroyConstruction();
        }

        if (life > maxLife)
        {
            life = maxLife;
        }

        if (TC != null)
        {
            TC.buttonsTeleports[indexConstruction].imageLifeConstruction.fillAmount = life / maxLife;
        }
        else
        {
            Debug.Log("TC is null");
        }
    }
    public void DestroyConstruction()
    {
        if (!DestroyedConstruction)
        {
            DestroyedConstruction = true;
            TC.buttonsTeleports[indexConstruction].disableButton = true;
            TC.buttonsTeleports[indexConstruction].CheckDisableButton();
            player.lockCursor = false;
            TC.camvasTeleport.SetActive(true);
            //player = null;
            //gameObject.SetActive(false);
            for (int i = 0; i < meshRenderers.Count; i++)
            {
                meshRenderers[i].enabled = false;
            }
            for (int i = 0; i < colliders.Count; i++)
            {
                colliders[i].enabled = false;
            }
            particleSystemTeleport.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<FPSController>();
        }
        if (other.tag == "RepairConstruction" && !DestroyedConstruction)
        {
            PowerUpController powerUpController = other.GetComponentInParent<PowerUpController>();
            if (powerUpController != null)
            {
                powerUpController.countRepairRecovered = (int)maxLife / 2;
                life = life + powerUpController.countRepairRecovered;
            }
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }*/
    public void SetIndexConstruction(int _indexConstruction)
    {
        indexConstruction = _indexConstruction;
    }
}
