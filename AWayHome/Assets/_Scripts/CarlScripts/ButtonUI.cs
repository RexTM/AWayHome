using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "CarlScene";
    [SerializeField] private string buttonName;
    public void OnButtonClick()
    {
        SceneManager.LoadScene(newGameLevel);
        Debug.Log($"Button '{buttonName}' clicked!");

        //Button click sound
        sfxManager.sfxInstance.Audio.PlayOneShot(sfxManager.sfxInstance.Click);
    }


    public void UseWishbone()
    {
        if (PlayerData.wishBones >= 50)
        {
            PlayerData.wishBones -= 50;
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
