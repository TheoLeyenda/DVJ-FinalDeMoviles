using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTreeFPS;

public class Demon : Enemy
{
    // Start is called before the first frame update
    private bool enableAttack;
    //private bool enableHealing;
    private PlayerStats TargetPlayer;
    //public float delayMagicCuration;
    public float delayStartAttack;
    public float delayAttack;
    public float auxDelayAttack;
    //[Header("Rangos del delay de curacion")]
    //public float minDelayMagicCuration;
    //public float maxDelayMagicCuration;
    public Pool poolLance;
    //public SphereHealing sphereHealing;
    public bool attackConstruction;
    public GameObject generatorLance;
    [Header("Rango del delay del comienzo de ataque")]
    public float minDelayStartAttack;
    public float maxDelayStartAttack;
    protected override void Start()
    {
        base.Start();
        enableAttack = false;
        //enableHealing = false;
        //delayMagicCuration = Random.Range(minDelayMagicCuration, maxDelayMagicCuration);
        delayStartAttack = Random.Range(minDelayStartAttack, maxDelayStartAttack);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckAttack();
    }
    /*public void CheckCuration()
    {
        if (enableHealing)
        {
            if (delayMagicCuration > 0)
            {
                delayMagicCuration = delayMagicCuration - Time.deltaTime;
            }
            else if (delayMagicCuration <= 0)
            {
                MagicCuration();
                delayMagicCuration = Random.Range(minDelayMagicCuration, maxDelayMagicCuration);
            }
        }
    }*/
    private void OnDisable()
    {
        enableAttack = false;
        delayStartAttack = Random.Range(minDelayStartAttack, maxDelayStartAttack);
    }
    public void CheckAttack()
    {
        if (enableAttack)
        {
            if (followRoute.GetAgent().speed > 0)
            {
                delayStartAttack = delayStartAttack - Time.deltaTime;
            }
            if (delayStartAttack <= 0 && followRoute.GetAgent().speed > 0)
            {
                delayStartAttack = Random.Range(minDelayStartAttack, maxDelayStartAttack);
                followRoute.GetAgent().speed = 0;
            }
            //Debug.Log(TargetPlayer);
            //Debug.Log(enableAttack);
            if (TargetPlayer != null && followRoute.GetAgent().speed <= 0)
            {
                transform.LookAt(new Vector3(TargetPlayer.HeadPlayer.transform.position.x, transform.position.y, TargetPlayer.HeadPlayer.transform.position.z));
                generatorLance.transform.LookAt(TargetPlayer.HeadPlayer.transform.position);
                if (TargetPlayer.health > 0)
                {
                    //Debug.Log("ENTRE");
                    CheckDelayAttack();
                }
                if (TargetPlayer.health <= 0)
                {
                    followRoute.GetAgent().speed = auxSpeed;
                    TargetPlayer = null;
                    enableAttack = false;
                }
            }
            else
            {
                followRoute.GetAgent().speed = auxSpeed;
            }
            
        }
    }
    public void CheckDelayAttack()
    {
        if (delayAttack > 0)
        {
            delayAttack = delayAttack - Time.deltaTime;
        }
        else
        {
            Attack();
        }
    }
    public override void Attack()
    {
        GameObject go = poolLance.GetObject();
        Lance lance = go.GetComponent<Lance>();

        go.transform.position = generatorLance.transform.position;
        go.transform.rotation = generatorLance.transform.rotation;

        lance.rig.AddForce(lance.transform.forward * lance.initialVelocity, ForceMode.Impulse);
        delayAttack = auxDelayAttack;
    }
    /*public void MagicCuration()
    {
        sphereHealing.gameObject.SetActive(true);
    }*/
    /*public void SetEnableHealing(bool _enableHealing)
    {
        enableHealing = _enableHealing;
    }*/
    /*public bool GetEnableHealing()
    {
        return enableHealing;
    }*/
    public void SetEnableAttack(bool _enableAttack)
    {
        enableAttack = _enableAttack;
    }
    public bool GetEnableAttack()
    {
        return enableAttack;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            TargetPlayer = player;
            enableAttack = true;
            //Debug.Log("ENTRE");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!attackConstruction)
            {
                enableAttack = false;
                followRoute.GetAgent().speed = auxSpeed;
            }
        }
        if (attackConstruction)
        {
            if (other.tag == "Construccion")
            {
                enableAttack = false;
                followRoute.GetAgent().speed = auxSpeed;
                
            }
        }
    }
}
