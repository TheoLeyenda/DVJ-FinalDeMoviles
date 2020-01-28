using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    public float speedLoad;
    private float porcentage;
    public GameData gd;
	private string sceneToLoad;

	[SerializeField]
	private Text percentText;

	[SerializeField]
	private Image progressImage;

    AsyncOperation loading;

    

    private void Awake()
    {
        gd = GameData.instaceGameData;
        sceneToLoad = "Nivel " + gd.currentLevel;
    }
    void Start () {
        //StartCoroutine(LoadScene());
        loading = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        loading.allowSceneActivation = false;
    }
    private void Update()
    {
        CheckLoad();
    }
    public void CheckLoad()
    {
        if (porcentage < 100)
        {
            porcentage = porcentage + Time.deltaTime * speedLoad;
            progressImage.fillAmount = porcentage / 100;
            percentText.text = (int)porcentage + "%";
        }
        
        if (porcentage >= 99)
        {
            //Debug.Log("ENTRE");
            //percentText.text = "100%";
            //progressImage.fillAmount = 1;
            //progressImage.fillAmount = loading.progress;
            loading.allowSceneActivation = true;

        }
    }
    /*IEnumerator LoadScene()
	{
		AsyncOperation loading;

		loading = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

		loading.allowSceneActivation = false;

		while (loading.progress < 0.9f) {
			
			percentText.text = string.Format ("{0}%", loading.progress * 100);

			progressImage.fillAmount = loading.progress;

			yield return null;
		}

		percentText.text = "100%";
		progressImage.fillAmount = 1;

		loading.allowSceneActivation = true;


	}*/

}
