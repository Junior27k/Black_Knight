using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    
    public string level;
    public void Restart()
    {
        SceneManager.LoadScene(level);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(1);
    }
}
