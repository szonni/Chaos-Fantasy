using UnityEngine;

public class GameManager : MonoBehaviour
{
    // These are the possible states of the game while running
    public enum GameState
    {
        Playing,
        Paused
    }
    public GameState currentState;

    void Update()
    {
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
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        // The scale at which time passes
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }
}
