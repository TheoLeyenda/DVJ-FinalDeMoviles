﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject CamvasStart;
    public GameObject CamvasMenu;
    public GameObject CamvasControls;
    public GameObject CamvasInsertName;
    public GameObject CamvasCredits;
    public GameObject CamvasSelectionSurvival;
    public GameObject CamvasSelectionLevelStory;
    public GameObject CamvasLobyInGame;
    public GameObject CamvasShop;
    public Text textInputField;
    private string userName;
    private GameData gd;
    public Text textUserNameLoby;
    public Text textGeneralScore;

    private void Start()
    {
        gd = GameData.instaceGameData;
    }

    public void StartGame() //BOTON DE INICIO
    {
        CamvasStart.SetActive(false);
        CamvasMenu.SetActive(true);
    }

    public void Controls()
    {
        CamvasMenu.SetActive(false);
        CamvasControls.SetActive(true);
    }
    public void InsertName()
    {
        CamvasMenu.SetActive(false);
        CamvasInsertName.SetActive(true);
    }
    public void Credits()
    {
        CamvasMenu.SetActive(false);
        CamvasCredits.SetActive(true);
    }
    public void LobyGame()
    {
        CamvasInsertName.SetActive(false);
        CamvasLobyInGame.SetActive(true);
        textUserNameLoby.text = "Partida: "+gd.currentNameUser+".";
        textGeneralScore.text = "Puntaje: "+gd.generalScore +"$";
    }
    public void SelectLevelSurvival()
    {
        CamvasLobyInGame.SetActive(false);
        CamvasSelectionSurvival.SetActive(true);
    }
    public void SelectLevelStory()
    {
        CamvasLobyInGame.SetActive(false);
        CamvasSelectionLevelStory.SetActive(true);
    }
    public void OpenShop()
    {
        CamvasLobyInGame.SetActive(false);
        CamvasShop.SetActive(true);
    }
    public void BackMainMenu(GameObject go)
    {
        go.SetActive(false);
        CamvasMenu.SetActive(true);
    }
    public void BackLobyGame(GameObject go)
    {
        go.SetActive(false);
        CamvasLobyInGame.SetActive(true);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void ConfirmNameUser()
    {
        userName = textInputField.text;
        gd.currentNameUser = userName;
        LobyGame();
    }
}