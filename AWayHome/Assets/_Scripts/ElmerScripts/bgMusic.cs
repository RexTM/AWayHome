 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class bgMusic : MonoBehaviour
{
    public static bgMusic bgInstance;
    private AudioSource audioSource;

    public AudioClip defaultMusicClip; 
    public AudioClip newMusicClip;    
    public AudioClip sadBark;
    public AudioClip howlBark;
    public AudioClip multiBark;
    private bool hasPlayedSadBark = false;
    private bool hasPlayedHowlBark = false;
    private bool hasPlayedMultiBark = false;


    private void Awake()
    {
        if (bgInstance != null && bgInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        bgInstance = this;
        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();

        // Set the default music clip as the audio clip for the AudioSource
        if (defaultMusicClip != null)
        {
            audioSource.clip = defaultMusicClip;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 21 && newMusicClip != null)
        {
            // Play new music for scene 21
            audioSource.clip = newMusicClip;
            audioSource.Play();
        }
        else if (scene.buildIndex == 107 && sadBark != null && !hasPlayedSadBark)
        {
            // Play sadBark once for scene 107
            audioSource.PlayOneShot(sadBark);
            hasPlayedSadBark = true;
        }
        else if (scene.buildIndex == 117 && sadBark != null && !hasPlayedHowlBark)
        {
            // Play sadBark once for scene 117
            audioSource.PlayOneShot(howlBark);
            hasPlayedSadBark = true;
        }
        else if (scene.buildIndex == 127 && sadBark != null && !hasPlayedHowlBark)
        {
            // Play sadBark once for scene 127
            audioSource.PlayOneShot(howlBark);
            hasPlayedSadBark = true;
        }
        else if (scene.buildIndex == 4 && sadBark != null && !hasPlayedMultiBark)
        {
            audioSource.PlayOneShot(multiBark);
            hasPlayedMultiBark = true;
        }
        else if (scene.buildIndex == 55 && sadBark != null && !hasPlayedMultiBark)
        {
            audioSource.PlayOneShot(multiBark);
            hasPlayedMultiBark = true;
        }
        else if (scene.buildIndex == 94 && sadBark != null && !hasPlayedMultiBark)
        {
            audioSource.PlayOneShot(multiBark);
            hasPlayedMultiBark = true;
        }
        else
        {
            // Play default music for other scenes
            if (defaultMusicClip != null && audioSource.clip != defaultMusicClip)
            {
                audioSource.clip = defaultMusicClip;
                audioSource.Play();
            }
        }
    }
}
