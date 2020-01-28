using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public string nameLoadScene;
    public void SceneLoad()
    {
        SceneManager.LoadScene(nameLoadScene);
    }
    public void SceneLoad(int level)
    {
        GameData.instaceGameData.currentLevel = level;
        SceneManager.LoadScene(nameLoadScene);
    }
    public void SceneLoad(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void NextLevel(ref GameData gd)
    {
        gd.currentLevel++;
        SceneManager.LoadScene("Nivel " + gd.currentLevel);
    }
}
