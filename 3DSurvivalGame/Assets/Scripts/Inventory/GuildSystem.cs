using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildSystem : MonoBehaviour
{
    public bool isOpen;
    public GameObject guidScreenUI;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && !isOpen)
        {
            guidScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
        }
        else if(Input.GetKeyDown(KeyCode.H) && isOpen)
        {
            guidScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpen = false;
        }
    }

    public void OnClosedButtonClicked()
    {
        guidScreenUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isOpen = false;
    }
}
