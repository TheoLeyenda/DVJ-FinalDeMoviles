using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobyInGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textNameParty;
    public Text textCurrentScore;
    private GameData gd;

    private void OnEnable()
    {
        gd = GameData.instaceGameData;
        textNameParty.text = "Partida: "+gd.currentNameUser+".";
        textCurrentScore.text = "Puntaje: " + gd.generalScore + "$";
    }
}
