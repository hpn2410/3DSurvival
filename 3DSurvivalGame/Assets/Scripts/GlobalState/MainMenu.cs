using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject guideMenu;
    public GameObject mainMenu_bg;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        Debug.LogWarning("Load Scene");
    }

    public void Guide()
    {
        guideMenu.SetActive(true);
        mainMenu_bg.SetActive(false);
    }

    public void QuitGame()
    {
        //Application.Quit();
        Debug.LogWarning("Quit Game");

        EditorApplication.isPlaying = false;

    }

    public void CloseBtn()
    {
        guideMenu.SetActive(false);
        mainMenu_bg.SetActive(true);
    }
}
