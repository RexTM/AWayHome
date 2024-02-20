using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;

    private void Update()
    {
        //Checks for user input to toggle pause
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
            //ShowPauseMenu();
        }
        else
        {
            Time.timeScale = 1;
            //HidePauseMenu();
        }

        //private void ShowPauseMenu()
        {
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
                
            }
        }

        //private void HidePauseMenu()
        {
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
                
            }
        }

        //public void LoadScene(string sceneName)
        {
            //SceneManager.LoadScene(sceneName);
        }
    }
}
