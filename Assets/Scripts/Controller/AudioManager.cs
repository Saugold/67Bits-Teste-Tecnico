using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip buySound;
    [SerializeField] private AudioClip sellSound;

    [Header("Background Music")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip backgroundMusic;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        PlayBackgroundMusic();
    }

    public void PlayBuySound()
    {
        sfxAudioSource.PlayOneShot(buySound);
    }
    public void PlaySellSound()
    {
        sfxAudioSource.PlayOneShot(sellSound);
    }
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicAudioSource != null)
        {
            musicAudioSource.clip = backgroundMusic;
            musicAudioSource.loop = true; 
            musicAudioSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
    }

}
