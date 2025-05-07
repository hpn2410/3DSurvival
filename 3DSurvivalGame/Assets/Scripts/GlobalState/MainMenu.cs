using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject guideMenu;
    public GameObject mainMenu_bg;
    public GameObject loadingMenu;
    public Slider loadingSlider;
    public void PlayGame()
    {
        mainMenu_bg.SetActive(false);
        loadingMenu.SetActive(true);
        StartCoroutine(LoadSceneAsync(1));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void Guide()
    {
        guideMenu.SetActive(true);
        mainMenu_bg.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Quit Game");

    }

    public void CloseBtn()
    {
        guideMenu.SetActive(false);
        mainMenu_bg.SetActive(true);
    }
}
