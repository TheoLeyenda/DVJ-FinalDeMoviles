using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkTreeFPS;

public class UIGameData : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textScore;
    public Text textLifes;
    //public Text textConstruction;
    //public Text textRemainingEnemies;
    public Text textWave;
    public Text textCountWaves;
    public GenerateEnemyManager gem;

    private int wave = 0;
    public GameManager gm;
    private GameData gd;

    private bool enableCountdown = false;
    private void Awake()
    {
        gd = GameData.instaceGameData;
        textScore.text = "Puntaje: 0$";
        textLifes.text = ""+gm.countLifes;
        textWave.text = "Ronda 1";
        if (gm.InTutorial)
        {
            wave = 1;
        }
        textCountWaves.text = "Rondas: "+wave +"/" + gem.countTotalWaves;
        if (gm.inSurvivalMode)
        {
            textWave.gameObject.SetActive(false);
        }
    }
    /*public void CheckCountdown()
    {
        if (gem.DelayStartRound >= 0)
        { 
            CountdownObject.SetActive(true);
            textCountdownObject.text = "" + Mathf.Round(gem.DelayStartRound);
        }
        else
        {
            CountdownObject.SetActive(false);
        }
    }
    private void Update()
    {
        CheckCountdown();
    }*/
    private void OnEnable()
    {
        Enemy.LifeIsZero += AddScoreForDieEnemy;
        BalisticProjectile.OnHitEnemy += AddScoreForHitEnemy;
        Enemy.OnFinishRoute += SubstractLifes;
        GenerateEnemyManager.OnFinishWave += AddWave;
    }
    private void OnDisable()
    {
        Enemy.LifeIsZero -= AddScoreForDieEnemy;
        BalisticProjectile.OnHitEnemy -= AddScoreForHitEnemy;
        Enemy.OnFinishRoute -= SubstractLifes;
        GenerateEnemyManager.OnFinishWave -= AddWave;
    }
    public void AddScoreForHitEnemy(BalisticProjectile bp)
    {
        gd.currentScore = gd.currentScore + gd.scoreForHitEnemy;
        textScore.text = "Puntaje: " + gd.currentScore + "$";
    }
    public void AddScoreForDieEnemy(Enemy e)
    {
        gd.currentScore = gd.currentScore + gd.scoreForDieEnemy;
        textScore.text = "Puntaje: " + gd.currentScore + "$";
    }
    public void SubstractLifes(Enemy e)
    {
        gm.countLifes = gm.countLifes - e.DamageLifes;
        textLifes.text = ""+gm.countLifes;
    }
    public void AddWave(GenerateEnemyManager _gem)
    {
        enableCountdown = true;
        wave++;
        textWave.text = "Ronda " + wave;
        if (wave > _gem.countTotalWaves)
        {
            textCountWaves.color = Color.yellow;
        }
        else
        {
            textCountWaves.text = "Rondas: " + wave + "/" + _gem.countTotalWaves;
        }
    }
}
