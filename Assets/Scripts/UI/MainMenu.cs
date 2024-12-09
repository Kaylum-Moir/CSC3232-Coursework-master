using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsScreen;

    public void OnPlay ()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnQuit ()
    {
        Application.Quit();
    }

    public void OnCredits ()
    {
        SceneManager.LoadScene("Credits");
    }
}
