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
