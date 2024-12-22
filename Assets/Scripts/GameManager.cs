using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // These are the possible states of the game while running
    public enum GameState
    {
        Playing,
        Paused,
        LevelUp,
    }
    public GameState currentState;

    [Header("Stopwatch")]
    public TextMeshProUGUI stopWatchDisplay;
    private float stopWatchTime;

    public static GameManager instance;

    [Header("Screens")]
    public GameObject levelUpScreen;

    [Header("UI Elements")]
    public GameObject pauseMenu; 
    public Button pauseButton;
    public AudioManager audioManager;
    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI enemyKilledText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI totalDamageText;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // Prevent the creation of another instance of this class
        else
        {
            Debug.Log("GameManager duplicate destroyed: " + this);
            Destroy(gameObject);
        }

        DisableScreens();
    }

    void DisableScreens()
    {
        levelUpScreen.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    void Start()
    {
        currentState = GameState.Playing;
        pauseMenu.SetActive(false);  // Hide the pause menu initially
        Time.timeScale = 1f;

    // Gọi nhạc gameplay khi bắt đầu game
    if (audioManager != null)
    {
        audioManager.PlayGameplayMusic();  // Chạy nhạc gameplay
    }

    // Set up the button's onClick event to toggle pause state
    pauseButton.onClick.AddListener(TogglePause);
}    void Update()
    {
        if (currentState == GameState.Playing)
        {
            UpdateStopWatch();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is paused then resume it
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            // If the game is playing then pause it
            else if (currentState == GameState.Playing)
            {
                TogglePause();
            }
        }

    }
    public void TogglePause()
    {
        if (currentState == GameState.Paused)
        {
            ResumeGame();
        }
        else if (currentState == GameState.Playing)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        // The scale at which time passes
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void StartLevelUpScreen()
    {
        currentState = GameState.LevelUp;
        Time.timeScale = 0f;
        levelUpScreen.SetActive(true);
    }

    public void EndLevelUpScreen()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
    }

    void UpdateStopWatch()
    {
        stopWatchTime += Time.deltaTime;
        UpdateStopWatchDisplay();
    }

    void UpdateStopWatchDisplay()
    {
        int minutes = (int)Math.Floor(stopWatchTime / 60);
        int seconds = (int)Math.Floor(stopWatchTime % 60);

        stopWatchDisplay.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

       public void StartGame()
    {
        // Chuyển sang Gameplay Scene
        SceneManager.LoadScene("GameplayScene");

        // Gọi nhạc gameplay sau khi chuyển scene
        if (audioManager != null)
        {
            audioManager.PlayGameplayMusic();
        }
    }
    public void TriggerGameOver()
    {
        ShowGameOverScreen();
        Time.timeScale = 0; // Dừng trò chơi

    }
    public void ShowGameOverScreen()
    {
        // Gán dữ liệu từ ScoreBoard
        var scoreboard = ScoreBoard.Instance;
        enemyKilledText.text = $"Enemies Killed: {scoreboard.enemyKilled}";
        timeText.text = $"Time: {scoreboard.timeScoreboard.text}";
        playerLevelText.text = $"Player Level: {scoreboard.lvPlayer}";
        totalDamageText.text = $"Total Damage: {scoreboard.totalDamage}";

        gameOverPanel.SetActive(true);
    }
}

