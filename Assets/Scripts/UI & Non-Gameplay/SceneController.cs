using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
        ScoreBoard.Instance.ResetScoreboard();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
