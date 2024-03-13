using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource playerSource;


    [Header("--- Audio Clip ---")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip walk;
    public AudioClip collectFish;
    public AudioClip crack1;
    public AudioClip crack2;
    public AudioClip crack3;
    public AudioClip thinIceCrack;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
    }

    public void PlayplayerSFX(AudioClip clip)
    {
        playerSource.clip = clip;
        playerSource.Play();
    }
}
