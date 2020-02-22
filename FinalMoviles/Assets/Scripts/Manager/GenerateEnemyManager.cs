using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GenerateEnemyManager : MonoBehaviour
{
    // HACER QUE EN TODAS LAS RONDAS EL BOTON DE SIGUIENTE RONDA Y EL BOTON ENTER LLAMEN A LA FUNCION EnableNextRound() DE ESTA CLASE.
    public bool usingDelayStartRound;
    public List<EnemyGenerate> enemyGenerates;
    private bool enableCountdown;
    public bool infinityGenerator;
    public float DelayStartRound;
    public float auxDelayStartRound;
    public float porcentageDisableGenerator;
    private bool AdvanceGeneration = false;
    private bool activateAllGenerators;
    private bool startFirstRoundInfiniteGenerator = false;
    public UINextWave uiNextWave;

    private int countfinishWave;
    public int currentWave = 0;
    public int countTotalWaves;

    [SerializeField]
    public bool onceActivateElementsUiNextWave = true;
    private bool ActivateElementsUiNextWave;

    private bool finishGenerator = false;
    public static event Action<GenerateEnemyManager> OnFinishWave;

    [Header("Variables Generador Infinito")]
    public int maxAddEnemyInfiniteGenerator = 4;
    public int minAddEnemyInfiniteGenerator = 2;
    private int countTotalEnemyGenerate;
    private int countEnemyDie = 0;
    private int countEnemyGenerate;
    private bool regulatorGenerate;
    //private bool enableGenerators;
    private void Awake()
    {
        countTotalEnemyGenerate = 5;
    }
    private void Start()
    {
        
        regulatorGenerate = true;
        //enableGenerators = true;
        if (!infinityGenerator)
        {
            if (enemyGenerates[0] != null)
            {
                countfinishWave = enemyGenerates[0].waves.Count;
            }
        }
        else
        {
            countfinishWave = 10;
        }
        if (porcentageDisableGenerator > 100)
        {
            porcentageDisableGenerator = 100;
        }
        else if (porcentageDisableGenerator < 0)
        {
            porcentageDisableGenerator = 0;
        }
        
    }
    private void OnEnable()
    {
        EnemyGenerate.OnGenerateEnemy += AddCountEnemyGenerate;
        Enemy.OnDieAction += AddEnemysDie;
    }
    private void OnDisable()
    {
        EnemyGenerate.OnGenerateEnemy -= AddCountEnemyGenerate;
        Enemy.OnDieAction -= AddEnemysDie;
    }

    public void AddEnemysDie(Enemy e)
    {
        if (e.nameEnemy != "MiniSpider" && e.nameEnemy != "Slime_2(Small)")
        {
            if (countEnemyDie < countTotalEnemyGenerate)
            {
                countEnemyDie++;
            }
            else
            {
                countEnemyDie = countTotalEnemyGenerate;
            }
            //enemysDie++;
            //Debug.Log("Enemigos muertos: "+ enemysDie);
        }
    }

    private void Update()
    {
        CheckEnableCountdown();
    }
    public void AddCountEnemyGenerate(EnemyGenerate eg)
    {
        countEnemyGenerate++;
    }
    public void CheckEnableCountdown()
    {
        bool enable = true;
        ActivateElementsUiNextWave = true;
        for (int i = 0; i < enemyGenerates.Count; i++)
        {
            if (!enemyGenerates[i].finishRound || !enemyGenerates[i].ready || !enemyGenerates[i].gameObject.activeSelf)
            {
                enable = false;
            }
            if (!enemyGenerates[i].gameObject.activeSelf)
            {
                ActivateElementsUiNextWave = false;
            }
        }
        enableCountdown = enable;
        if (ActivateElementsUiNextWave && onceActivateElementsUiNextWave)
        { 
            uiNextWave.activateElementsCamvasNextWave = true;
        }
        else if (!enableCountdown && !activateAllGenerators || currentWave >= countfinishWave)
        {
            DelayStartRound = auxDelayStartRound;
            if(uiNextWave.activateElementsCamvasNextWave && !infinityGenerator)
                uiNextWave.activateElementsCamvasNextWave = false;
        }
        //Debug.Log(enableCountdown)
        if (enableCountdown || activateAllGenerators || infinityGenerator)
        {
            
            if (currentWave < countfinishWave || infinityGenerator)
            {
                if (AdvanceGeneration && !infinityGenerator)
                {
                    DelayStartRound = 0;
                    AdvanceGeneration = false;
                }
                if (!uiNextWave.activateElementsCamvasNextWave && !infinityGenerator)
                    uiNextWave.activateElementsCamvasNextWave = true;
                //Debug.Log("countEnemyGenerate " + countEnemyGenerate);
                //Debug.Log("countTotalEnemyGenerate" + countTotalEnemyGenerate);
                //Debug.Log(DelayStartRound);
                if (DelayStartRound <= 0 || infinityGenerator)
                {
                    if (infinityGenerator && startFirstRoundInfiniteGenerator)
                    {
                        
                        //(Debug.Log(countEnemyDie + "/" + countTotalEnemyGenerate);
                        if (countEnemyDie >= countTotalEnemyGenerate)
                        {
                            
                            regulatorGenerate = true;
                            enableCountdown = false;
                            activateAllGenerators = false;
                            uiNextWave.activateElementsCamvasNextWave = true;
#if UNITY_STANDALONE
                            uiNextWave.textStartWave.gameObject.SetActive(true);
#else
                            uiNextWave.buttonStartWave.gameObject.SetActive(true);
#endif

                            //enableGenerators = true;
                        }

                        if (countEnemyGenerate >= countTotalEnemyGenerate && countTotalEnemyGenerate > 0)
                        {
                            //Debug.Log("ENTRE");
                            for (int i = 0; i < enemyGenerates.Count; i++)
                            {
                                //enemyGenerates[i].enableGenerateInfinite = false;
                                enemyGenerates[i].gameObject.SetActive(false);
                            }
                        }
                        else if (countEnemyGenerate < countTotalEnemyGenerate && regulatorGenerate)
                        {
                            for (int i = 0; i < enemyGenerates.Count; i++)
                            {
                                float a = UnityEngine.Random.Range(0, 100);
                                //enemyGenerates[i].StartGenerate = true;
                                enemyGenerates[i].typeGenerator = EnemyGenerate.TypeGenerator.Infinite;
                                //enemyGenerates[i].ready = false;
                                //enemyGenerates[i].skipRound = false;
                                //enemyGenerates[i].skipRound = false;
                                
                                if (i > 0 && enemyGenerates.Count > 1)
                                {
                                    //Debug.Log(a);
                                    if (a <= porcentageDisableGenerator)
                                    {
                                        //Debug.Log("ENTRE");
                                        //enemyGenerates[i].ready = true;
                                        //enemyGenerates[i].skipRound = true;
                                        //enemyGenerates[i].StartGenerate = false;
                                        enemyGenerates[i].typeGenerator = EnemyGenerate.TypeGenerator.None;
                                        //enemyGenerates[i].finishRound = true;
                                    }
                                    else
                                    {
                                        //Debug.Log("GENERADOR ACTIVADO");
                                    }
                                }
                            }
                            
                            regulatorGenerate = false;
                        }
                        
                    }
                    if (!infinityGenerator)
                    {
                        for (int i = 0; i < enemyGenerates.Count; i++)
                        {
                            if (enemyGenerates[i].GetIndexWave() < enemyGenerates[i].waves.Count)
                            {
                                enemyGenerates[i].typeGenerator = EnemyGenerate.TypeGenerator.Finite;
                                enemyGenerates[i].StartGenerate = true;
                                enemyGenerates[i].ready = false;
                                enemyGenerates[i].finishRound = false;
                                enemyGenerates[i].skipRound = false;

                                if (enemyGenerates[i].waves[enemyGenerates[i].GetIndexWave()].skipWave)
                                {
                                    enemyGenerates[i].typeGenerator = EnemyGenerate.TypeGenerator.None;
                                    enemyGenerates[i].StartGenerate = false;
                                    enemyGenerates[i].ready = true;
                                    enemyGenerates[i].finishRound = true;
                                    enemyGenerates[i].skipRound = true;

                                    enemyGenerates[i].SetIndexWave(enemyGenerates[i].GetIndexWave() + 1);
                                }
                            }

                        }
                        currentWave++;
                        if (OnFinishWave != null)
                        {
                            OnFinishWave(this);
                        }
                        DelayStartRound = auxDelayStartRound;
                        enableCountdown = false;
                        activateAllGenerators = false;
                    }
                    

                }
                else if (DelayStartRound > 0)
                {
                    if (usingDelayStartRound)
                    {
                        DelayStartRound = DelayStartRound - Time.deltaTime;
                        /*if (infinityGenerator && enableGenerators)
                        {
                            for (int i = 0; i < enemyGenerates.Count; i++)
                            {
                                enemyGenerates[i].countEnemysGenerate = 0;
                                enemyGenerates[i].totalEnemyRound = countTotalEnemyGenerate;
                            }
                            enableGenerators = false;
                        }*/
                    }
                }
            }
            else
            {
                finishGenerator = true;
                //Debug.Log("NIVEL TERMINADO");
                //Debug.ClearDeveloperConsole();
                //EL JUGADOR GANO EL NIVEL.
            }
        }

    }
    public void EnableNextRound()
    {
        //Debug.Log("ENTRE");
        if (OnFinishWave != null)
        {
            OnFinishWave(this);
        }
        startFirstRoundInfiniteGenerator = true;
        countTotalEnemyGenerate = countTotalEnemyGenerate + UnityEngine.Random.Range(minAddEnemyInfiniteGenerator, maxAddEnemyInfiniteGenerator);
        countEnemyDie = 0;
        //DelayStartRound = auxDelayStartRound;
        currentWave++;
        countEnemyGenerate = 0;
#if UNITY_STANDALONE
        uiNextWave.textStartWave.gameObject.SetActive(false);
#else
        uiNextWave.buttonStartWave.gameObject.SetActive(false);
#endif
        for (int i = 0; i < enemyGenerates.Count; i++)
        {
            enemyGenerates[i].gameObject.SetActive(true);
            enemyGenerates[i].StartInfiniteGenerate = true;
        }
    }
    public bool GetFinishGenerator()
    {
        return finishGenerator;
    }
    public void ActivateAllGenerators()
    {
        enableCountdown = true;
        AdvanceGeneration = true;
        activateAllGenerators = true;
        onceActivateElementsUiNextWave = false;
    }
}
