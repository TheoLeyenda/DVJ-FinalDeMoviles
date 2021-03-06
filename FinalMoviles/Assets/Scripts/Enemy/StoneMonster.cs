﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMonster : Enemy
{
    // Start is called before the first frame update
    public GameObject generator;
    public int DamageProjectileConstructions;
    public float powerShoot;
    private bool constructionInRange;
    public float delayAttack;
    public float auxDelayAttack;
    public Pool poolProyectile;
    [SerializeField]
    private Wall target;
    private float timerStartAttackConstruction;
    private bool inRangeAttack;
    public float minRangeDelay;
    public float maxRangeDelay;
    protected override void Start()
    {
        base.Start();
        constructionInRange = false;
        timerStartAttackConstruction = Random.Range(minRangeDelay, maxRangeDelay);
        //Debug.Log(timerStartAttackConstruction);
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        if (stateEnemy != StateEnemy.stune)
        {
            CheckFinishRouteEnemy();
            CheckInRageAttack();
        }
        CheckAnimations();
        CheckState();
    }
    public void CheckInRageAttack()
    {
        if (inRangeAttack)
        {
            if (followRoute.GetAgent().speed > 0)
            {
                timerStartAttackConstruction = timerStartAttackConstruction - Time.deltaTime;
            }
            if (timerStartAttackConstruction <= 0 && followRoute.GetAgent().speed > 0)
            {
                timerStartAttackConstruction = Random.Range(minRangeDelay, maxRangeDelay);
                followRoute.GetAgent().speed = 0;
            }
            if (target != null && followRoute.GetAgent().speed <= 0)
            {
                transform.LookAt(new Vector3(target.gameObject.transform.position.x, transform.position.y, target.gameObject.transform.position.z));
                if (target.construction.life > 0)
                {
                    CheckDelayAttack();
                }
                if (target.construction.life <= 0)
                {
                    followRoute.GetAgent().speed = auxSpeed;
                    target = null;
                    inRangeAttack = false;
                }
            }
        }
    }
    private void OnDisable()
    {
        inRangeAttack = false;
        timerStartAttackConstruction = Random.Range(minRangeDelay, maxRangeDelay);
    }
    public void CheckDelayAttack()
    {
        if (delayAttack > 0)
        {
            delayAttack = delayAttack - Time.deltaTime; 
        }
        else if (delayAttack <= 0)
        {
            animator.Play("Attack");
            Attack();//Cambiar esto por la animacion de ataque cuando esten todas las animaciones.
            delayAttack = auxDelayAttack;
        } 
    }
    public override void Attack()
    {
        if (target != null)
        {
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            GameObject go = poolProyectile.GetObject();
            FireBall fireBall = go.GetComponentInChildren<FireBall>();
            fireBall.shooter = this;
            go.transform.rotation = generator.transform.rotation;
            go.transform.position = generator.transform.position;
            go.GetComponent<Rigidbody>().AddForce(transform.forward * powerShoot, ForceMode.Impulse);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Construccion")
        {
            //followRoute.GetAgent().speed = 0;
            inRangeAttack = true;
            target = other.gameObject.GetComponent<Wall>();
        }
    }

}
