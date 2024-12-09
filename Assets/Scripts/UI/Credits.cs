using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject creditsScreen;

    public void OnBack ()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
