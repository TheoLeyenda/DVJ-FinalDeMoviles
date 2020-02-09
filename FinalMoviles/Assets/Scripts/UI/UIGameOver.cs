using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGameOver : MonoBehaviour
{
    public Text textCountEnemysDie;
    public Text textCountBulletsShoots;
    public Text textScore;
    public Text textTotalScore;
    private GameData gd;
    public GameManager gm;
    private void Start()
    {
        gd = GameData.instaceGameData;
    }
    private void OnEnable()
    {
        gd = GameData.instaceGameData;
        ShowDataGameOver();   
    }
    public void ShowDataGameOver()
    {
        gm.PlayerPC.lockCursor = false;
        textCountEnemysDie.text = "Enemigos Abatidos: " + gd.countEnemysDie;
        textCountBulletsShoots.text = "Balas Disparadas: " + gd.countBulletsShoots;
        textScore.text = "Puntaje: " + gd.currentScore + "$";
        gd.generalScore = gd.generalScore + gd.currentScore;
        textTotalScore.text = "Puntaje Total: " + gd.generalScore + "$";
        gd.countEnemysDie = 0;
        gd.countBulletsShoots = 0;
        gd.currentScore = 0;
    }
}
