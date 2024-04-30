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
    public AudioClip cheersClip;    
    public AudioClip sadBark;
    public AudioClip howlBark;
    public AudioClip multiBark;
    public AudioClip grunt;
    public AudioClip laugh;
    public AudioClip scream;



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
        else if (scene.buildIndex == 163 && cheersClip != null)
        {
            // Play new music for scene 163
            audioSource.clip = cheersClip;
            audioSource.Play();
        }
        else if (scene.buildIndex == 107 )
        {
            // Play sadBark once for scene 107
            audioSource.PlayOneShot(sadBark);
          
        }
        else if (scene.buildIndex == 117 )
        {
            // Play sadBark once for scene 117
            audioSource.PlayOneShot(howlBark);
          
        }
        else if (scene.buildIndex == 127 )
        {
            // Play sadBark once for scene 127
            audioSource.PlayOneShot(howlBark);
            
        }
        else if (scene.buildIndex == 4 )
        {
            audioSource.PlayOneShot(multiBark);
           
        }
        else if (scene.buildIndex == 55 )
        {
            audioSource.PlayOneShot(multiBark);
            
        }
        else if (scene.buildIndex == 94 )
        {
            audioSource.PlayOneShot(multiBark);
          
        }
        else if (scene.buildIndex == 34)
        {
            audioSource.PlayOneShot(grunt);
            
        }
        else if (scene.buildIndex == 31)
        {
            audioSource.PlayOneShot(grunt);
           
        }
        else if (scene.buildIndex == 12)
        {
            audioSource.PlayOneShot(laugh);
            
        }
        else if (scene.buildIndex == 9)
        {
            audioSource.PlayOneShot(scream);
            
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
