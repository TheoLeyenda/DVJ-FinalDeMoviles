using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<EnemyGenerate> enemyGenerates;
    private bool enableCountdown;
    public bool infinityGenerator;
    public float DelayStartRound;
    public float auxDelayStartRound;
    public float porcentageDisableGenerator;
    private bool AdvanceGeneration = false;
    private bool once = true;
    private bool activateAllGenerators;
    private void Start()
    {
        if (porcentageDisableGenerator > 100)
        {
            porcentageDisableGenerator = 100;
        }
        else if (porcentageDisableGenerator < 0)
        {
            porcentageDisableGenerator = 0;
        }
    }
    private void Update()
    {
        CheckEnableCountdown();
    }
    public void CheckEnableCountdown()
    {
        bool enable = true;
        activateAllGenerators = true;
        for (int i = 0; i < enemyGenerates.Count; i++)
        {
            //if (!enemyGenerates[i].finishRound)
            //{
            // Debug.Log(i);
            //}
            if (!enemyGenerates[i].finishRound  || !enemyGenerates[i].ready || !enemyGenerates[i].gameObject.activeSelf)
            {

                enable = false;
            }
            if (!enemyGenerates[i].gameObject.activeSelf)
            {
                activateAllGenerators = false;
            }
        }
        enableCountdown = enable;
        if (!enableCountdown && !(once && activateAllGenerators))
        {
            DelayStartRound = auxDelayStartRound;
        }
        //Debug.Log(enableCountdown);
        if (enableCountdown || (once && activateAllGenerators))
        {
            if (AdvanceGeneration)
            {
                DelayStartRound = 0;
                enableCountdown = false;
                AdvanceGeneration = false;
            }

            if (DelayStartRound <= 0)
            {
                if (infinityGenerator && enemyGenerates.Count > 1)
                {
                    for (int i = 0; i < enemyGenerates.Count; i++)
                    {
                        float a = Random.Range(0, 100);
                        enemyGenerates[i].StartGenerate = true;
                        enemyGenerates[i].typeGenerator = EnemyGenerate.TypeGenerator.Infinite;
                        enemyGenerates[i].ready = false;
                        enemyGenerates[i].skipRound = false;
                        //enemyGenerates[i].skipRound = false;
                        if (i > 0)
                        {
                            if (a >= porcentageDisableGenerator)
                            {
                                enemyGenerates[i].ready = true;
                                enemyGenerates[i].skipRound = true;
                                enemyGenerates[i].StartGenerate = false;
                                enemyGenerates[i].typeGenerator = EnemyGenerate.TypeGenerator.None;
                                enemyGenerates[i].finishRound = true;
                            }
                            
                        }
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
                }
                DelayStartRound = auxDelayStartRound;
                enableCountdown = false;
                once = false;
                
            }
            else if(DelayStartRound > 0)
            {
                DelayStartRound = DelayStartRound - Time.deltaTime;
            }
        }
        
       
    } 
}
