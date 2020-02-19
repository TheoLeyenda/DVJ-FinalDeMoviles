using System.Collections;
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
        if (stateEnemy != StateEnemy.stune)
        {
            CheckFinishRouteEnemy();
            if (gameObject.activeSelf && animator.enabled)
            {
                CheckMeleAttack();
            }
        }
        CheckDie();
        CheckState();
        CheckInFire();
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
        if (life <= 0)
        {
            for (int i = 0; i < slimes.Length; i++)
            {
                slimes[i].followRoute.enabled = true;
                slimes[i].EnemyPrefab.transform.position = spawns[i].transform.position;
                slimes[i].followRoute.numRoute = followRoute.numRoute;
                slimes[i].followRoute.FindGoDataRoute();
                slimes[i].followRoute.generatePath();
                slimes[i].followRoute.SetIndexRoute(followRoute.GetIndexRoute() + 1);
                if (slimes[i].followRoute.pathPoints.Count > 0 && followRoute.GetIndexRoute() + 1 < slimes[i].followRoute.pathPoints.Count)
                {
                    slimes[i].followRoute.SetFinishPoint(slimes[i].followRoute.pathPoints[followRoute.GetIndexRoute() + 1]);
                }
                else
                {
                    slimes[i].followRoute.SetFinishPoint(followRoute.GetFinishPoint());
                }
                slimes[i].EnemyPrefab.transform.SetParent(null);
                slimes[i].EnemyPrefab.SetActive(true);

                /*if (OnGenerateSoonsSlimes != null)
                {
                    OnGenerateSoonsSlimes(this);
                }*/
            }
            //if (!iAmSoon)
            //{
                DieEnemy();
            //}
        }
        

    }
}
