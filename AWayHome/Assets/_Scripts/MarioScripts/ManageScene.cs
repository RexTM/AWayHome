using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public static int gameSceneIndex;
    public static int tempSceneIndex;

    [Header("Use this to force a scene after the bubble minigame")]
    public bool forceSceneSelection = false;
    [Header("(Use Index from build order)")]
    public int forcedScene = 0;

    [Header("Checkpoints")]
    public int checkpointScene = 0;

    private void Start()
    {
        gameSceneIndex = PlayerPrefs.GetInt("LastGameplayScene", 0);
        Debug.Log(gameSceneIndex);
    }

    public void bubleGameTransition1()
    {
        gameSceneIndex = SceneManager.GetActiveScene().buildIndex;
        tempSceneIndex = gameSceneIndex;
        SceneManager.LoadScene(15);
    }
    //temp
    public void bubleGameTransition2()
    {
        gameSceneIndex = SceneManager.GetActiveScene().buildIndex;
        tempSceneIndex = gameSceneIndex;
        SceneManager.LoadScene(16);
    }
    public void bubleGameTransition3()
    {
        gameSceneIndex = SceneManager.GetActiveScene().buildIndex;
        tempSceneIndex = gameSceneIndex;
        SceneManager.LoadScene(17);
       
    }
    //temp
    public void returnToStory()
    { 
        if (forceSceneSelection)
        {
            SceneManager.LoadScene(forcedScene);
        }
        else
        {
            tempSceneIndex++;
            SceneManager.LoadScene(tempSceneIndex);
        }
    }

}
