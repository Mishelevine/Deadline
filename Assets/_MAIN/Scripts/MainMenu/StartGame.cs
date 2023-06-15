using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("VisualNovel");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is closed!!!");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
}
