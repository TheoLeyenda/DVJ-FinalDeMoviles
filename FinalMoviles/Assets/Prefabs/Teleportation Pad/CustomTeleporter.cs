using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkTreeFPS;
using System;
public class CustomTeleporter : MonoBehaviour
{
    public ParticleSystem PS;
    [HideInInspector]
    public FPSController fpsAndroid;
    [HideInInspector]
    public FPSController fpsPC;
    public GameObject camvasTeleport;
	public bool instantTeleport;
	public bool randomTeleport;
	public bool buttonTeleport;
	public string buttonName;
	public bool delayedTeleport;
	public float teleportTime = 3;
	public string objectTag = "if empty, any object will tp";
	public List<Transform> destinationPad;
	public float teleportationHeightOffset = 1;
	private float curTeleportTime;
	private bool inside;
	[HideInInspector]
	public bool arrived;
	private Transform subject;
	public AudioSource teleportSound;
	public AudioSource teleportPadSound;
	public bool teleportPadOn = true;
    public bool teleportOccupet;
    public float DelayCamvasActivate = 1f;
    private float auxDelayCamvasActivate = 1f;
    public static event Action<CustomTeleporter> OnTriggerWhitMe;

    void Start ()
	{
		curTeleportTime = teleportTime;
	}


    /*void Update ()
	{
		if(inside)
		{
			if(!arrived && teleportPadOn)
			Teleport();
		}
	}*/
    private void Update()
    {
        if (teleportPadOn)
        {
            PS.enableEmission = true;
            teleportPadSound.volume = 0.5f;

        }
        else
        {
            PS.enableEmission = false;
            teleportPadSound.volume = 0;
            PS.Clear();
        }
        //Debug.Log(delayNotActivateCamvas);
        //Debug.Log(ActivateCamvas);

    }

    public void Teleport()
	{
        if (teleportPadOn)
        {
            if (instantTeleport)
            {
                if (randomTeleport)
                {
                    int chosenPad = UnityEngine.Random.Range(0, destinationPad.Count);
                    destinationPad[chosenPad].GetComponent<CustomTeleporter>().arrived = true;
                    subject.transform.position = destinationPad[chosenPad].transform.position + new Vector3(0, teleportationHeightOffset + 1, 0);
                    teleportSound.Play();
                    camvasTeleport.SetActive(false);
                }
                else
                {
                    if (destinationPad[0] != null)
                    {
                        destinationPad[0].GetComponent<CustomTeleporter>().arrived = true;
                        subject.transform.position = destinationPad[0].transform.position + new Vector3(0, teleportationHeightOffset + 1, 0);
                        teleportSound.Play();
                        camvasTeleport.SetActive(false);
                        DelayCamvasActivate = auxDelayCamvasActivate;
                    }
                }
            }
            else if (delayedTeleport)
            {
                curTeleportTime -= 1 * Time.deltaTime;
                if (curTeleportTime <= 0)
                {
                    curTeleportTime = teleportTime;
                    if (randomTeleport)
                    {
                        int chosenPad = UnityEngine.Random.Range(0, destinationPad.Count);
                        destinationPad[chosenPad].GetComponent<CustomTeleporter>().arrived = true;
                        subject.transform.position = destinationPad[chosenPad].transform.position + new Vector3(0, teleportationHeightOffset + 1, 0);
                        teleportSound.Play();
                        camvasTeleport.SetActive(false);
                    }
                    else
                    {
                        if (destinationPad[0] != null)
                        {
                            destinationPad[0].GetComponent<CustomTeleporter>().arrived = true;
                            subject.transform.position = destinationPad[0].transform.position + new Vector3(0, teleportationHeightOffset + 1, 0);
                            teleportSound.Play();
                            camvasTeleport.SetActive(false);
                        }
                    }
                }
            }
            else if (buttonTeleport)
            {
                if (Input.GetButtonDown(buttonName))
                {
                    if (delayedTeleport)
                    {
                        curTeleportTime -= 1 * Time.deltaTime;
                        if (curTeleportTime <= 0)
                        {
                            curTeleportTime = teleportTime;
                            if (randomTeleport)
                            {
                                int chosenPad = UnityEngine.Random.Range(0, destinationPad.Count);
                                destinationPad[chosenPad].GetComponent<CustomTeleporter>().arrived = true;
                                subject.transform.position = destinationPad[chosenPad].transform.position + new Vector3(0, teleportationHeightOffset + 1, 0);
                                teleportSound.Play();
                                camvasTeleport.SetActive(false);
                            }
                            else
                            {
                                if (destinationPad[0] != null)
                                {
                                    destinationPad[0].GetComponent<CustomTeleporter>().arrived = true;
                                    subject.transform.position = destinationPad[0].transform.position + new Vector3(0, teleportationHeightOffset + 1, 0);
                                    teleportSound.Play();
                                    camvasTeleport.SetActive(false);
                                }
                            }
                        }
                    }
                    else if (randomTeleport)
                    {
                        int chosenPad = UnityEngine.Random.Range(0, destinationPad.Count);
                        destinationPad[chosenPad].GetComponent<CustomTeleporter>().arrived = true;
                        subject.transform.position = destinationPad[chosenPad].transform.position + new Vector3(0, teleportationHeightOffset + 1, 0);
                        teleportSound.Play();
                        camvasTeleport.SetActive(false);
                    }
                    else
                    {
                        if (destinationPad[0] != null)
                        {
                            destinationPad[0].GetComponent<CustomTeleporter>().arrived = true;
                            subject.transform.position = destinationPad[0].transform.position + new Vector3(0, teleportationHeightOffset + 1, 0);
                            teleportSound.Play();
                            camvasTeleport.SetActive(false);
                        }
                    }
                }
            }
        }
	}

	void OnTriggerStay(Collider trig)
	{
		if(objectTag != "")
		{
			if(trig.gameObject.tag == objectTag && teleportPadOn)
			{
                if (DelayCamvasActivate <= 0)
                {
                    camvasTeleport.SetActive(true);
#if !UNITY_ANDROID
                    fpsPC.lockCursor = false;
#endif
                }
                else
                {
                    DelayCamvasActivate = DelayCamvasActivate - Time.deltaTime;
                }
                

                subject = trig.transform;
				inside = true;
                teleportOccupet = true;
				if(buttonTeleport)
				{
					arrived = false;
				}
                if (OnTriggerWhitMe != null)
                {
                    OnTriggerWhitMe(this);
                }
			}
		}
		else
		{
			subject = trig.transform;
			inside = true;
			if(buttonTeleport)
			{
				arrived = false;
			}
		}
	}

	void OnTriggerExit(Collider trig)
	{
		if(objectTag != "")
		{
			if(trig.gameObject.tag == objectTag && teleportPadOn)
			{
                DelayCamvasActivate = auxDelayCamvasActivate;
				inside = false;
#if !UNITY_ANDROID
                fpsPC.lockCursor = true;
#endif
                curTeleportTime = teleportTime;
                teleportOccupet = false;
                camvasTeleport.SetActive(false);
                if (trig.transform == subject)
				{
					arrived = false;
				}
				subject = null;
			}
		}
		else
		{
			inside = false;
			curTeleportTime = teleportTime;
			if(trig.transform == subject)
			{
				arrived = false;
			}
			subject = null;
		}
	}
}
