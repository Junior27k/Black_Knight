using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Vector3 lastCheckpointPosition;
    private bool hasCheckpoint = false;
    private int checkpointTimes = 3;

    [Header("UI")]
    public TextMeshProUGUI textLife;
    public GameObject gameOverScreen;
    public GameObject pauseMenu;

    private bool isPaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        LoadGame();
    }

    public void UpdateLifeUI(int life)
    {
        if (textLife != null)
            textLife.text = life.ToString();
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        hasCheckpoint = true;
    }

    public void RespawnPlayer(PlayerController player)
    {
        if (hasCheckpoint)
        {
            if(checkpointTimes-- <= 0){
                hasCheckpoint = false;
            }
            player.transform.position = lastCheckpointPosition;
            player.life = 3; // Opcional: Restaurar vida ao reviver
            UpdateLifeUI(player.life);
            this.enabled = true;
            player.gameObject.SetActive(true);
            Debug.Log("Jogador renasceu no checkpoint!");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveGame(int life)
    {
        PlayerPrefs.SetInt("Life", life);
        PlayerPrefs.SetString("SavedLevel", SceneManager.GetActiveScene().name);
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("wasLoaded", 0) == 1)
        {
            Debug.Log("Game Loaded");
        }
    }
}
