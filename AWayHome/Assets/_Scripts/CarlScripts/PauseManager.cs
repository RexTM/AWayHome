using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;
    private string previousScene; // Variable to store the previous scene name

    private void Start()
    {
        previousScene = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            ShowPauseMenu();
        }
        else
        {
            Time.timeScale = 1;
            HidePauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    private void HidePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        HidePauseMenu();
        SceneManager.LoadScene(previousScene);
    }
}
