﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Slime : Enemy
{
    // Start is called before the first frame update
    public GameObject[] spawns;
    public Enemy[] slimes;
    //public float delayDisable = 0.01f;
    public GameObject parent;

    public static event Action<Slime> OnGenerateSoonsSlimes;

    protected override void Start()
    {
        base.Start();
    }
    private void OnEnable()
    {
        //delayDisable = 0.01f;
        for (int i = 0; i < slimes.Length; i++)
        {
            slimes[i].EnemyPrefab.transform.position = transform.position;
            slimes[i].EnemyPrefab.transform.parent = parent.transform;
            slimes[i].followRoute.enabled = false;
        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        CheckFinishRouteEnemy();
        CheckDie();
        if (gameObject.activeSelf && animator.enabled)
        {
            CheckMeleAttack();
        }
    }
    public void CheckDie()
    {
        bool allSlimeActivate = true;
        for (int i = 0; i < slimes.Length; i++)
        {
            if (!slimes[i].EnemyPrefab.activeSelf)
            {
                allSlimeActivate = false;
            }
        }
        if (allSlimeActivate)
        {
            CheckDieEnemy();
        }
        if (spawns.Length == slimes.Length && life <= 0)
        {
            for (int i = 0; i < slimes.Length; i++)
            {
                slimes[i].EnemyPrefab.transform.position = spawns[i].transform.position;
                slimes[i].followRoute.FindGoDataRoute();
                slimes[i].followRoute.generatePath();
                slimes[i].followRoute.SetIndexRoute(followRoute.GetIndexRoute());
                if (slimes[i].followRoute.pathPoints.Count > 0)
                {
                    slimes[i].followRoute.SetFinishPoint(slimes[i].followRoute.pathPoints[followRoute.GetIndexRoute()]);
                }
                else
                {
                    slimes[i].followRoute.SetFinishPoint(followRoute.GetFinishPoint());
                }
                slimes[i].followRoute.enabled = true;
                slimes[i].EnemyPrefab.transform.SetParent(null);
                slimes[i].EnemyPrefab.SetActive(true);
                if (OnGenerateSoonsSlimes != null)
                {
                    OnGenerateSoonsSlimes(this);
                }
            }
        }
        

    }
}
