using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<DetectedTeleport> detectedTeleports;
    [SerializeField]
    private List<CustomTeleporter> customTeleporters;
    public CustomTeleporter currentCustomTeleporter;

    // ESTA FUNCION VA A SER LLAMADA POR EL BOTON DE JUGAR DE LA FASE DE CONSTRUCCION.
    private void OnEnable()
    {
        CustomTeleporter.OnTriggerWhitMe += EventOnTriggerWhitMe;
    }
    private void OnDisable()
    {
        CustomTeleporter.OnTriggerWhitMe -= EventOnTriggerWhitMe;
    }
    private void Start()
    {
        customTeleporters = new List<CustomTeleporter>();
    }
    public void EventOnTriggerWhitMe(CustomTeleporter ct)
    {
        SetCurrentCustomTeleport();
    }
    public void SetTeleports()
    {
        
        for (int i = 0; i < detectedTeleports.Count; i++)
        {
            if (detectedTeleports[i].CT != null)
            {
                detectedTeleports[i].CT.teleportPadOn = true;
                customTeleporters.Insert(detectedTeleports[i].indexTeleport, detectedTeleports[i].GetCustomTeleport());
            }
        }
        SetCurrentCustomTeleport();
    }
    // ESTA FUNCION SERA LLAMADA POR EL BOTON DE TELEPORT ANTES QUE LA FUNCION Teleport(int indexDestination)
    public void SetCurrentCustomTeleport()
    {
        for (int i = 0; i < customTeleporters.Count; i++)
        {
            if (customTeleporters[i].teleportOccupet)
            {
                currentCustomTeleporter = customTeleporters[i];
            }
        }
        if(currentCustomTeleporter != null)
        {
            currentCustomTeleporter.destinationPad.Clear();
        }
    }

    // ESTA FUNCION SERA LLAMADA POR EL BOTON DE TELEPORT
    public void Teleport(int indexDestination)
    {
        currentCustomTeleporter.destinationPad.Add(detectedTeleports[indexDestination].GetCustomTeleport().transform);
        currentCustomTeleporter.Teleport();
        currentCustomTeleporter.camvasTeleport.SetActive(false);
    }
}
