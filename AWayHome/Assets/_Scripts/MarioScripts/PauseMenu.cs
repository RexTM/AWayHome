using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool pausePressed = false;
    public GameObject pauseMenuUI;

    private void Update()
    {
        if (pausePressed)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            pausePressed = false;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadHowToPlay()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void PressPause()
    {
        pausePressed = true;
    }

}
