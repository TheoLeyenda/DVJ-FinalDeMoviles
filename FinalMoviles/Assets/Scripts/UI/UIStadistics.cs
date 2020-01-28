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
    private void OnEnable()
    {
        ShowData();
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
    }
}
