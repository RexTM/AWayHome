using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public GameObject PauseButton;
    private GameObject pauseMenuInstance;

    //private bool isPaused = false;
    private string previousScene; // Variable to store the previous scene name

    private void Start()
    {
         pauseMenuInstance = Instantiate(PauseButton);
            pauseMenuInstance.SetActive(false);
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
        SceneManager.LoadScene("pauseMenu");

        if(pauseMenuInstance != null)
        {
            pauseMenuInstance.SetActive(!pauseMenuInstance.activeSelf);
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

    public void LoadScene(string pauseMenu)
    {
        SceneManager.LoadScene(pauseMenu);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        HidePauseMenu();
        SceneManager.LoadScene(previousScene);
    }
}
