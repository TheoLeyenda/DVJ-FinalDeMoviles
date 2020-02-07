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

    private int wave = 1;
    public GameManager gm;
    private GameData gd;

    private void Awake()
    {
        gd = GameData.instaceGameData;
        textScore.text = "Puntaje: 0$";
        textLifes.text = ""+gm.countLifes;
        textWave.text = "Ronda 1";
        textCountWaves.text = "Rondas: 1/" + gem.countTotalWaves;
        if (gm.inSurvivalMode)
        {
            textWave.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        Enemy.OnDieAction += AddScoreForDieEnemy;
        BalisticProjectile.OnHitEnemy += AddScoreForHitEnemy;
        Enemy.OnFinishRoute += SubstractLifes;
        GenerateEnemyManager.OnFinishWave += AddWave;
    }
    private void OnDisable()
    {
        Enemy.OnDieAction -= AddScoreForDieEnemy;
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
