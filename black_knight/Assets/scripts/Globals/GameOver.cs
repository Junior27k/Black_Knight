using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public string level;
     public void Restart() 
    {
        SceneManager.LoadScene(level);
    }
    

    public void BackaToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
