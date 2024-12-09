using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    private bool visible;

    public void OnQuit ()
    {
        SceneManager.LoadScene("Title Screen");
    }

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            visible = !visible;
            menu.SetActive(visible);
            if (menu)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                OnResume();
            }
            
        }
    }

    public void OnResume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        visible = false;
        menu.SetActive(visible);
    }
}
