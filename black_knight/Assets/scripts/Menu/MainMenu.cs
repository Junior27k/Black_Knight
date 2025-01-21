using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        LoadGame.Instance.wasLoaded = false;
        PlayerPrefs.DeleteAll();
         SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
