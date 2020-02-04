using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    private int countGenerateSoons;
    [SerializeField]
    private float delayGenerationSoons;
    [SerializeField]
    private int minCountGenerateSoons;
    [SerializeField]
    private int maxCountGenerateSoons;


    public GameObject generator;
    public bool generateSoons;
    public float rangeGenerateX;
    public float rangeGenerateZ;
    public Pool poolSpiderSoons;
    public float speedSoons;
    public float lifeSoons;
    public string soonsName;
    public float acelerationSoons;
    public float minDelayGenerateSoons; // Valor Recomendado: 5
    public float maxDelayGenerateSoons;// Valor Recomendado: 8
    protected override void Start()
    {
        delayGenerationSoons = Random.Range(minDelayGenerateSoons, maxDelayGenerateSoons);

        countGenerateSoons = Random.Range(minCountGenerateSoons, maxCountGenerateSoons);
        base.Start();
    }

    protected override void Update()
    {
        if (stateEnemy != StateEnemy.stune)
        {
            if (generateSoons)
            {
                CheckDelayGenerateSoons();
            }
        }
        base.Update();
        CheckState();
    }
    public void CheckDelayGenerateSoons()
    {
        if (delayGenerationSoons > 0)
        {
            delayGenerationSoons = delayGenerationSoons - Time.deltaTime;
        }
        else if(delayGenerationSoons <= 0)
        {
            GenerateSoons();
        }
    }
    public void GenerateSoons()
    {
        if (followRoute.GetFinishPoint() != followRoute.pathPoints[followRoute.pathPoints.Count - 1])
        {
            for (int i = 0; i < countGenerateSoons; i++)
            {
                float x = Random.Range(-rangeGenerateX, rangeGenerateX);
                float z = Random.Range(-rangeGenerateZ, rangeGenerateZ);
                GameObject go;

                go = poolSpiderSoons.GetObject();
                Enemy enemy = go.GetComponent<Enemy>();
                //Debug.Log(generatePosition);
                go.transform.position = transform.position + new Vector3(x, generator.transform.position.y, z);
                go.transform.rotation = transform.rotation;

#if UNITY_STANDALONE
                enemy.followRoute.GetAgent().speed = speedSoons;
                enemy.followRoute.GetAgent().acceleration = acelerationSoons;
#endif
#if UNITY_ANDROID
                enemy.followRoute.GetAgent().speed = speedSoons / 2;
                enemy.followRoute.GetAgent().acceleration = acelerationSoons / 2;
#endif
                enemy.life = lifeSoons;
                enemy.nameEnemy = soonsName;
                enemy.followRoute.FindGoDataRoute();
                enemy.followRoute.generatePath();
                if (enemy.followRoute.GetIndexRoute() < enemy.followRoute.pathPoints.Count - 1)
                {
                    enemy.followRoute.SetIndexRoute(followRoute.GetIndexRoute());
                    enemy.followRoute.SetFinishPoint(enemy.followRoute.pathPoints[followRoute.GetIndexRoute()]);
                }
            }
            delayGenerationSoons = Random.Range(minDelayGenerateSoons, maxDelayGenerateSoons);
            countGenerateSoons = Random.Range(minCountGenerateSoons, maxCountGenerateSoons);
        }
        else
        {
            generateSoons = false;
        }
    }
}
