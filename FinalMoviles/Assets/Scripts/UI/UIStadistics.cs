using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStadistics : MonoBehaviour
{
    public GameObject camvasStadistics;
    public Text textCountEnemysDie;
    public Text textCountBulletsShoots;
    public Text textScore;
    public Text textTotalScore;
    public Text textNotUnlockedConstruction;
    public Image imageUnlockedConstruction;
    public GameObject button;
    [HideInInspector]
    public bool unlockedConstruction;
    private GameData gd;
    private int bulletsShoots = 0;
    private void Start()
    {
        gd = GameData.instaceGameData;
    }
    private void OnEnable()
    {
        if (gd == null)
        {
            gd = GameData.instaceGameData;
        }
        ShowData();
    }
    private void Update()
    {
        if (gd == null)
        {
            gd = GameData.instaceGameData;
        }
    }
    public void ShowData()
    {
        if (unlockedConstruction)
        {
            imageUnlockedConstruction.gameObject.SetActive(true);
            button.transform.position = button.transform.position - new Vector3(0, 180, 0);
            textNotUnlockedConstruction.text = "¡Edificio Desbloqueado!";
            textNotUnlockedConstruction.color = Color.yellow;
        }
        else
        {
            textNotUnlockedConstruction.text = "Ningun Edificio Desbloqueado";
            textNotUnlockedConstruction.color = Color.red;
            imageUnlockedConstruction.gameObject.SetActive(false);
        }
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
