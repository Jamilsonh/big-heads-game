using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton

    [Header("Audio Sources")]
    //public AudioSource musicSource;      // Para música de fundo
    public AudioSource sfxSource;        // Para efeitos sonoros

    private void Awake() {
        // Singleton pattern
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else {
            Destroy(gameObject); // Garante que apenas uma instância exista
        }
    }

    /*
    public void PlayMusic(AudioClip musicClip) {
        if (musicSource.clip != musicClip) {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    */

    public void PlaySFX(AudioClip sfxClip) {
        sfxSource.PlayOneShot(sfxClip);
    }
}
