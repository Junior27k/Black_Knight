using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public static LoadGame Instance;
    public bool wasLoaded;

    void Awake() {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void Loading(){
        if(PlayerPrefs.HasKey("LevelSaved")){
            wasLoaded = true;
            PlayerPrefs.SetInt("wasLoaded", 1);

            string levelToLoad = PlayerPrefs.GetString("LevelSaved");
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
