using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "CarlScene";
    [SerializeField] private string buttonName;
    public static int checkPointSceneIndex;

   

   
    public void OnButtonClick()
    {
        SceneManager.LoadScene(newGameLevel);
        Debug.Log($"Button '{buttonName}' clicked!");

        //Button click sound
        sfxManager.sfxInstance.Audio.PlayOneShot(sfxManager.sfxInstance.Click);
    }


    public void ResumeScene()
    {
        SceneManager.LoadScene(PauseManager.resumeSceneIndex);
        Debug.Log($"Button '{buttonName}' clicked!");

        //Button click sound
        sfxManager.sfxInstance.Audio.PlayOneShot(sfxManager.sfxInstance.Click);
    }

    public void GetCheckPointScene()
    {
        checkPointSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(newGameLevel);
        Debug.Log($"Button '{buttonName}' clicked!");

        
    }

    public void GetCheckPointSceneUseWishbone()
    {
        if (PlayerData.wishBones >= 75)
        {
            PlayerData.wishBones -= 75;
            checkPointSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(PlayerData.wishBones);
            SceneManager.LoadScene(newGameLevel);
            Debug.Log($"Button '{buttonName}' clicked!");

            //Button Click Sound
            sfxManager.sfxInstance.Audio.PlayOneShot(sfxManager.sfxInstance.Click);
        }
        
    }

    public void RestartFromCheckpoint()
    {
        PlayerData.wishBones = 0;
        SceneManager.LoadScene(checkPointSceneIndex);

        //Button Click Sound
        sfxManager.sfxInstance.Audio.PlayOneShot(sfxManager.sfxInstance.Click);
    }

    public void UseWishbone()
    {
        if (PlayerData.wishBones >= 75)
        {
            PlayerData.wishBones -= 75;
            Debug.Log(PlayerData.wishBones);
            SceneManager.LoadScene(newGameLevel);
            Debug.Log($"Button '{buttonName}' clicked!");

            //Button click sound
            sfxManager.sfxInstance.Audio.PlayOneShot(sfxManager.sfxInstance.Click);
        }
    }

    public void MainMenuWishBoneReset()
    {
        PlayerData.wishBones = 0;
        SceneManager.LoadScene(newGameLevel);
        Debug.Log($"Button '{buttonName}' clicked!");

        //Button click sound
        sfxManager.sfxInstance.Audio.PlayOneShot(sfxManager.sfxInstance.Click);
    }
}
