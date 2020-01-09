using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    public enum NameEnemys
    {
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
    public TypeGenerator typeGenerator;
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
    private int indexWave;
    //private bool swarm;//Enjambre (boleano que controla si los enemigos a salir salen en enjambre o no)

    private void Start()
    {
        indexWave = 0;
        auxDelayBetweenWaves = delayBetweenWaves;
        for (int i = 0; i < waves.Count; i++)
        {    
            waves[i].delayGenerationEnemys = new float[waves[i].countTotalEnemysWave];
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
        public int countTotalEnemysWave;
        [HideInInspector]
        public float[] delayGenerationEnemys;
        public DataCountEnemySpawn[] dataCountEnemySpawns;
        
    }
    public void CheckGenerate()
    {
        if (typeGenerator == TypeGenerator.Finite)
        {
            if (indexWave < waves.Count)
            {
                if (delayBetweenWaves <= 0)
                {
                    float currentDelay = waves[indexWave].delayGenerationEnemys[waves[indexWave].indexDelayGenerationSpawn];
                    GameObject go = null;
                    float Height = 0;
                    for (int i = 0; i < listPools.Count; i++)
                    {
                        if (listPools[i].nameObjectPool == waves[indexWave].dataCountEnemySpawns[waves[indexWave].indexDataCountEnemySpawns].ToString())
                        {
                            currentPool = listPools[i].pool;
                            Height = listPools[i].objectHeight;
                        }
                    }
                    if (currentDelay <= 0)
                    {
                        if (waves[indexWave].dataCountEnemySpawns[waves[indexWave].indexDataCountEnemySpawns].swarn)
                        {

                            //Genero al enemigo de forma individual
                            if (currentPool != null)
                            {
                                for (int i = 0; i < waves[indexWave].dataCountEnemySpawns[waves[indexWave].indexDataCountEnemySpawns].countEnemysSpawn; i++)
                                {
                                    go = currentPool.GetObject();

                                    //APARECE TODOS EN EL MISMA POSICION
                                    //go.transform.position = new Vector3(transform.position.x, Height, transform.position.z);

                                    //APARECEN EN X o Z ALEATORIO
                                    go.transform.position = new Vector3(transform.position.x + (Random.Range(-rangeGenerationX, rangeGenerationX)), Height, transform.position.z + (Random.Range(-rangeGenerationZ, rangeGenerationZ)));

                                    go = currentPool.GetObject();
                                    waves[indexWave].currentEnemysGenerate++;
                                }
                                waves[indexWave].indexDataCountEnemySpawns++;
                            }
                        }
                        else
                        {
                            //Genero al enemigo de forma individual.

                            if (currentPool != null)
                            {

                                go = currentPool.GetObject();
                                go.transform.position = new Vector3(transform.position.x + (Random.Range(-rangeGenerationX, rangeGenerationX)), Height, transform.position.z + (Random.Range(-rangeGenerationZ, rangeGenerationZ)));
                                waves[indexWave].currentEnemysGenerate++;
                                waves[indexWave].indexDataCountEnemySpawns++;
                            }
                        }
                        waves[indexWave].indexDelayGenerationSpawn++;

                    }
                    else
                    {
                        currentDelay -= Time.deltaTime;
                        waves[indexWave].delayGenerationEnemys[waves[indexWave].indexDelayGenerationSpawn] = currentDelay;
                    }
                    if (waves[indexWave].currentEnemysGenerate >= waves[indexWave].countTotalEnemysWave)
                    {
                        indexWave++;
                        currentPool = null;
                    }
                }
                else
                {
                    delayBetweenWaves = delayBetweenWaves - Time.deltaTime;
                }
            }
        }
        else if (typeGenerator == TypeGenerator.Infinite)
        {
            //Programar un estilo de generacion infinita en donde cada ronda sera progresivamente mas dificil a
            //la anterior, ademas hacer que los enemigos se elijan completamente aleatoria ya sea un conjunto de enemigos
            //juntos generados de golpe o varios enemigos individuales caminando.
            
            /* 
             Para hacer esto hacer que los datos de la wave actual se llenen de forma automatica. asi por cada nueva 
             ronda se utiliza la misma wave solo que con datos renovados 
            */
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckGenerate();    
    }
}
