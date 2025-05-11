using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingSystem : MonoBehaviour
{
    public bool isOpen;
    public GameObject settingScreenUI;
    public GameObject loadingScreenUI;
    public Slider loadingSlider;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isOpen)
        {
            settingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.K) && isOpen)
        {
            settingScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpen = false;
        }
    }

    public void OnClosedButtonClicked()
    {
        settingScreenUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isOpen = false;
    }

    public void OnBackToMenuButtonClicked()
    {
        settingScreenUI.SetActive(false);
        loadingScreenUI.SetActive(true);
        StartCoroutine(LoadSceneAsync(0));
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
}
