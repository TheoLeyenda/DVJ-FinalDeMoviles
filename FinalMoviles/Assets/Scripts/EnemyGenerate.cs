using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    public enum NameEnemys
    {
        None,
        Slime_2,
        Rabbit,
        Ghost_White,
        Bat,
        TurtellShell,
        Slime,
        Spider,
        BoximonFiery,
        BoximonCyclopes,
        StoneMonster,
    }
    public enum TypeGenerator
    {
        None,
        Finite,
        Infinite,
    }
    private NameEnemys nameEnemys;
    private TypeGenerator auxTypeGenerator;
    public TypeGenerator typeGenerator;
    public float porcentageGrupInSurvivalMode;
    public int addEnemysForRound_InfiniteGenerated;
    public float rangeGenerationX;
    public float rangeGenerationZ;
    public float delayBetweenWaves;
    private float auxDelayBetweenWaves;
    public List<PoolsData> listPools;
    public List<Wave> waves;
    public float minDelaySpawn = 0.2f;
    public float maxDelaySpawn = 1.5f;
    private Pool currentPool;
    private bool finishWaves;
    //[SerializeField]
    private int indexWave;
    private int enemysDie;
    private float delayGeneratorInfinite;
    public int countEnemysRount_InfiniteGenered;
    private int EnemysRount_InfiniteGenered;
    private bool infinite;
    public bool DisableGenerator;
    public int numRoute;
    //private bool swarm;//Enjambre (boleano que controla si los enemigos a salir salen en enjambre o no)

    private void Start()
    {
        if (addEnemysForRound_InfiniteGenerated <= 0)
        {
            addEnemysForRound_InfiniteGenerated = 5;
        }
        if (typeGenerator == TypeGenerator.Infinite)
        {
            infinite = true;
        }
        else
        {
            infinite = false;
        }
        if (countEnemysRount_InfiniteGenered <= 0)
        {
            countEnemysRount_InfiniteGenered = 25;
        }
        delayGeneratorInfinite = 0;
        auxTypeGenerator = typeGenerator;
        waves.Add(new Wave());
        indexWave = 0;
        auxDelayBetweenWaves = delayBetweenWaves;
        for (int i = 0; i < waves.Count - 1; i++)
        {
            for (int k = 0; k < waves[i].dataCountEnemySpawns.Length; k++)
            {
                waves[i].countTotalEnemysWave = waves[i].countTotalEnemysWave + waves[i].dataCountEnemySpawns[k].countEnemysSpawn;
            }
            waves[i].delayGenerationEnemys = new float[waves[i].countTotalEnemysWave + 1];
            for (int j = 0; j < waves[i].delayGenerationEnemys.Length; j++)
            {
                if (j > 0)
                {
                    waves[i].delayGenerationEnemys[j] = Random.Range(minDelaySpawn, maxDelaySpawn);
                }
                else
                {
                    waves[i].delayGenerationEnemys[j] = 0;
                }
            }
            waves[i].indexDelayGenerationSpawn = 0;
            waves[i].indexDataCountEnemySpawns = 0;
            waves[i].currentEnemysGenerate = 0;
            
        }
        if (DisableGenerator)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        Enemy.OnDieAction += AddEnemysDie;
        Slime.OnGenerateSoonsSlimes += AddCountTotalEnemysWave;
    }
    private void OnDisable()
    {
        Enemy.OnDieAction -= AddEnemysDie;
        Slime.OnGenerateSoonsSlimes -= AddCountTotalEnemysWave;
    }
    public void AddCountTotalEnemysWave(Slime s)
    {
        waves[indexWave].countTotalEnemysWave++;
        waves[indexWave].currentEnemysGenerate++;
    }
    public void AddEnemysDie(Enemy e)
    {
        if (e.nameEnemy != "MiniSpider")
        {
            enemysDie++;
            //Debug.Log("Enemigos muertos: "+ enemysDie);
        }
    }
    [System.Serializable]
    public class PoolsData
    {
        public Pool pool;
        public string nameObjectPool;
        public float objectHeight; // altura que tiene el objeto en el escenario
    }
    [System.Serializable]
    public struct DataCountEnemySpawn
    {
        public int countEnemysSpawn;
        public NameEnemys enemysGenerate;
        public bool swarn;
    }
    [System.Serializable]
    public class Wave
    {
        [HideInInspector]
        public int indexDelayGenerationSpawn;
        [HideInInspector]
        public int indexDataCountEnemySpawns;
        [HideInInspector]
        public int currentEnemysGenerate;
        //SI ALGO FALLA DESCOMENTAR EL [HideInInspector] de abajo y comparar el countTotalEnemyWave con la
        //suma de todos los countEnemysSpawn del DataCountEnemySpawn en cuestion.
        [HideInInspector]
        public int countTotalEnemysWave;
        [HideInInspector]
        public float[] delayGenerationEnemys;
        public DataCountEnemySpawn[] dataCountEnemySpawns;
        
    }
    public void CheckGenerate()
    {
        if (typeGenerator == TypeGenerator.Finite)
        {

            if (delayBetweenWaves <= 0)
            {
                if (indexWave < waves.Count)
                {
                    //Debug.Log(waves[indexWave].delayGenerationEnemys.Length);
                    //Debug.Log("indexDelayGenerationSpawn: "+ waves[indexWave].indexDelayGenerationSpawn);

                    float currentDelay = waves[indexWave].delayGenerationEnemys[waves[indexWave].indexDelayGenerationSpawn];
                    GameObject go = null;
                    float Height = 0;
                    for (int i = 0; i < listPools.Count; i++)
                    {
                        //Debug.Log("dataCountEnemySpawns: "+waves[indexWave].dataCountEnemySpawns.Length);
                        //Debug.Log("indexDataCountEnemySpawns: " + waves[indexWave].indexDataCountEnemySpawns);
                        if (listPools[i].nameObjectPool == waves[indexWave].dataCountEnemySpawns[waves[indexWave].indexDataCountEnemySpawns].enemysGenerate.ToString())
                        {
                            currentPool = listPools[i].pool;
                            Height = listPools[i].objectHeight;
                        }
                    }
                    //Debug.Log("currentDelay:" + currentDelay);
                    if (currentDelay <= 0)
                    {
                        if (waves[indexWave].dataCountEnemySpawns[waves[indexWave].indexDataCountEnemySpawns].swarn)
                        {

                            //Genero al enemigo de forma grupal
                            if (currentPool != null)
                            {
                                for (int i = 0; i < waves[indexWave].dataCountEnemySpawns[waves[indexWave].indexDataCountEnemySpawns].countEnemysSpawn; i++)
                                {
                                    go = currentPool.GetObject();

                                    //APARECE TODOS EN EL MISMA POSICION
                                    //go.transform.position = new Vector3(transform.position.x, Height, transform.position.z);

                                    //APARECEN EN X o Z ALEATORIO
                                    go.transform.position = new Vector3(transform.position.x + (Random.Range(-rangeGenerationX, rangeGenerationX)), Height, transform.position.z + (Random.Range(-rangeGenerationZ, rangeGenerationZ)));
                                    go.transform.rotation = transform.rotation;
                                    FollowRoute followRoute = go.GetComponent<FollowRoute>();
                                    followRoute.numRoute = numRoute;
                                    followRoute.FindGoDataRoute();
                                    waves[indexWave].currentEnemysGenerate++;
                                    Enemy enemy = go.GetComponent<Enemy>();
                                    if(enemy != null)
                                    {
                                        if (enemy.nameEnemy == "Spider")
                                        {
                                            Spider spider = enemy.GetComponent<Spider>();
                                            spider.generateSoons = false;
                                            spider.poolSpiderSoons.enabled = false;
                                        }
                                    }
                                }
                                waves[indexWave].indexDataCountEnemySpawns++;
                                waves[indexWave].indexDelayGenerationSpawn++;
                            }
                        }
                        else
                        {
                            //Genero al enemigo de forma individual.
                            if (currentPool != null)
                            {
                                go = currentPool.GetObject();
                                go.transform.rotation = transform.rotation;
                                go.transform.position = new Vector3(transform.position.x + (Random.Range(-rangeGenerationX, rangeGenerationX)), Height, transform.position.z + (Random.Range(-rangeGenerationZ, rangeGenerationZ)));
                                FollowRoute followRoute = go.GetComponent<FollowRoute>();
                                followRoute.numRoute = numRoute;
                                followRoute.FindGoDataRoute();
                                waves[indexWave].currentEnemysGenerate++;
                                waves[indexWave].indexDataCountEnemySpawns++;
                                waves[indexWave].indexDelayGenerationSpawn++;
                                Enemy enemy = go.GetComponent<Enemy>();
                                if (enemy != null)
                                {
                                    if (enemy.nameEnemy == "Spider")
                                    {
                                        Spider spider = enemy.GetComponent<Spider>();
                                        spider.generateSoons = true;
                                        spider.poolSpiderSoons.enabled = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        currentDelay -= Time.deltaTime;
                        waves[indexWave].delayGenerationEnemys[waves[indexWave].indexDelayGenerationSpawn] = currentDelay;
                    }

                }

            }
            else
            {
                delayBetweenWaves = delayBetweenWaves - Time.deltaTime;
            }
        }
        else if (typeGenerator == TypeGenerator.Infinite)
        {
            GameObject go;
            int iter = 0;
            if (delayBetweenWaves <= 0)
            {
                if (EnemysRount_InfiniteGenered < countEnemysRount_InfiniteGenered)
                {
                    if (delayGeneratorInfinite <= 0)
                    {
                        float Height = 0;
                        int maxEnemyGrup = 10;
                        int minEnemyGrup = 3;
                        int porcentageGrupGenetation = Random.Range(0, 101);
                        int enemySelected = Random.Range(0, listPools.Count);
                        int countEnemyGrup = Random.Range(minEnemyGrup, maxEnemyGrup + 1);

                        if (porcentageGrupGenetation > porcentageGrupInSurvivalMode)
                        {
                            while (iter < countEnemyGrup && EnemysRount_InfiniteGenered < countEnemysRount_InfiniteGenered)
                            {
                                Height = listPools[enemySelected].objectHeight;
                                go = listPools[enemySelected].pool.GetObject();
                                go.transform.position = new Vector3(transform.position.x + (Random.Range(-rangeGenerationX, rangeGenerationX)), Height, transform.position.z + (Random.Range(-rangeGenerationZ, rangeGenerationZ)));
                                go.transform.rotation = transform.rotation;
                                FollowRoute followRoute = go.GetComponent<FollowRoute>();
                                followRoute.numRoute = numRoute;
                                followRoute.FindGoDataRoute();
                                EnemysRount_InfiniteGenered++;
                                iter++;
                                Enemy enemy = go.GetComponent<Enemy>();
                                if (enemy != null)
                                {
                                    if (enemy.nameEnemy == "Spider")
                                    {
                                        Spider spider = enemy.GetComponent<Spider>();
                                        spider.generateSoons = false;
                                        spider.poolSpiderSoons.enabled = false;
                                    }
                                }
                            }
                            delayGeneratorInfinite = Random.Range(minDelaySpawn, maxDelaySpawn);
                        }
                        else if (porcentageGrupGenetation <= porcentageGrupInSurvivalMode)
                        {
                            if (EnemysRount_InfiniteGenered < countEnemysRount_InfiniteGenered)
                            {
                                Height = listPools[enemySelected].objectHeight;
                                go = listPools[enemySelected].pool.GetObject();
                                go.transform.position = new Vector3(transform.position.x + (Random.Range(-rangeGenerationX, rangeGenerationX)), Height, transform.position.z + (Random.Range(-rangeGenerationZ, rangeGenerationZ)));
                                go.transform.rotation = transform.rotation;
                                FollowRoute followRoute = go.GetComponent<FollowRoute>();
                                followRoute.numRoute = numRoute;
                                followRoute.FindGoDataRoute();
                                delayGeneratorInfinite = Random.Range(minDelaySpawn, maxDelaySpawn);
                                EnemysRount_InfiniteGenered++;
                                Enemy enemy = go.GetComponent<Enemy>();
                                if (enemy != null)
                                {
                                    if (enemy.nameEnemy == "Spider")
                                    {
                                        Spider spider = enemy.GetComponent<Spider>();
                                        spider.generateSoons = true;
                                        spider.poolSpiderSoons.enabled = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        delayGeneratorInfinite = delayGeneratorInfinite - Time.deltaTime;
                    }
                }
            }
            else
            {
                delayBetweenWaves = delayBetweenWaves - Time.deltaTime;
            }
        }
            
    }
    public void CheckNextWave(TypeGenerator curremtTypeGenerator)
    {
        if (!infinite)
        {
            if (indexWave < waves.Count)
            {
                if (waves[indexWave].currentEnemysGenerate >= waves[indexWave].countTotalEnemysWave &&
                    (enemysDie >= waves[indexWave].countTotalEnemysWave))
                {
                    enemysDie = 0;
                    indexWave++;
                    delayBetweenWaves = auxDelayBetweenWaves;
                    currentPool = null;
                    typeGenerator = curremtTypeGenerator;
                    //Debug.Log("indexWave:"+ indexWave);
                }
                else if (indexWave >= waves.Count - 1  ||
                    (waves[indexWave].currentEnemysGenerate >= waves[indexWave].countTotalEnemysWave
                    && (enemysDie < waves[indexWave].countTotalEnemysWave)))
                {
                    typeGenerator = TypeGenerator.None;
                }
            }
            else
            {
                typeGenerator = TypeGenerator.None;
            }
        }
        else if (infinite)
        {
            if (enemysDie >= countEnemysRount_InfiniteGenered)
            {
                indexWave++;
                delayBetweenWaves = auxDelayBetweenWaves;
                typeGenerator = curremtTypeGenerator;
                enemysDie = 0;
                EnemysRount_InfiniteGenered = 0;
                if (countEnemysRount_InfiniteGenered + addEnemysForRound_InfiniteGenerated < listPools[0].pool.count && countEnemysRount_InfiniteGenered < listPools[0].pool.count)
                {
                    countEnemysRount_InfiniteGenered = countEnemysRount_InfiniteGenered + addEnemysForRound_InfiniteGenerated;
                }
                //Debug.Log(indexWave);
            }
            else if(EnemysRount_InfiniteGenered >= countEnemysRount_InfiniteGenered)
            {
                typeGenerator = TypeGenerator.None;
            }
            
        }

    }
    // Update is called once per frame
    void Update()
    {
        CheckGenerate();
        CheckNextWave(auxTypeGenerator);
    }
}
